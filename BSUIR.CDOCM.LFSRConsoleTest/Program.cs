using BSUIR.CDOCM.ConstantMistake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.CDOCM.LFSRConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                
                for (int fun = 1; fun <= 13; fun++)
                {
                    
                    var minTicks = int.MaxValue;
                    for (int type = 0; type <= 1; type++)
                    {
                        var lfsr = new LFSR.Lfsr(new byte[] { 1, 0, 0, 0, 0, 0, 0, 0 });
                        var scheme = new TestingSchema(fun, type);
                        var mistakes = new Dictionary<int, List<int>> 
                        { 
                            { 0, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 } },
                            { 1, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 } } 
                        };
                        int ticks = 0;
                        while( ++ticks <= 128 )
                        {
                            scheme.XVector = lfsr.Registry;
                            lfsr.Shift();
                            foreach (var x in scheme.XVector)
                            {
                                Console.Write(x);
                            }
                            Console.Write(Environment.NewLine);
                            var test = scheme.Test();
                            var actual = scheme.Actual();

                            if (test != actual)
                            {
                                mistakes[type].Remove(fun);
                            }

                            if (mistakes[0].Count < 1 && mistakes[1].Count < 1)
                                break;
                        }

                        if (ticks >= 128)
                        {
                            Console.WriteLine("Test doesnot cover all the mistakes");
                        }

                        if (ticks < minTicks)
                        {
                            Console.WriteLine(ticks);

                            minTicks = ticks;
                        }
                    }
                }
            }
        }
    }
}
