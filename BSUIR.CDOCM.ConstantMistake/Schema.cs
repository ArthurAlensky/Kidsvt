namespace BSUIR.CDOCM.ConstantMistake
{
    public abstract class Schema
    {
        public int[] XVector { get; set; }
        protected int[] Nodes { get; set; }

        public abstract int F1(bool test);
        public abstract int F2(bool test);
        public abstract int F3(bool test);
        public abstract int F4(bool test);
        public abstract int F5(bool test);
        public abstract int F6(bool test);
    }
}
