using System;
using BSUIR.CDOCM.ConstantMistake;
using System.Collections.Generic;
using BSUIR.CDOCM.Roth.LogicElement;

namespace BSUIR.CDOCM.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var _3OrNot = new _3OrNot();
            var _3And = new _3And();
            var M2 = new M2();
            var X1 = new X();
            var X2 = new X();
            var X3 = new X();
            var X4 = new X();
            var X5 = new X();

            

            var _3OrNotCubes = _3OrNot.DCubes;
            var _3AndCubes = _3And.DCubes;
            var M2Cubes = M2.DCubes;




            return;
            Console.WriteLine("Set number of function: ");
            var f = int.Parse(Console.ReadLine());

            Console.WriteLine("Set type of error: ");
            var type = int.Parse(Console.ReadLine());

            var scheme = new TestingSchema(f, type);

            for (int i = 0; i < 128; i++)
            {
                uint m = 1;
                for (int j = 1; j <= 7; j++)
                {
                    scheme.XVector[j] = (i & m) > 0 ? 1 : 0;
                    m = m << 1;
                }

                var test = scheme.Test();
                var actual = scheme.Actual();

                if (test != actual)
                {
                    foreach (var x in scheme.XVector)
                    {
                        Console.Write(x);
                    }

                    Console.WriteLine(" Test:{0} Actual:{1} " +  Environment.NewLine, test,actual);
                }
            }

        }
    }
}
