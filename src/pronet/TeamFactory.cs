using System.Collections.Generic;

namespace ProNet
{
    public class TeamFactory : ITeamFactory
    {
        public Team CreateTeam(string language, IEnumerable<IProgrammer> members, DegreesOfSeparation degreesOfSeparation)
        {
            return new Team(language, members, degreesOfSeparation);
        }
    }
}