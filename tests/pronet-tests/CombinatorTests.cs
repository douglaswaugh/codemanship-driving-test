using System.Collections.Generic;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class CombinatorTests
    {
        private IProgrammer _programmer1;

        [SetUp]
        public void SetUp()
        {
            _programmer1 = new Programmer("programmer1", new List<string>(), new RankCalculator());
        }

        [Test]
        public void Should_return_empty_list_for_combinations_of_size_0()
        {
            var combinator = new Combinator();
            var programmers = new List<IProgrammer>();

            var combinations = combinator.CombinationsFor(programmers, 0);

            Assert.That(combinations, Is.EquivalentTo(new List<IProgrammer>()));
        }

        [Test]
        public void Should_return_collection_of_size_1_for_combinations_of_size_1_from_collection_of_size_1()
        {
            var combinator = new Combinator();
            var programmers = new List<IProgrammer> { _programmer1 };

            var combinations = combinator.CombinationsFor(programmers, 1);

            Assert.That(combinations, Is.EquivalentTo(new List<IProgrammer> { _programmer1 }));
        }
    }
}