using BSUIR.CDOCM.ConstantMistake;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.CDOCM.SwitchingConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sets = new Dictionary<int, List<List<int>>>();

            for (int fun = 1; fun <= 13; fun++)
            {
                for (int type = 0; type < 2; type++)
                {
                    var scheme = new TestingSchema(fun, type);

                    for (int i = 0; i < 128; i++)
                    {
                        uint m = 1;
                        for (int j = 0; j < 7; j++)
                        {
                            scheme.XVector[j] = (byte)((i & m) > 0 ? 1 : 0);
                            m = m << 1;
                        }

                        var test = scheme.Test();
                        var actual = scheme.Actual();

                        if (test != actual)
                        {
                            var builder = new StringBuilder();
                            foreach (var x in scheme.XVector)
                            {
                                builder.Append(x.ToString());
                            }
                            var set = builder.ToString();
                            if (!sets.ContainsKey(i))
                                sets.Add(i, new List<List<int>>{ new List<int>(), new List<int>()});

                            sets[i][type].Add(fun);
                        }
                    }
                }
            }

            var max = int.MinValue;
            var maxSet = new KeyValuePair<int,List<List<int>>>(0, new List<List<int>>());
            foreach(var set in sets)
            {
                if (set.Value[0].Count + set.Value[1].Count > max)
                {
                    max = set.Value[0].Count + set.Value[1].Count;
                    maxSet = set;
                }
            }

            var minSets = new Dictionary<int, List<List<int>>> { {maxSet.Key, maxSet.Value} };

            while (true)
            {
                var maxCount = 0;
                foreach (var set in sets)
                {
                    if (!minSets.ContainsKey(set.Key))
                    {
                        var count = 0;
                        foreach (var fun in set.Value[0])
                        {
                            count++;
                            foreach (var minSet in minSets)
                                if (minSet.Value[0].Contains(fun)) { count--; break; }
                        }

                        foreach (var fun in set.Value[1])
                        {
                            count++;
                            foreach (var minSet in minSets)
                                if (minSet.Value[1].Contains(fun)) { count--; break; }
                        }

                        if (count > maxCount)
                        {
                            maxCount = count;
                            maxSet = set;
                        }
                    }
                }

                if (maxCount == 0)
                    break;

                minSets.Add(maxSet.Key, maxSet.Value);
            }

            var vectors = new List<byte[]>();

            foreach (var set in minSets)
            {
                var scheme = new TestingSchema();
                uint m = 1;
                for (int j = 0; j < 7; j++)
                {
                    scheme.XVector[j] = (byte)((set.Key & m) > 0 ? 1 : 0);
                    m = m << 1;
                }
                vectors.Add(scheme.GetVector());
            }

            var switchingTable = new Dictionary<int, List<int>>();

            for (int i = 0; i < vectors.Count; i++)
            {
                switchingTable.Add(i, new List<int>());
                for (int j = 0; j < vectors.Count; j++ )
                {
                    var count = 0;

                    if (i != j)
                    {
                        for (int k = 0; k < vectors[i].Length; k++)
                        {
                            if (vectors[i][k] != vectors[j][k])
                                count++;
                        }
                    }
                    switchingTable[i].Add(count);
                }
            }

            var excluded = new List<int>();
            foreach (var test in switchingTable)
            {
                excluded.Add(test.Key);
                var minSwitching = Min(switchingTable, test.Key, excluded);
                Console.Write("TS{0}={1}{2}", test.Key + 1, minSwitching, Environment.NewLine);
                excluded.Clear();
            }
        }

        public static int Min( Dictionary<int, List<int>> table, int row, List<int> excluded ) 
        {
            Console.Write("TS{0}-", row + 1);
            var min = int.MaxValue;
            var minIndex = -1;
            foreach (var switchVal in table[row])
            {
                if (switchVal < min && !excluded.Contains(table[row].IndexOf(switchVal)) )
                {
                    min = switchVal;
                    minIndex = table[row].IndexOf(switchVal);
                }
            }

            if (minIndex < 0)
                return table[row][excluded.First()];

            excluded.Add(minIndex);
            min += Min(table, minIndex, excluded);
            return min;
        }
    }
}
