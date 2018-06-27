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
                var combinations = new List<IEnumerable<IProgrammer>>();

                foreach(var programmer in programmers)
                    combinations.Add(new List<IProgrammer>{programmer});

                return combinations;
            }
        }
    }
}