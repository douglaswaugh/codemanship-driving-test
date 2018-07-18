using System.Collections.Generic;
using NUnit.Framework;

namespace ProNet.Test
{
    [TestFixture]
    public class Combinator2Tests
    {
        private IProgrammer _programmer1;
        private IProgrammer _programmer2;
        private IProgrammer _programmer3;
        private IProgrammer _programmer4;
        private IProgrammer _programmer5;

        [SetUp]
        public void SetUp()
        {
            _programmer1 = new Programmer("programmer1", new List<string>(), new RankCalculator());
            _programmer2 = new Programmer("programmer2", new List<string>(), new RankCalculator());
            _programmer3 = new Programmer("programmer3", new List<string>(), new RankCalculator());
            _programmer4 = new Programmer("programmer4", new List<string>(), new RankCalculator());
            _programmer5 = new Programmer("programmer5", new List<string>(), new RankCalculator());
        }

        [Test]
        public void Should_return_empty_list_for_combinations_of_size_0()
        {
            var combinator = new Combinator2();
            var programmers = new List<IProgrammer>();

            var combinations = combinator.CombinationsFor(programmers, 0);

            Assert.That(combinations, Is.EquivalentTo(new List<IEnumerable<IProgrammer>>()));
        }

        [Test]
        public void Should_return_collection_of_size_1_for_combinations_of_size_1_from_collection_of_size_1()
        {
            var combinator = new Combinator2();
            var programmers = new List<IProgrammer> { _programmer1 };

            var combinations = combinator.CombinationsFor(programmers, 1);

            Assert.That(combinations, Is.EquivalentTo(new List<IEnumerable<IProgrammer>> { new List<IProgrammer> { _programmer1 }}));
        }

        [Test]
        public void Should_return_10_collections_of_size_3_from_collection_of_size_5()
        {
            var combinator = new Combinator2();
            var programmers = new List<IProgrammer>{_programmer1, _programmer2, _programmer3, _programmer4, _programmer5};

            var combinations = combinator.CombinationsFor(programmers, 3);

            var expected = new List<IEnumerable<IProgrammer>> {
                new List<IProgrammer>{_programmer1, _programmer2, _programmer3},
                new List<IProgrammer>{_programmer1, _programmer2, _programmer4},
                new List<IProgrammer>{_programmer1, _programmer2, _programmer5},
                new List<IProgrammer>{_programmer1, _programmer3, _programmer4},
                new List<IProgrammer>{_programmer1, _programmer3, _programmer5},
                new List<IProgrammer>{_programmer1, _programmer4, _programmer5},
                new List<IProgrammer>{_programmer2, _programmer3, _programmer4},
                new List<IProgrammer>{_programmer2, _programmer3, _programmer5},
                new List<IProgrammer>{_programmer2, _programmer4, _programmer5},
                new List<IProgrammer>{_programmer3, _programmer4, _programmer5},
            };

            Assert.That(combinations, Is.EquivalentTo(expected));
        }

        [Test]
        public void Should_remove_combination_with_duplicate_programmers()
        {
            var combinator = new Combinator2();

            var combinations = new List<ICollection<IProgrammer>> {
                new List<IProgrammer>{_programmer1, _programmer1},
            };

            var reducedCombinations = combinator.RemoveCombinationsWithDuplicateProgrammers(combinations);

            Assert.That(reducedCombinations, Is.EquivalentTo(new List<ICollection<IProgrammer>>()));
        }

        [Test]
        public void Should_not_remove_combination_without_duplicate_programmers()
        {
            var combinator = new Combinator2();

            var combinations = new List<ICollection<IProgrammer>> {
                new List<IProgrammer>{_programmer1, _programmer2}
            };

            var reducedCombinations = combinator.RemoveCombinationsWithDuplicateProgrammers(combinations);

            Assert.That(reducedCombinations, Is.EquivalentTo(combinations));
        }

        [Test]
        public void Should_order_elements_by_name()
        {
            var combinations = new List<ICollection<IProgrammer>> {
                new List<IProgrammer>{_programmer2, _programmer3, _programmer4, _programmer1}
            };

            var combinator = new Combinator2();

            var orderedCombinations = combinator.OrderCombinations(combinations);

            var expected = new List<ICollection<IProgrammer>> {
                new List<IProgrammer>{_programmer1, _programmer2, _programmer3, _programmer4}
            };

            Assert.That(orderedCombinations, Is.EquivalentTo(expected));
        }

        [Test]
        public void Should_add_set_to_initial_combinations()
        {
            var combinator = new Combinator2();

            var combinationsWithAddedSet = combinator.AddSetToCombinations(new List<ICollection<IProgrammer>> { new List<IProgrammer>() }, new List<IProgrammer>{_programmer1, _programmer2, _programmer3});

            var expected = new List<ICollection<IProgrammer>> {
                new List<IProgrammer> {_programmer1},
                new List<IProgrammer> {_programmer2},
                new List<IProgrammer> {_programmer3}
            };

            Assert.That(combinationsWithAddedSet, Is.EquivalentTo(expected));
        }

        [Test]
        public void Should_add_set_to_level_1_combinations()
        {
            var combinator = new Combinator2();

            var combinations = new List<ICollection<IProgrammer>> {
                new List<IProgrammer> { _programmer1 },
                new List<IProgrammer> { _programmer2 }
            };
            var programmers = new List<IProgrammer> { _programmer1, _programmer2 };

            var combinationsWithAddedSet = combinator.AddSetToCombinations(combinations, programmers);

            var expected = new List<ICollection<IProgrammer>> {
                new List<IProgrammer> { _programmer1, _programmer1 },
                new List<IProgrammer> { _programmer1, _programmer2 },
                new List<IProgrammer> { _programmer2, _programmer1 },
                new List<IProgrammer> { _programmer2, _programmer2 },
            };

            Assert.That(combinationsWithAddedSet, Is.EquivalentTo(expected));
        }

        [Test]
        public void Should_remove_duplicate_combinations_of_size_1()
        {
            var combinator = new Combinator2();
            var combinations = new List<ICollection<IProgrammer>>{
                new List<IProgrammer> {_programmer1},
                new List<IProgrammer> {_programmer1}
            };

            var uniqueCombinations = combinator.RemoveDuplicateCombinations(combinations);

            Assert.That(uniqueCombinations, Is.EquivalentTo(new List<ICollection<IProgrammer>>(){new List<IProgrammer> {_programmer1}}));
        }

        [Test]
        public void Should_remove_duplicate_combinations_of_size_2()
        {
            var combinator = new Combinator2();
            var combinations = new List<ICollection<IProgrammer>> {
                new List<IProgrammer> {_programmer1, _programmer2},
                new List<IProgrammer> {_programmer1, _programmer2}
            };

            var uniqueCombinations = combinator.RemoveDuplicateCombinations(combinations);

            Assert.That(uniqueCombinations, Is.EquivalentTo(new List<ICollection<IProgrammer>>(){new List<IProgrammer> {_programmer1, _programmer2}}));
        }

        [Test]
        public void Should_create_2_teams_with_each_member_as_leader_from_combination_of_size_2()
        {
            var combinations = new List<ICollection<IProgrammer>> {
                new List<IProgrammer> {_programmer1, _programmer2}
            };

            var combinator = new Combinator2();

            var teams = combinator.AddTeamsWithEachMemberAsLeader(combinations);

            var expected = new List<ICollection<IProgrammer>>(){
                new List<IProgrammer> {_programmer1, _programmer2},
                new List<IProgrammer> {_programmer2, _programmer1}};

            Assert.That(teams, Is.EquivalentTo(expected));
        }
    }
}