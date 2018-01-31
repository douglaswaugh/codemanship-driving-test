using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammer
    {
        string Name { get; }
        IEnumerable<string> RecommendedProgrammers { get; }
        IEnumerable<string> Skills { get; }
        decimal Rank { get; }
        decimal ProgrammerRankShare { get; }
        IEnumerable<IProgrammer> Relations { get; }

        void UpdateRank();
        void Recommends(IProgrammer programmer);
        void RecommendedBy(IProgrammer programmer);
        bool IsRelatedTo(IProgrammer programmer);
    }
}