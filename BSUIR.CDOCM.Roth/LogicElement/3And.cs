using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Fundamentals;
using BSUIR.CDOCM.Roth.Sequences;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class _3And : BaseLogicElement
    {
        public _3And() : base(3) 
        {
        }

        public override int F(List<Sequences.Value> vector)
        {
            return ((int)vector[0]).And((int)vector[1]).And((int)vector[2]);
        }
    }
}
