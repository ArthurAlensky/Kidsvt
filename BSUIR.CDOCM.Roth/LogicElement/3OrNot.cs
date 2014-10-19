using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Fundamentals;
using BSUIR.CDOCM.Roth.Sequences;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class _3OrNot : BaseLogicElement
    {
        public _3OrNot() : base(3) 
        {
        }

        public override int F(List<Value> vector)
        {
            return (((int)vector[0]).Or((int)vector[1]).Or((int)vector[2])).Not();
        }
    }
}
