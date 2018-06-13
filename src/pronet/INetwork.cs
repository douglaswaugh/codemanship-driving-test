using System.Collections.Generic;

namespace ProNet
{
    public interface INetwork
    {
        void Calculate();
        int DegreesOfSeparation(string programmer1, string programmer2);
        ProgrammerDto GetDetailsFor(string name);
        decimal TeamStrength(string language, string[] team);
    }
}