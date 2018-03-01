using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ProNet
{
    public class XmlNetworkStore : INetworkStore
    {
        private readonly IXmlLoader _xmlLoader;
        private readonly INetworkFactory _networkFactory;

        public XmlNetworkStore(IXmlLoader xmlLoader, INetworkFactory networkFactory)
        {
            _xmlLoader = xmlLoader;
            _networkFactory = networkFactory;
        }

        public INetwork GetNetwork()
        {
            return _networkFactory.BuildNetwork(GetRecommendations(), GetSkills());
        }

        private IReadOnlyDictionary<string, IEnumerable<string>> GetRecommendations()
        {
            return GetProgrammerNames()
                .Select(name => new {
                    Programmer = name,
                    Recommendations = RecommendationsFor(name)
                })
                .ToDictionary(tuple => tuple.Programmer, tuple => tuple.Recommendations);
        }

        private IEnumerable<string> RecommendationsFor(string name)
        {
            return _xmlLoader.Load()
                .Descendants("Programmer")
                .Where(p => p.Attribute("name").Value == name)
                .Descendants("Recommendations")
                .Descendants("Recommendation")
                .Select(recommendation => recommendation.Value);
        }

        private IReadOnlyDictionary<string, IEnumerable<string>> GetSkills()
        {
            return GetProgrammerNames()
                .Select(name => new {Name = name, Skills = SkillsFor(name) })
                .ToDictionary(programmer => programmer.Name, programmer => programmer.Skills);
        }

        private IEnumerable<string> SkillsFor(string name)
        {
            return _xmlLoader.Load()
                .Descendants("Programmer")
                .Where(p => p.Attribute("name").Value == name)
                .Descendants("Skills")
                .Descendants("Skill")
                .Select(skill => skill.Value);
        }

        private IEnumerable<string> GetProgrammerNames()
        {
            return _xmlLoader.Load()
                .Descendants("Programmer")
                .Select(programmer => programmer.Attribute("name").Value );
        }
    }
}