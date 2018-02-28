using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class ProgrammerFactory : IProgrammerFactory
    {
        public IEnumerable<IProgrammer> BuildProgrammers(IReadOnlyDictionary<string, IEnumerable<string>> recommendations, IReadOnlyDictionary<string, IEnumerable<string>> skills)
        {
            var listOfProgrammers = BuildProgrammersWithSkills(skills);

            return AddRecommendations(listOfProgrammers, recommendations);
        }

        private List<IProgrammer> BuildProgrammersWithSkills(IReadOnlyDictionary<string, IEnumerable<string>> skills)
        {
            return skills
                .Select(programmer => BuildProgrammer(programmer.Key, programmer.Value))
                .ToList(); // required so BuildProgrammer is not called every time a Programmer is accessed inside Programmers
        }

        private IProgrammer BuildProgrammer(string name, IEnumerable<string> skills)
        {
            return new Programmer(name, skills);
        }

        private IEnumerable<IProgrammer> AddRecommendations(IEnumerable<IProgrammer> programmers, IReadOnlyDictionary<string, IEnumerable<string>> recommendations)
        {
            foreach (var recommender in recommendations)
            {
                AddRecommendations(programmers, GetProgrammerByName(programmers, recommender.Key), recommendations[recommender.Key]);
            }

            return programmers;
        }

        private void AddRecommendations(IEnumerable<IProgrammer> programmers, IProgrammer recommender, IEnumerable<string> recommendations)
        {
            foreach (var recommendation in recommendations)
            {
                recommender.Recommends(GetProgrammerByName(programmers, recommendation));
            }
        }

        private IProgrammer GetProgrammerByName(IEnumerable<IProgrammer> programmers, string name)
        {
            return programmers.Single(programmer => programmer.Name == name);
        }
    }
}