using NUnit.Framework;

namespace ProNet.Test.Unit
{
    [TestFixture]
    public class DegreesOfSeparationTests
    {
        [Test]
        public void Should_calculate_degrees_of_separation_between_developer_and_itself_is_0()
        {
            var degreesOfSeparation = new DegreesOfSeparation();
            var programmer1 = new Programmer("Programemr1", new string[]{}, new DegreesOfSeparationNetwork());
            
            var degrees = degreesOfSeparation.Calculate(programmer1, programmer1);

            Assert.That(degrees, Is.EqualTo(0));
        }

        [Test]
        public void Should_calculate_degrees_of_separation_between_developer_and_direct_relation_is_1()
        {
            var degreesOfSeparation = new DegreesOfSeparation();
            var programmer1 = new Programmer("Programemr1", new string[]{}, new DegreesOfSeparationNetwork());
            var programmer2 = new Programmer("Programmer2", new string[]{}, new DegreesOfSeparationNetwork());
            programmer1.Recommends(programmer2);

            var degrees = degreesOfSeparation.Calculate(programmer1, programmer2);

            Assert.That(degrees, Is.EqualTo(1));
        }
    }
}