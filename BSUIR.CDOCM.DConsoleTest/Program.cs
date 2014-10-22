using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Roth.LogicElement;
using BSUIR.CDOCM.Roth.Sequences;
using BSUIR.CDOCM.Roth.SingularRules;

namespace BSUIR.CDOCM.DConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var _3OrNot = new _3OrNot();
            //var _3And = new _3And();
            //var M2 = new M2();
            //var _3OrNotCubes = _3OrNot.DCubes;
            //var _3AndCubes = _3And.DCubes;
            //var M2Cubes = M2.DCubes;

            var _2OrNot = new _2OrNot() { Inputs = new List<int>() { 0, 1,7 } };
            var not = new Not() { Inputs = new List<int>() { 2, 8 } };
            var _2AndNot = new _2AndNot() { Inputs = new List<int>() { 4, 5, 9 } };
            var _3And = new _3And() { Inputs = new List<int>() { 3, 9, 6, 10 }, Logics = new List<BaseLogicElement>() { _2AndNot } };
            var _2Or = new _2Or() { Inputs = new List<int>() { 8, 10, 11 }, Logics = new List<BaseLogicElement>() { not, _3And } };
            var _2And = new _2And() { Inputs = new List<int>() { 7, 11, 12 }, Logics = new List<BaseLogicElement>() { _2OrNot, _2Or } };

            var _2OrNotDCubes = _2OrNot.DCubes;
            var notdCubes = not.DCubes;
            var _2AndNotDCubes = _2AndNot.DCubes;
            var _3AndDCubes = _3And.DCubes;
            var _2OrDCubes = _2Or.DCubes;
            var _2AndDCubes = _2And.DCubes;

            var elementList = new List<BaseLogicElement>() { _2OrNot, not, _2AndNot, _3And, _2Or, _2And };

            Process(_2AndNot, _2And, elementList);
        }

        private static void Process(BaseLogicElement failed, BaseLogicElement end, List<BaseLogicElement> elementList)
        {
            var elements1 = new List<BaseLogicElement>();
            FindPath(failed, end, elements1);
            var elements2 = new List<BaseLogicElement>();
            foreach (var el in elementList) 
            {
                if (!elements1.Contains(el)) 
                {
                    elements2.Add(el);
                }
            }
            elements2.Remove(failed);

            DirectWalk(failed, end, elements1, elements2);
        }

        private static void DirectWalk(BaseLogicElement failed, BaseLogicElement end, List<BaseLogicElement> elements1, List<BaseLogicElement> elements2)
        {
            const Value x = Value.X;
            List<Value> result;
            foreach (var primitive in failed.PrimitivDCubes)
            {
                result = new List<Value>() {x, x, x, x, x, x, x, x, x, x, x, x, x};

                Console.Write(Environment.NewLine);
                primitive.ForEach(e => Console.Write(e));
                Console.Write(":"+Environment.NewLine + "----------------------------------------" + Environment.NewLine);

                Intersect(ref result, primitive, failed.Inputs);
                foreach (var element in elements1)
                {
                    foreach (var dCube in element.DCubes)
                    {
                        if (Intersect(ref result, dCube, element.Inputs))
                        {
                            break;
                        }
                    }
                }

                ReverseWalk(result, elements2);
            }
        }

        private static void ReverseWalk(List<Value> vector, List<BaseLogicElement> elements) 
        {
            foreach (var element in elements)
            {
                foreach (var sCube in element.SingularCubes)
                {
                    if (Intersect(ref vector, sCube, element.Inputs))
                    {
                        break;
                    }
                }
            }

            vector.ForEach(e => Console.Write(e));
            Console.Write(Environment.NewLine);
        }

        private static bool Intersect(ref List<Value> result, List<Value> vector, IEnumerable<int> inputs)
        {
            var res = new List<Value>(result);
            int i = 0;
            foreach (var input in inputs)
            {
                res[input] = res[input].GetDIntersection(vector[i++]);
            }

            if (res.Contains(Value.O))
                return false;

            result = new List<Value>(res);
            return true;
        }

        private static bool FindPath(BaseLogicElement failed, BaseLogicElement end, List<BaseLogicElement> elements)
        {
            if(end == failed)
                return true;

            foreach (var el in end.Logics)
            {
                if (FindPath(failed, el, elements))
                {
                    elements.Add(end);
                    return true;
                }
            }

            return false;
        }
    }
}
