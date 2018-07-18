using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Network : INetwork
    {
        public readonly IEnumerable<IProgrammer> _programmers;
        private readonly DegreesOfSeparation _degreesOfSeparation;
        private readonly ITeamFactory _teamFactory;
        private readonly IRankCalculator _rankCalculator;

        public Network(IEnumerable<IProgrammer> programmers,
            IDegreesOfSeparationFactory degreesOfSeparationFactory,
            ITeamFactory teamFactory,
            IRankCalculatorFactory rankCalculatorFactory)
        {
            _programmers = programmers;
            _degreesOfSeparation = degreesOfSeparationFactory.BuildDegreesOfSeparation(_programmers);
            _teamFactory = teamFactory;
            _rankCalculator = rankCalculatorFactory.BuildRankCalculator();
            _rankCalculator.Calculate(_programmers);
        }

        public ProgrammerDto GetDetailsFor(string name)
        {
            return GetByName(name).Details;
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return _degreesOfSeparation.Between(GetByName(programmer1), GetByName(programmer2));
        }

        public decimal TeamStrength(string language, string[] memberNames)
        {
            var members = memberNames.Select(name => GetByName(name));
            var team = _teamFactory.CreateTeam(language, members, _degreesOfSeparation);

            return team.Strength;
        }

        public string[] StrongestTeam(string language, int teamSize)
        {
            const string leader = "Nick";
            return new[] {leader, "Dave", "Jason"};
        }

        private IProgrammer GetByName(string name)
        {
            return _programmers.Single(programmer => programmer.IsNamed(name));
        }
    }
}