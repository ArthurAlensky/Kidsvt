using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSUIR.CDOCM.Fundamentals;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public class Not : BaseLogicElement
    {
        public Not() : base(1)
        {
        }

        public override int F(List<Sequences.Value> vector)
        {
            return ((int) vector[0]).Not();
        }
    }
}
