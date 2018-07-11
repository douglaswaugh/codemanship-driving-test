using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Combinator2
    {
        public IEnumerable<IEnumerable<IProgrammer>> CombinationsFor(IEnumerable<IProgrammer> programmers, int size)
        {
            if (size == 0)
                return new List<IEnumerable<IProgrammer>>();

            if (programmers.Count() == size)
                return new List<IEnumerable<IProgrammer>>{programmers};

            var combinations = new List<ICollection<IProgrammer>>();

            for (var i = 0;i<size;i++)
            {
                combinations.Add(new List<IProgrammer>());
            }

            for (var i = 0;i<size;i++)
            {
                AddSetToCombinations(combinations, programmers);
            }

            combinations = RemoveCombinationsWithDuplicateProgrammers(combinations);
            combinations = OrderCombinations(combinations);
            combinations = RemoveDuplicateCombinations(combinations);

            return null;
        }

        private List<ICollection<IProgrammer>> RemoveDuplicateCombinations(List<ICollection<IProgrammer>> combinations)
        {
            return null;
        }

        public List<ICollection<IProgrammer>> OrderCombinations(List<ICollection<IProgrammer>> combinations)
        {
            var orderedCombinations = new List<ICollection<IProgrammer>>();

            foreach (var combination in combinations)
            {
                orderedCombinations.Add(combination.OrderBy(c => c.Details.Name).ToList());
            }

            return orderedCombinations;
        }

        public List<ICollection<IProgrammer>> RemoveCombinationsWithDuplicateProgrammers(List<ICollection<IProgrammer>> combinations)
        {
            var reducedCombinations = new List<ICollection<IProgrammer>>();
            foreach (var combination in combinations)
            {
                if (combination.Distinct().Count() == combination.Count())
                {
                    reducedCombinations.Add(combination);
                }
            }

            return reducedCombinations;
        }

        public void AddSetToCombinations(IEnumerable<ICollection<IProgrammer>> combinations, IEnumerable<IProgrammer> programmers)
        {
            foreach(var combination in combinations)
            {
                foreach(var programmer in programmers)
                {
                    combination.Add(programmer);
                }
            }
        }
    }
}