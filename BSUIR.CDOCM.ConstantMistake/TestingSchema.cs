using System;
namespace BSUIR.CDOCM.ConstantMistake
{
    public class TestingSchema : Schema
    {
        public TestingSchema() { XVector = new byte[7]; }

        public TestingSchema(int fun, int type)
        {
            XVector = new byte[7];
            Nodes = new[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
            Nodes[fun - 1] = type;
        }

        public byte Test()
        {
            return F6(true);
        }

        public byte Actual()
        {
            return F6(false);
        }

        public override byte F1(bool test)
        {
            return (byte)( test ?
                BinaryLogic.F(Nodes[7], BinaryLogic.Not(BinaryLogic.Or(BinaryLogic.F(Nodes[0], XVector[0]), BinaryLogic.F(Nodes[1], XVector[1]))))
                : BinaryLogic.Not(BinaryLogic.Or(XVector[0], XVector[1])));
        }

        public override byte F2(bool test)
        {
            return (byte)(test ? 
                BinaryLogic.F(Nodes[8], BinaryLogic.Not(BinaryLogic.F(Nodes[2], XVector[2])))
                : BinaryLogic.Not(XVector[2]));
        }

        public override byte F3(bool test)
        {
            return (byte)(test ?
                BinaryLogic.F(Nodes[9], BinaryLogic.Not(BinaryLogic.And(BinaryLogic.F(Nodes[4], XVector[4]), BinaryLogic.F(Nodes[5], XVector[5]))))
                : BinaryLogic.Not(BinaryLogic.And(XVector[4], XVector[5])));
        }

        public override byte F4(bool test)
        {
            return (byte)( test ?
                BinaryLogic.F(Nodes[10], BinaryLogic.And(BinaryLogic.F(Nodes[3], XVector[3]), F3(true), BinaryLogic.F(Nodes[6],XVector[6])))
                : BinaryLogic.And(XVector[3], F3(false), XVector[6]));
        }

        public override byte F5(bool test)
        {
            return (byte)(test ?
                BinaryLogic.F(Nodes[11], BinaryLogic.Or(F4(true), F2(true)))
                : BinaryLogic.Or(F4(false), F2(false)));
        }

        public override byte F6(bool test)
        {
            return (byte)(test ?
                BinaryLogic.F(Nodes[12], BinaryLogic.And(F1(true), F5(true)))
                : BinaryLogic.And(F1(false), F5(false)));
        }

        public byte[] GetVector()
        {
            var vector = new byte[13];
            Array.Copy(XVector, 0, vector, 0, XVector.Length);
            vector[7] = F1(false);
            vector[8] = F2(false);
            vector[9] = F3(false);
            vector[10] = F4(false);
            vector[11] = F5(false);
            vector[12] = F6(false);
            return vector;
        }
    }
}
