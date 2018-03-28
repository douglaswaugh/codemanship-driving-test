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
        private readonly DegreesOfSeparation _degreesOfSeparation;

        public Programmer(string name, IEnumerable<string> skills, DegreesOfSeparation degreesOfSeparation)
        {
            _recommendations = new List<Programmer>();
            _recommendedBys = new List<Programmer>();
            _name = name;
            _skills = skills;
            _degreesOfSeparation = degreesOfSeparation;
        }

        public ProgrammerDto Details => new ProgrammerDto(Name, _rank, _recommendations.Select(programmer => programmer.Name), _skills);
        public bool IsNamed(string name) => Name.Equals(name);
        private string Name => _name;

        public void Recommends(Programmer programmer)
        {
            _recommendations.Add(programmer);
            programmer.RecommendedBy(this);
        }

        private void RecommendedBy(Programmer programmer)
        {
            _recommendedBys.Add(programmer);
        }

        public void UpdateRank()
        {
            // (1 - d) + d(PR(T1)/C(T1)) + ... + d(PR(Tn)/C(Tn))
            _rank = _recommendedBys
                .Aggregate(1m - 0.85m, (current, programmer) => current + 0.85m * programmer.ProgrammerRankShare);
        }

        private decimal ProgrammerRankShare => _rank / _recommendations.Count;

        public int SeparatedByDegreesFrom(IProgrammer programmerTo)
        {
            var network = _degreesOfSeparation.BuildNetwork(this);

            return network.Single(tuple => tuple.Item2.Equals(programmerTo)).Item1;
        }

        public void AddRelationsTo(Queue<Tuple<int, IProgrammer>> queue, int degreeOfSeparation)
        {
            foreach (var relation in _recommendations.Concat(_recommendedBys))
            {
                queue.Enqueue(new Tuple<int, IProgrammer>(degreeOfSeparation, relation));
            }
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
