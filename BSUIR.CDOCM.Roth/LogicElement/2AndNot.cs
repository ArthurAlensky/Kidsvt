using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Fundamentals;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class _2AndNot : BaseLogicElement
    {
        public _2AndNot() : base(2)
        {
        }

        public override int F(List<Sequences.Value> vector)
        {
            return (((int)vector[0]).And((int)vector[1])).Not();
        }
    }
}
