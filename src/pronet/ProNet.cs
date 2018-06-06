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
            return _network.GetDetailsFor(programmer).Skills.ToArray();
        }

        public string[] Recommendations(string programmer)
        {
            return _network.GetDetailsFor(programmer).Recommendations.ToArray();
        }

        public double Rank(string programmer)
        {
            return Convert.ToDouble(_network.GetDetailsFor(programmer).Rank);
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return _network.DegreesOfSeparation(programmer1, programmer2);
        }

        public double TeamStrength(string language, string[] team)
        {
            var leader = team[0];

            double jasonRank = Rank(leader);
            int jasonRubySkillIndex = Array.IndexOf(Skills(leader), "Ruby") + 1;;
            double billRank = Rank("Bill");
            int billRubySkillIndex = Array.IndexOf(Skills("Bill"), "Ruby") + 1;
            int billDegreesFromJason = DegreesOfSeparation(leader, "Bill");
            double frankRank = Rank("Frank");
            int frankRubySkillIndex = Array.IndexOf(Skills("Frank"), "Ruby") + 1;
            int frankDegreesFromJason = DegreesOfSeparation(leader, "Frank");

            double teamStrength = ((double)1 / (double)3) * ((jasonRank / jasonRubySkillIndex) + (billRank / (billRubySkillIndex * billDegreesFromJason)) + (frankRank / (frankRubySkillIndex * frankDegreesFromJason)));

            return teamStrength;
        }

        public string[] FindStrongestTeam(string language, int teamSize)
        {
            throw new System.NotImplementedException();
        }
    }
}