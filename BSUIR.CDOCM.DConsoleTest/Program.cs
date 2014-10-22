﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Roth.LogicElement;

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

            var _2OrNot = new _2OrNot() { Inputs = new List<int>() { 0, 1 } };
            var not = new Not() { Inputs = new List<int>() { 2 } };
            var _2AndNot = new _2AndNot() { Inputs = new List<int>() { 4, 5 } };
            var _3And = new _3And() { Inputs = new List<int>() { 3, 9, 6 }, Logics = new List<BaseLogicElement>() { _2AndNot } };
            var _2Or = new _2Or() { Inputs = new List<int>() { 8, 10 }, Logics = new List<BaseLogicElement>() { not, _3And } };
            var _2And = new _2And() { Inputs = new List<int>() { 7, 11 }, Logics = new List<BaseLogicElement>() { _2OrNot, _2Or } };

            var _2OrNotDCubes = _2OrNot.DCubes;
            var notdCubes = not.DCubes;
            var _2AndNotDCubes = _2AndNot.DCubes;
            var _3AndDCubes = _3And.DCubes;
            var _2OrDCubes = _2Or.DCubes;
            var _2AndDCubes = _2And.DCubes;

            DWalkThrough(_2AndNot, _2And);
        }

        private static void Process( BaseLogicElement failed )
        {

        }

        private static void DWalkThrough(BaseLogicElement failed, BaseLogicElement end)
        {
            var elements = new List<BaseLogicElement>();
            FindPath(failed, end, elements);
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