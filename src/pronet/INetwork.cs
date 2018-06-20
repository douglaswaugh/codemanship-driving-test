using System.Collections.Generic;

namespace ProNet
{
    public interface INetwork
    {
        int DegreesOfSeparation(string programmer1, string programmer2);
        ProgrammerDto GetDetailsFor(string name);
        decimal TeamStrength(string language, string[] team);
        string[] StrongestTeam(string language, int teamSize);
    }
}