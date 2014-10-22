using BSUIR.CDOCM.Roth.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSUIR.CDOCM.Roth.SingularRules;

namespace BSUIR.CDOCM.Roth.LogicElement
{
    public abstract class BaseLogicElement
    {
        private int _numOfIns;

        public List<BaseLogicElement> Inputs { get; set; }
        private List<List<Value>> _singularCubes;
        private List<List<Value>> _dCubes;

        public List<List<Value>> SingularCubes
        {
            get { return _singularCubes ?? (_singularCubes = GenerateSingularCubes()); }
        }

        public List<List<Value>> DCubes
        {
            get { return _dCubes ?? (_dCubes = GenerateDCubes()); }
        }

        protected BaseLogicElement(int numOfIns)
        {
            _numOfIns = numOfIns;
        }

        private List<List<Value>> GenerateDCubes()
        {
            return DBonding(SingularCubes);
        }

        private List<List<Value>> GenerateSingularCubes()
        {
            var vectors = GenerateAllVectors();

            return MaxCovering(SingularBonding(vectors));
        }

        private List<List<Value>> GenerateAllVectors()
        {
            var vectors = new List<List<Value>>();
            for (int i = 0; i < Math.Pow(2, _numOfIns); i++)
            {
                var vector = new List<Value>();
                uint m = 1;
                for (int j = 0; j <= _numOfIns - 1; j++)
                {
                    vector.Add((Value)((i & m) > 0 ? 1 : 0));
                    m = m << 1;
                }
                vector.Add((Value)F(vector));
                vectors.Add(vector);
            }

            return vectors;
        }

        private List<List<Value>> DBonding(List<List<Value>> sequences)
        {
            var output = new List<List<Value>>();
            for (int i = 0; i < sequences.Count; i++)
            {
                for (int j = i; j < sequences.Count; j++)
                {
                    if (sequences[i].Last() != sequences[j].Last())
                    {
                        var vector = new List<Value>(sequences[j].Count + 1);

                        for (int k = 0; k < sequences[i].Count; k++)
                        {
                            var val1 = (Sequences.Value)sequences[i][k];
                            var val2 = (Sequences.Value)sequences[j][k];
                            vector.Add(val1.GetD(val2));
                        }
                        output.Add(vector);
                    }
                }
            }

            return output;
        }

        private List<List<Value>> SingularBonding(List<List<Value>> sequences)
        {
            var temp = new List<List<Value>>(sequences);
            var output = new List<List<Value>>();
            for (int k = 0; k < 3; k++)
            {
                output.Clear();
                for (int i = 0; i < temp.Count; i++)
                {
                    int z = 0;
                    for (int j = i; j < temp.Count; j++)
                    {
                        if (Conglutinable(temp[i], temp[j]))
                        {
                            z++;
                            var vector = Conglutinate(temp[i], temp[j]);
                            output.Add(vector);
                        }
                    }

                    if (z < 1)
                        output.Add(new List<Value>(temp[i]));
                }
                temp.Clear();
                temp.AddRange(output);

            }
            return output;
        }

        private bool Conglutinable(List<Value> vector1, List<Value> vector2)
        {
            int z = 0;
            if (vector1.Last() == vector2.Last())
            {
                for (int i = 0; i < vector1.Count - 1; i++)
                    if (vector1[i] != vector2[i])
                        z++;

                return z == 1;
            }

            return false;
        }

        private List<Value> Conglutinate(List<Value> vector1, List<Value> vector2)
        {
            var vector = new List<Value>(vector2.Count);

            for (int k = 0; k < vector1.Count; k++)
            {
                var val1 = (Sequences.Value)vector1[k];
                var val2 = (Sequences.Value)vector2[k];
                vector.Add(val1.GetIntersection(val2));
            }

            return vector;
        }

        private List<List<Value>> MaxCovering(List<List<Value>> sequences)
        {
            var temp = new List<List<Value>>(sequences);
            for (int i = 0; i < temp.Count; i++)
            {
                if (!temp[i].HasX())
                    continue;

                for (int j = 0; j < temp.Count; j++)
                {
                    if ( i != j && temp[i].IsCovering(temp[j]))
                    {
                        temp.RemoveAt(j);
                        i -= j > i ? 0 : 1;
                        j--;
                    }
                }
            }
            return temp;
        }

        public abstract int F(List<Value> vector);
    }
}
