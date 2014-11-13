using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Fundamentals;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class _2And : BaseLogicElement
    {
        public _2And() : base(2)
        {
        }

        public override int F(List<Sequences.Value> vector)
        {
            return ((int)vector[0]).And((int)vector[1]);
        }
    }
}
