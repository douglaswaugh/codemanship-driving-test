using System.Collections.Generic;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class CombinatorTests
    {
        private IProgrammer _programmer1;
        private IProgrammer _programmer2;

        [SetUp]
        public void SetUp()
        {
            _programmer1 = new Programmer("programmer1", new List<string>(), new RankCalculator());
            _programmer2 = new Programmer("programmer2", new List<string>(), new RankCalculator());
        }

        [Test]
        public void Should_return_empty_list_for_combinations_of_size_0()
        {
            var combinator = new Combinator();
            var programmers = new List<IProgrammer>();

            var combinations = combinator.CombinationsFor(programmers, 0);

            Assert.That(combinations, Is.EquivalentTo(new List<IEnumerable<IProgrammer>>()));
        }

        [Test]
        public void Should_return_collection_of_size_1_for_combinations_of_size_1_from_collection_of_size_1()
        {
            var combinator = new Combinator();
            var programmers = new List<IProgrammer> { _programmer1 };

            var combinations = combinator.CombinationsFor(programmers, 1);

            Assert.That(combinations, Is.EquivalentTo(new List<IEnumerable<IProgrammer>> { new List<IProgrammer> { _programmer1 }}));
        }

        [Test]
        public void Should_return_2_collections_of_size_1_for_combinations_of_size_1_from_collection_of_size_2()
        {
            var combinator = new Combinator();
            var programmers = new List<IProgrammer> { _programmer1, _programmer2 };

            var combinations = combinator.CombinationsFor(programmers, 1);

            Assert.That(combinations, Is.EquivalentTo(new List<IEnumerable<IProgrammer>> { new List<IProgrammer> { _programmer1 }, new List<IProgrammer> { _programmer2 }}));
        }
    }
}