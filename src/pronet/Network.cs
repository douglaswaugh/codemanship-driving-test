﻿using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Network : INetwork
    {
        public readonly IEnumerable<IProgrammer> _programmers;
        private DegreesOfSeparation _degreesOfSeparation;

        public Network(IEnumerable<IProgrammer> programmers)
        {
            _programmers = programmers;
            _degreesOfSeparation = new DegreesOfSeparation();
        }

        public void Calculate()
        {
            do
                UpdateRanks();
            while (1 - AverageRank() >= 0.000001m);
        }

        public decimal RankFor(string name)
        {
            return GetByName(name).Rank;
        }

        public ProgrammerDto GetDetailsFor(string name)
        {
            return GetByName(name).Details;
        }

        public int DegreesOfSeparation(string programmer1, string programmer2)
        {
            return _degreesOfSeparation.Calculate(GetByName(programmer1), GetByName(programmer2));
        }

        private IProgrammer GetByName(string name)
        {
            return _programmers.Single(programmer => programmer.Name == name);
        }

        private decimal AverageRank()
        {
            var totalRank = 0m;
            foreach (var programmer in _programmers)
            {
                totalRank += programmer.Details.Rank;
            }
            return totalRank / _programmers.Count();
        }

        private void UpdateRanks()
        {
            foreach (var programmer in _programmers)
            {
                programmer.UpdateRank();
            }
        }
    }
}