using BSUIR.CDOCM.Roth.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.CDOCM.Roth.SingularRules
{
    public static class Intersection
    {
        public static Value GetD(this Value arg1, Value arg2) 
        {
            var res = Value.X;

            if (arg1 == Value.X)
                res = arg2;

            if (arg2 == Value.X)
                res = arg1;

            if (arg1 == Value.One && arg2 == Value.Zero)
                res = Value.D;

            if (arg1 == Value.Zero && arg2 == Value.One)
                res = Value._D;

            return res;
        }


        public static Value GetSingular(this Value arg1, Value arg2)
        {
            var res = arg1;

            if (arg1 == Value.One && arg2 == Value.Zero)
                res = Value.X;

            if (arg1 == Value.Zero && arg2 == Value.One)
                res = Value.X;

            return res;
        }
    }
}
