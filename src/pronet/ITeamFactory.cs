using System.Collections.Generic;

namespace ProNet
{
    public interface ITeamFactory
    {
        Team CreateTeam(string language, IEnumerable<IProgrammer> members, DegreesOfSeparation degreesOfSeparation);
    }
}