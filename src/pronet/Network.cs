using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Network : INetwork
    {
        public readonly IEnumerable<IProgrammer> _programmers;
        private readonly DegreesOfSeparation _degreesOfSeparation;

        public Network(IEnumerable<IProgrammer> programmers, IDegreesOfSeparationFactory degreesOfSeparationFactory)
        {
            _programmers = programmers;
            _degreesOfSeparation = degreesOfSeparationFactory.BuildDegreesOfSeparation(_programmers);
        }

        public void Calculate()
        {
            do
                UpdateRanks();
            while (1 - AverageRank() >= 0.000001m);
        }

        public ProgrammerDto GetDetailsFor(string name)
        {
            return GetByName(name).Details;
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return _degreesOfSeparation.Between(GetByName(programmer1), GetByName(programmer2));
        }

        public decimal TeamStrength(string language, string[] team)
        {
            var leader = team[0];

            var rankSkillDegrees = ((decimal)1 / (decimal)team.Length) * team
                .Select(member =>
                    new Tuple<decimal,int, int>(
                        GetDetailsFor(member).Rank,
                        Array.IndexOf(GetDetailsFor(member).Skills.ToArray(), language) + 1,
                        leader.Equals(member) ? 1 : DegreesOfSeparation(leader, member)))
                .Aggregate(0m, (total, rankSkillDegree) => total += rankSkillDegree.Item1 / (rankSkillDegree.Item2 * rankSkillDegree.Item3));

            return rankSkillDegrees;
        }

        private IProgrammer GetByName(string name)
        {
            return _programmers.Single(programmer => programmer.IsNamed(name));
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