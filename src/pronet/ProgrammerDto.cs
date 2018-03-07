using System.Collections.Generic;

namespace ProNet
{
    public class ProgrammerDto
    {
        private string _name;
        private decimal _rank;
        private IEnumerable<string> _recommendations;
        private IEnumerable<string> _skills;

        public ProgrammerDto(string name, decimal rank, IEnumerable<string> recommendations, IEnumerable<string> skills)
        {
            _name = name;
            _rank = rank;
            _recommendations = recommendations;
            _skills = skills;
        }

        public string Name => _name;
        public decimal Rank => _rank;
        public IEnumerable<string> Recommendations => _recommendations;
        public IEnumerable<string> Skills => _skills;
    }
}