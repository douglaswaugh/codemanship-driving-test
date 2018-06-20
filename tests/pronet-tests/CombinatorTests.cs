using System.Collections.Generic;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class CombinatorTests
    {
        [Test]
        public void Should_return_empty_list_for_combinations_of_size_0()
        {
            var combinator = new Combinator();
            var programmers = new List<IProgrammer>();

            var combinations = combinator.CombinationsFor(programmers, 0);

            Assert.That(combinations, Is.EquivalentTo(new List<IProgrammer>()));
        }
    }
}