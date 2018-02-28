using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammerFactory
    {
        IEnumerable<IProgrammer> BuildProgrammers(IReadOnlyDictionary<string, IEnumerable<string>> recommendations, IReadOnlyDictionary<string, IEnumerable<string>> skills);
    }
}