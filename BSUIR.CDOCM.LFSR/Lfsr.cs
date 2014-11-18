using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.CDOCM.LFSR
{
    public class Lfsr
    {
        byte[] bits;

        public Lfsr(int bitCount)
        {
            bits = new byte[bitCount];
        }

        public Lfsr(byte[] reg)
        {
            bits = new byte[reg.Length];
            Array.Copy(reg, 0, bits, 0, reg.Length);
        }

        public byte[] Registry
        {
            get
            {
                var registry = new byte[bits.Length];
                Array.Copy(bits, 0, registry, 0, bits.Length);
                return registry;
            }
        }

        //Linear Feedback Shift Register
        //#7. X7(+)X6(+)X5(+)X4(+)1
        public void Shift()
        {
            byte prev;
            byte r;

            prev = bits[0];
            for (int i = 1; i < bits.Length; i++)
            {
                if (i == 4 || i == 5 || i == 6) // element (+)
                {
                    r = (byte)(prev ^ bits[i]);
                    bits[i] = prev;
                    prev = r;
                }
                else
                {
                    byte t = bits[i];
                    bits[i] = prev;
                    prev = t;
                }
            }
            bits[0] = prev;
        }

    }
}
