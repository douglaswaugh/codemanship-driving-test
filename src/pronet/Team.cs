using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class Team
    {
        private readonly string _language;
        private readonly IEnumerable<IProgrammer> _members;
        private readonly DegreesOfSeparation _degreesOfSeparation;

        public Team(string language, IEnumerable<IProgrammer> members, DegreesOfSeparation degreesOfSeparation)
        {
            _language = language;
            _members = members;
            _degreesOfSeparation = degreesOfSeparation;
        }

        public decimal Strength =>
            ((decimal)1 / (decimal)_members.Count()) *
            _members
                .Select(member => MemberStrength(member))
                .Sum();

        private decimal MemberStrength(IProgrammer member)
        {
            var rankSkillDegree = new Tuple<decimal, int, int>(
                member.Details.Rank,
                Array.IndexOf(member.Details.Skills.ToArray(), _language) + 1,
                _members.First().Equals(member) ? 1 : _degreesOfSeparation.Between(_members.First(), member));

            return rankSkillDegree.Item1 / (rankSkillDegree.Item2 * rankSkillDegree.Item3);
        }
    }
}