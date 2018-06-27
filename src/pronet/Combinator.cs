using System;
using System.Collections.Generic;

namespace ProNet.Test
{
    public class Combinator
    {
        public IEnumerable<IEnumerable<IProgrammer>> CombinationsFor(IEnumerable<IProgrammer> programmers, int size)
        {
            if (size == 0)
                return new List<IEnumerable<IProgrammer>>();

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