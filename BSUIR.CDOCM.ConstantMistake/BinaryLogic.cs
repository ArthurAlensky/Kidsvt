namespace BSUIR.CDOCM.ConstantMistake
{
    public static class BinaryLogic
    {
        public static int F(int node, int val)
        {
            return node == -1 ? val : node;
        }

        public static int Or(int val1, int val2)
        {
            return val1 | val2;
        }

        public static int And(int val1, int val2)
        {
            return val1 & val2;
        }

        public static int And(int val1, int val2, int val3)
        {
            return val1 & val2 & val3;
        }

        public static int Not(int val)
        {
            return val == 0 ? 1 : 0;
        }
    }
}
