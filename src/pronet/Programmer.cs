using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Programmer : IProgrammer
    {
        private readonly string _name;
        private decimal _rank;
        private readonly ICollection<Programmer> _recommendations;
        private readonly ICollection<Programmer> _recommendedBys;
        private readonly IEnumerable<string> _skills;
        private readonly IRankCalculator _rankCalculator;
        private string Name => _name;

        public Programmer(string name, IEnumerable<string> skills, IRankCalculator rankCalculator)
        {
            _recommendations = new List<Programmer>();
            _recommendedBys = new List<Programmer>();
            _name = name;
            _skills = skills;
            _rankCalculator = rankCalculator;
        }

        public ProgrammerDto Details => new ProgrammerDto(Name, _rank, _recommendations.Select(programmer => programmer.Name), _skills);
        public bool IsNamed(string name) => Name.Equals(name);
        public IEnumerable<Programmer> RecommendedBys => _recommendedBys;
        public decimal ProgrammerRankShare => _rank / _recommendations.Count;

        public void Recommends(Programmer programmer)
        {
            _recommendations.Add(programmer);
            programmer.RecommendedBy(this);
        }

        public void UpdateRank()
        {
            _rank = _rankCalculator.CalculateRank(this);
        }

        public IEnumerable<IProgrammer> Relations()
        {
            return _recommendations.Concat(_recommendedBys);
        }

        private void RecommendedBy(Programmer programmer)
        {
            _recommendedBys.Add(programmer);
        }

        public bool HasSkill(string language)
        {
            return _skills.Any(skill => skill == language);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object that)
        {
            if (((Programmer)that).Name == this.Name)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
