using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSUIR.CDOCM.Fundamentals;
using BSUIR.CDOCM.Roth.Sequences;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class M2 : BaseLogicElement
    {
        public M2() : base(2) 
        {
 
        }

        public override int F(List<Sequences.Value> vector)
        {
            return ((int)vector[0]).Xor((int)vector[1]);
        }
    }
}
