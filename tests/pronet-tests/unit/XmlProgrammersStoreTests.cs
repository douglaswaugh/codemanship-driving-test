using NUnit.Framework;
using NSubstitute;
using System.Xml.Linq;

namespace ProNet.Test.Unit
{
    [TestFixture]
    public class XmlNetworkStoreTests
    {
        [Test]
        public void Rank_should_be_calculated_when_network_is_built()
        {
            var networkStore = new XmlNetworkStore(new HardCodedXmlLoader(), new NetworkFactory(new ProgrammerFactory()));
            var network = networkStore.GetNetwork();
            Assert.That(network.GetDetailsFor("Nick").Rank, Is.EqualTo(0.15m));
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

            var networkStore = new XmlNetworkStore(new HardCodedXmlLoader(), new NetworkFactory(new ProgrammerFactory()));
            var network = networkStore.GetNetwork();
            Assert.That(network.GetDetailsFor("Ed").Recommendations, Is.EquivalentTo(new string[] {"Liz", "Rick", "Bill"}));
        }
    }
}