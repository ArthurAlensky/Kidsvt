using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Fundamentals;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class _2OrNot : BaseLogicElement
    {
        public _2OrNot() : base(2) 
        {
        }

        public override int F(List<Sequences.Value> vector)
        {
            return (((int)vector[0]).Or((int)vector[1])).Not();
        }
    }
}
