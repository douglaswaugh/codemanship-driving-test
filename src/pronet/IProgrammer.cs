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
        IEnumerable<IProgrammer> Recommendations { get; }
        IEnumerable<IProgrammer> RecommendedBys { get; }

        void UpdateRank();
        void Recommends(IProgrammer programmer);
        void RecommendedBy(IProgrammer programmer);
        bool HasRecommended(IProgrammer programmer);
        bool WasRecommendedBy(IProgrammer programmer);
    }
}