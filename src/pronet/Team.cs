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

        public decimal Strength => (1m / _members.Count()) * _members.Sum(member => MemberStrength(member));

        private decimal MemberStrength(IProgrammer member)
        {
            var rank = member.Details.Rank;
            var skillIndex = Array.IndexOf(member.Details.Skills.ToArray(), _language) + 1;
            var leader = _members.First();
            var degreesOfSeparation = leader.Equals(member) ? 1 : _degreesOfSeparation.Between(leader, member);

            return rank / (skillIndex * degreesOfSeparation);
        }
    }
}