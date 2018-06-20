using System.Collections.Generic;

namespace ProNet
{
    public interface IRankCalculator
    {
        void Calculate(IEnumerable<IProgrammer> programmers);
        decimal CalculateRank(IProgrammer programmer);
    }
}