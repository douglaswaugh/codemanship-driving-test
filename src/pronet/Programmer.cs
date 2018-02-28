using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Programmer : IProgrammer
    {
        private readonly string _name;
        private decimal _rank;
        private readonly ICollection<IProgrammer> _recommendations;
        private readonly ICollection<IProgrammer> _recommendedBys;
        private readonly IEnumerable<string> _skills;

        public Programmer(string name, IEnumerable<string> skills)
        {
            _recommendations = new List<IProgrammer>();
            _recommendedBys = new List<IProgrammer>();
            _name = name;
            _skills = skills;
        }

        public decimal Rank
        {
            get => _rank;
            set => _rank = value;
        }
        public string Name => _name;
        public IEnumerable<string> RecommendedProgrammers => _recommendations.Select(programmer => programmer.Name);
        public IEnumerable<string> Skills => _skills;
        public decimal ProgrammerRankShare => Rank / _recommendations.Count;
        public IEnumerable<IProgrammer> Relations => _recommendations.Concat(_recommendedBys);

        public void Recommends(IProgrammer programmer)
        {
            _recommendations.Add(programmer);
            programmer.RecommendedBy(this);
        }

        public void RecommendedBy(IProgrammer programmer)
        {
            _recommendedBys.Add(programmer);
        }

        public void UpdateRank()
        {
            // (1 - d) + d(PR(T1)/C(T1)) + ... + d(PR(Tn)/C(Tn))
            Rank = _recommendedBys
                .Aggregate(1m - 0.85m, (current, programmer) => current + 0.85m * programmer.ProgrammerRankShare);
        }

        private bool HasRecommended(IProgrammer programmer)
        {
            return _recommendations.Contains(programmer);
        }

        private bool WasRecommendedBy(IProgrammer programmer)
        {
            return _recommendations.Contains(programmer);
        }

        public bool IsRelatedTo(IProgrammer programmer)
        {
            return WasRecommendedBy(programmer) || HasRecommended(programmer);
        }

        public override string ToString()
        {
            return _name;
        }

        public override bool Equals(object that)
        {
            if (((IProgrammer)that).Name == this.Name)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
