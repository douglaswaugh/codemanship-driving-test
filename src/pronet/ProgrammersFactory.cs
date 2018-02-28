using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class ProgrammersFactory : IProgrammersFactory
    {
        private readonly IProgrammerFactory _programmerFactory;

        public ProgrammersFactory(IProgrammerFactory programmerFactory)
        {
            _programmerFactory = programmerFactory;
        }

        public IProgrammers BuildProgrammers(IReadOnlyDictionary<string, IEnumerable<string>> recommendations, IReadOnlyDictionary<string, IEnumerable<string>> skills)
        {
            var listOfProgrammers = skills
                .Select(programmer => _programmerFactory.BuildProgrammer(programmer.Key, programmer.Value))
                .ToList(); // required so BuildProgrammer is not called every time a Programmer is accessed inside Programmers

            return new Programmers(AddRecommendations(listOfProgrammers, recommendations).ToList());
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