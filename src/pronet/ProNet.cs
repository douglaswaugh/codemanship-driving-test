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

            var rankSkillDegrees = ((double)1 / (double)team.Length) * team
                .Select(member =>
                    new Tuple<double,int, int>(
                        Rank(member),
                        Array.IndexOf(Skills(member), language) + 1,
                        leader.Equals(member) ? 1 : DegreesOfSeparation(leader, member)))
                .Aggregate(0d, (total, rankSkillDegree) => total += rankSkillDegree.Item1 / (rankSkillDegree.Item2 * rankSkillDegree.Item3));

            return rankSkillDegrees;
        }

        public string[] FindStrongestTeam(string language, int teamSize)
        {
            throw new System.NotImplementedException();
        }
    }
}