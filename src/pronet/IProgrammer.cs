using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammer
    { 
        IEnumerable<IProgrammer> Relations { get; }
        ProgrammerDto Details { get; }

        bool IsNamed(string name);
        void UpdateRank();
        void Recommends(Programmer programmer);
    }
}