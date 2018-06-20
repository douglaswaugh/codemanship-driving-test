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
                return new List<IEnumerable<IProgrammer>> { programmers };
        }
    }
}