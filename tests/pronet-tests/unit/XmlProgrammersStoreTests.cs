using NUnit.Framework;
using NSubstitute;
using System.Xml.Linq;

namespace ProNet.Test.Unit
{
    [TestFixture]
    public class XmlProgrammersStoreTests
    {
        [Test]
        public void Programmer_should_have_a_rank_of_zero_when_first_built()
        {
            var programmersStore = new XmlProgrammersStore(new HardCodedXmlLoader(), new ProgrammersFactory(new ProgrammerFactory()));
            var programmers = programmersStore.GetAll();
            Assert.That(programmers.RankFor("Nick"), Is.EqualTo(0));
        }

        [Test]
        public void Should_build_programmers_with_correct_recommendations()
        {
            var xmlLoader = Substitute.For<IXmlLoader>();
            xmlLoader
                .Load()
                .Returns(XElement.Parse(@"
                    <?xml version=""1.0"" encoding=""utf-8"" ?>
                    <Network>
                        <Programmer name='Nick'></Programmer>
                        <Programmer name='Bill'></Programmer>
                        <Programmer name='Dave'></Programmer>
                        <Programmer name='Ed'>
                            <Recommendations>
                                <Recommendation>Liz</Recommendation>
                                <Recommendation>Rick</Recommendation>
                                <Recommendation>Bill</Recommendation>
                            </Recommendations>
                        </Programmer>
                        <Programmer name='Liz'></Programmer>
                        <Programmer name='Rick'></Programmer>
                    </Network>".Trim()));

            var programmersStore = new XmlProgrammersStore(new HardCodedXmlLoader(), new ProgrammersFactory(new ProgrammerFactory()));
            var programmers = programmersStore.GetAll();
            Assert.That(programmers.RecommendationsFor("Ed"), Is.EquivalentTo(new string[] {"Liz", "Rick", "Bill"}));
        }
    }
}