using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammer
    {
        string Name { get; }
        decimal ProgrammerRankShare { get; }
        IEnumerable<IProgrammer> Relations { get; }
        ProgrammerDto Details { get; }

        void UpdateRank();
        void Recommends(Programmer programmer);
    }
}