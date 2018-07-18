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
            combinations.Add(new List<IProgrammer>());

            for (var i = 0;i<size;i++)
                combinations = AddSetToCombinations(combinations, programmers);

            combinations = RemoveCombinationsWithDuplicateProgrammers(combinations);
            combinations = OrderCombinations(combinations);
            combinations = RemoveDuplicateCombinations(combinations);

            return combinations;
        }

        public List<ICollection<IProgrammer>> RemoveDuplicateCombinations(List<ICollection<IProgrammer>> combinations)
        {
            var uniqueCombinations = new List<ICollection<IProgrammer>>();

            foreach (var combination in combinations)
            {
                if (!uniqueCombinations.Any(c => c.SequenceEqual(combination)))
                    uniqueCombinations.Add(combination);
            }

            return uniqueCombinations;
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

        public List<ICollection<IProgrammer>> AddSetToCombinations(IEnumerable<ICollection<IProgrammer>> combinations, IEnumerable<IProgrammer> programmers)
        {
            var newCombinations = new List<ICollection<IProgrammer>>();

            foreach(var combination in combinations)
            {
                foreach(var programmer in programmers)
                {
                    var newCombination = new List<IProgrammer>();
                    newCombination.AddRange(combination);
                    newCombination.Add(programmer);
                    newCombinations.Add(newCombination);
                }
            }

            return newCombinations;
        }

        public List<ICollection<IProgrammer>> AddTeamsWithEachMemberAsLeader(List<ICollection<IProgrammer>> combinations)
        {
            var newCombinations = new List<ICollection<IProgrammer>>();

            foreach(var combination in combinations)
            {
                foreach(var member in combination)
                {
                    var newCombination = new List<IProgrammer>();
                    newCombination.Add(member);
                    newCombination.AddRange(combination.Where(m => m != member));
                    newCombinations.Add(newCombination);
                }
            }

            return newCombinations;
        }
    }
}