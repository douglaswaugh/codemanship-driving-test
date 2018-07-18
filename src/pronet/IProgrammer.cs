using System;
using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammer
    { 
        ProgrammerDto Details { get; }

        bool IsNamed(string name);
        void UpdateRank();
        void Recommends(Programmer programmer);
        IEnumerable<IProgrammer> Relations();
        IEnumerable<Programmer> RecommendedBys { get; }
        decimal ProgrammerRankShare { get; }
        bool HasSkill(string language);
    }
}