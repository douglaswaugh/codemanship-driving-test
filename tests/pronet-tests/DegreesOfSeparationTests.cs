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
            var programmer1 = new Programmer("Programemr1", new string[]{});
            
            var degrees = degreesOfSeparation.Calculate(programmer1, programmer1);

            Assert.That(degrees, Is.EqualTo(0));
        }
    }
}