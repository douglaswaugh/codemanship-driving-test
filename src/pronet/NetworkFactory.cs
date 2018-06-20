using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class NetworkFactory : INetworkFactory
    {
        private readonly IProgrammerFactory _programmerFactory;

        public NetworkFactory(IProgrammerFactory programmerFactory)
        {
            _programmerFactory = programmerFactory;
        }

        public INetwork BuildNetwork(IReadOnlyDictionary<string, IEnumerable<string>> recommendations, IReadOnlyDictionary<string, IEnumerable<string>> skills)
        {
            return new Network(_programmerFactory.BuildProgrammers(recommendations, skills), new DegreesOfSeparationFactory(), new TeamFactory(), new RankCalculatorFactory());
        }
    }
}