using NUnit.Framework;

namespace ProNet.Test.Unit
{
    [TestFixture]
    public class ProgrammerTests
    {
        [Test]
        public void Should_calculate_degrees_of_separation_between_developer_and_itself_is_0()
        {
            var programmer1 = new Programmer("Programemr1", new string[]{}, new DegreesOfSeparationNetwork());
            
            var degrees = programmer1.SeparatedByDegreesFrom(programmer1);

            Assert.That(degrees, Is.EqualTo(0));
        }

        [Test]
        public void Should_calculate_degrees_of_separation_between_developer_and_direct_relation_is_1()
        {
            var programmer1 = new Programmer("Programemr1", new string[]{}, new DegreesOfSeparationNetwork());
            var programmer2 = new Programmer("Programmer2", new string[]{}, new DegreesOfSeparationNetwork());
            programmer1.Recommends(programmer2);

            var degrees = programmer1.SeparatedByDegreesFrom(programmer2);

            Assert.That(degrees, Is.EqualTo(1));
        }
    }
}