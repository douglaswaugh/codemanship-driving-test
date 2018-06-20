using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class RankCalculator : IRankCalculator
    {
        private IEnumerable<IProgrammer> _programmers;

        public void Calculate(IEnumerable<IProgrammer> programmers)
        {
            _programmers = programmers;

            int count = 0;
            do
            {
                UpdateRanks();
                count++;
            }
            while (1 - AverageRank() >= 0.000001m && count < 10000);
        }

        private decimal AverageRank()
        {
            var totalRank = 0m;
            foreach (var programmer in _programmers)
            {
                totalRank += programmer.Details.Rank;
            }
            return totalRank / _programmers.Count();
        }

        private void UpdateRanks()
        {
            foreach (var programmer in _programmers)
            {
                programmer.UpdateRank();
            }
        }
    }
}