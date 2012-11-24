using NUnit.Framework;
using NeuronalNetworks.Common;
using NeuronalNetworks.Networks;

namespace NeuronalNetwork.Tests
{
    [TestFixture]
    public class KohonenTest
    {


        [Test]
        [TestCase(new double[] { 0, 0, 1, 0, 0, 1, 0, 0, 1 }, 0)]
        [TestCase(new double[] { 0, 1, 0, 1, 1, 1, 0, 1, 0 }, 1)]
        [TestCase(new double[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 }, 2)]
        [TestCase(new double[] { 1, 1, 1, 1, 0, 1, 1, 1, 1 }, 3)]
        public void KohonenIconsDefault(double[] input, int result)
        {
            var network = (KohonenNetwork) NeuronalNetworkSerializer.DeserializeFromXml(Resources.kohonen_ikonki);
            network.Compute(input);
            Assert.That(network.GetWinner() == result);

        }

        
    }
}