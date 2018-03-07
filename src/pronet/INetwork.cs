using System.Collections.Generic;

namespace ProNet
{
    public interface INetwork
    {
        void Calculate();
        decimal RankFor(string name);
        int DegreesOfSeparation(string programmer1, string programmer2);
        ProgrammerDto GetDetailsFor(string name);
    }
}