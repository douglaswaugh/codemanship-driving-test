using NUnit.Framework;

namespace ProNet.Test.Unit
{
    [TestFixture]
    public class ProgrammerTests
    {
        [Test]
        public void Should_calculate_degrees_of_separation_between_developer_and_itself_is_0()
        {
            var programmer1 = new Programmer("Programemr1", new string[]{}, null);
            
            var degreesOfSeparation = new DegreesOfSeparation(new[] {programmer1});

            var degrees = degreesOfSeparation.Between(programmer1, programmer1);

            Assert.That(degrees, Is.EqualTo(0));
        }

        [Test]
        public void Should_calculate_degrees_of_separation_between_developer_and_direct_relation_is_1()
        {
            var programmer1 = new Programmer("Programemr1", new string[]{}, null);
            var programmer2 = new Programmer("Programmer2", new string[]{}, null);
            programmer1.Recommends(programmer2);

            var degreesOfSeparation = new DegreesOfSeparation(new[] {programmer1, programmer2});

            var degrees = degreesOfSeparation.Between(programmer1, programmer2);

            Assert.That(degrees, Is.EqualTo(1));
        }

        [Test]
        public void Should_state_programmer_has_skill_when_programmer_has_skill()
        {
            var programmer = new Programmer("Programmer1", new string[]{ "skill" }, null);

            Assert.That(programmer.HasSkill("skill"), Is.True);
        }

        [Test]
        public void Should_state_programmer_does_not_have_skill_when_programmer_does_not_have_skill()
        {
            var programmer = new Programmer("Programmer1", new string[] { "skill" }, null);

            Assert.That(programmer.HasSkill("missing skill"), Is.False);
        }
    }
}