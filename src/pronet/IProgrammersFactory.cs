using System.Collections.Generic;

namespace ProNet
{
    public interface INetworkFactory
    {
        INetwork BuildNetwork(IReadOnlyDictionary<string, IEnumerable<string>> recommendations, IReadOnlyDictionary<string, IEnumerable<string>> skills);
    }
}