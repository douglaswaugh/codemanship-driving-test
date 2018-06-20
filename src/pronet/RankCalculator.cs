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

        public decimal CalculateRank(IProgrammer programmer)
        {
            var _recommendedBys = programmer.RecommendedBys;

            // (1 - d) + d(PR(T1)/C(T1)) + ... + d(PR(Tn)/C(Tn))
            return _recommendedBys
                .Aggregate(1m - 0.85m, (current, p) => current + 0.85m * p.ProgrammerRankShare);
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