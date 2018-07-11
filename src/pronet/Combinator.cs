using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet.Test
{
    public class Combinator
    {
        public IEnumerable<IEnumerable<IProgrammer>> CombinationsFor(IEnumerable<IProgrammer> programmers, int size)
        {
            if (size == 0)
                return new List<IEnumerable<IProgrammer>>();

            if (programmers.Count() == size)
                return new List<IEnumerable<IProgrammer>>{programmers};

            else
            {
                IEnumerable<IEnumerable<IProgrammer>> combinations = new List<IEnumerable<IProgrammer>>();

                for (int i = size; i > 0; i--)
                {
                    var set = programmers.Skip(i - 1).Take(programmers.Count() - (size - 1));
                    combinations = AddSetToCombinations(set, combinations);
                }

                return combinations;
            }
        }

        public IEnumerable<IEnumerable<IProgrammer>> AddSetToCombinations(IEnumerable<IProgrammer> set, IEnumerable<IEnumerable<IProgrammer>> initialCombinations)
        {
            if(!initialCombinations.Any())
                return set.Select(element => new List<IProgrammer>{element});

            var combinations = new List<IEnumerable<IProgrammer>>();

            foreach(var element in set)
            {
                foreach (var combination in initialCombinations)
                {
                    if (!combination.Contains(element))
                    {
                        var newCombination = new List<IProgrammer>();
                        newCombination.Add(element);
                        newCombination.AddRange(combination);
                        combinations.Add(newCombination);
                    }
                }
            }

            return combinations;
        }
    }
}