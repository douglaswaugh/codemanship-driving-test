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

        public Programmer(string name, IEnumerable<string> skills)
        {
            _recommendations = new List<Programmer>();
            _recommendedBys = new List<Programmer>();
            _name = name;
            _skills = skills;
        }

        public IEnumerable<IProgrammer> Relations => _recommendations.Concat(_recommendedBys);

        public ProgrammerDto Details => new ProgrammerDto(Name, _rank, _recommendations.Select(programmer => programmer.Name), _skills);

        public bool IsNamed(string name)
        {
            return Name.Equals(name);
        }

        public void Recommends(Programmer programmer)
        {
            _recommendations.Add(programmer);
            programmer.RecommendedBy(this);
        }

        public void UpdateRank()
        {
            // (1 - d) + d(PR(T1)/C(T1)) + ... + d(PR(Tn)/C(Tn))
            _rank = _recommendedBys
                .Aggregate(1m - 0.85m, (current, programmer) => current + 0.85m * programmer.ProgrammerRankShare);
        }

        private string Name => _name;

        private decimal ProgrammerRankShare => _rank / _recommendations.Count;

        private void RecommendedBy(Programmer programmer)
        {
            _recommendedBys.Add(programmer);
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
