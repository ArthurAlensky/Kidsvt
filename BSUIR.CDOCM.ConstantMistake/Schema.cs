namespace BSUIR.CDOCM.ConstantMistake
{
    public abstract class Schema
    {
        public byte[] XVector { get; set; }
        protected int[] Nodes { get; set; }

        public abstract byte F1(bool test);
        public abstract byte F2(bool test);
        public abstract byte F3(bool test);
        public abstract byte F4(bool test);
        public abstract byte F5(bool test);
        public abstract byte F6(bool test);
    }
}
