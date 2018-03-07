using System.Collections.Generic;

namespace ProNet
{
    public class ProgrammerDto
    {
        private string _name;
        private decimal _rank;
        private IEnumerable<string> _recommendedProgrammers;
        private IEnumerable<string> _skills;

        public ProgrammerDto(string name, decimal rank, IEnumerable<string> recommendedProgrammers, IEnumerable<string> skills)
        {
            _name = name;
            _rank = rank;
            _recommendedProgrammers = recommendedProgrammers;
            _skills = skills;
        }

        public string Name => _name;
        public decimal Rank => _rank;
        public IEnumerable<string> RecommendedProgrammers => _recommendedProgrammers;
        public IEnumerable<string> Skills => _skills;
    }
}