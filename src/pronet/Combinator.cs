using System;
using System.Collections.Generic;

namespace ProNet.Test
{
    public class Combinator
    {
        public IEnumerable<IProgrammer> CombinationsFor(IEnumerable<IProgrammer> programmers, int size)
        {
            if (size == 0)
                return new List<IProgrammer>();

            else
                return programmers;
        }
    }
}