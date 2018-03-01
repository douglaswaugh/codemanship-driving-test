using System;
using System.Linq;

namespace ProNet
{
    public class ProNet : IProNet
    {
        private readonly INetwork _network;

        public ProNet(INetworkStore networkStore)
        {
            _network = networkStore.GetNetwork();
            _network.Calculate();
        }

        public string[] Skills(string programmer)
        {
            return _network.Skills(programmer).ToArray();
        }

        public string[] Recommendations(string programmer)
        {
            return _network.RecommendationsFor(programmer).ToArray();
        }

        public double Rank(string programmer)
        {
            return Convert.ToDouble(_network.RankFor(programmer));
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return _network.DegreesOfSeparation(programmer1, programmer2);
        }

        public double TeamStrength(string language, string[] team)
        {
            throw new System.NotImplementedException();
        }

        public string[] FindStrongestTeam(string language, int teamSize)
        {
            throw new System.NotImplementedException();
        }
    }
}