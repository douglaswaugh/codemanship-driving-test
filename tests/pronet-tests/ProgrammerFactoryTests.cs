using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ProNet.Test.Unit
{
    [TestFixture]
    public class ProgrammerFactoryTests
    {
        [Test]
        public void Should_build_relations_correctly()
        {
            var programmerFactory = new ProgrammerFactory();
            var recommendations = new Dictionary<string, IEnumerable<string>>()
            {
                {"programmer1", new List<string> {"programmer2"}},
                {"programmer3", new List<string> {"programmer1"}}
            };
            var skills = new Dictionary<string, IEnumerable<string>>()
            {
                {"programmer1", new List<string>()},
                {"programmer2", new List<string>()},
                {"programmer3", new List<string>()}
            };
            var programmers = programmerFactory.BuildProgrammers(recommendations, skills);
            var programmer = programmers.Single(p => p.IsNamed("programmer1"));
            Assert.That(programmer.Relations, Is.EquivalentTo(new IProgrammer[] { new Programmer("programmer2", new string[]{}, new DegreesOfSeparationNetwork()), new Programmer("programmer3", new string[]{}, new DegreesOfSeparationNetwork())}));
        }
    }
}