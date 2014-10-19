using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.CDOCM.Fundamentals
{
    public static class BinaryLogic
    {
        public static int F(int node, int val)
        {
            return node == -1 ? val : node;
        }

        public static int Or(this int val1, int val2)
        {
            return val1 | val2;
        }

        public static int And(this int val1, int val2)
        {
            return val1 & val2;
        }

        public static int And(this int val1, int val2, int val3)
        {
            return val1 & val2 & val3;
        }

        public static int Not(this int val)
        {
            return val == 0 ? 1 : 0;
        }

        public static int Xor(this int val1, int val2)
        {
            return val1 ^ val2;
        }
    }
}
