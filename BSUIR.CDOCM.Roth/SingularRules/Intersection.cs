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
            var res = arg1;

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

        public static Value GetDIntersection(this Value arg1, Value arg2)
        {
            var res = Value.O;

            if (arg2 == arg1 || arg2 == Value.X)
                res = arg1;

            if (arg1 == arg2 || arg1 == Value.X)
                res = arg2;

            return res;
        }

        public static Value GetIntersection(this Value arg1, Value arg2)
        {
            var res = arg1;

            if (arg1 == Value.One && arg2 == Value.Zero)
                res = Value.X;

            if (arg1 == Value.Zero && arg2 == Value.One)
                res = Value.X;

            return res;
        }

        public static bool HasX( this List<Value> vector )
        {
            return vector.Contains(Value.X);
        }

        public static bool IsCovering(this List<Value> vector1, List<Value> vector2)
        {
            int z = 0;
            if (vector1.Last() == vector2.Last())
            {
                for (int i = 0; i < vector1.Count - 1; i++)
                    if (vector1[i] != vector2[i] && vector1[i] != Value.X)
                        z++;

                return z == 0;
            }

            return false;
        }
    }
}
