using System;
using NUnit.Framework;
using NeuronalNetworks.Common;

namespace NeuronalNetwork.Tests
{
   
    [TestFixture]
    public class LogicFunctionTests
    {

        [Test]
        public void AndLogicFunction()
        {
            var network = NeuronalNetworkSerializer.DeserializeFromXml(Resources.and);
            Assert.That(network.Compute(new double[] { 1, 1 })[0] == 1);
            Assert.That(network.Compute(new double[] { 1, 0 })[0] == 0);
            Assert.That(network.Compute(new double[] { 0, 1 })[0] == 0);
            Assert.That(network.Compute(new double[] { 0, 0 })[0] == 0);
        }

        [Test]
        public void AndLogicFunctionWithSigmoid()
        {
            var network = NeuronalNetworkSerializer.DeserializeFromXml(Resources.and);
            var EPSILON = 0.1;
            Assert.That(Math.Abs(network.Compute(new double[] { 1, 1 })[0] - 1) < EPSILON);
            Assert.That(Math.Abs(network.Compute(new double[] { 1, 0 })[0] - 0) < EPSILON);
            Assert.That(Math.Abs(network.Compute(new double[] { 0, 1 })[0] - 0) < EPSILON);
            Assert.That(Math.Abs(network.Compute(new double[] { 0, 0 })[0] - 0) < EPSILON);
        }

        [Test]
        public void XorLogicFunction()
        {
            var network = NeuronalNetworkSerializer.DeserializeFromXml(Resources.xor);
            Assert.That(network.Compute(new double[] { 1, 1 })[0] == 0);
            Assert.That(network.Compute(new double[] { 1, 0 })[0] == 1);
            Assert.That(network.Compute(new double[] { 0, 1 })[0] == 1);
            Assert.That(network.Compute(new double[] { 0, 0 })[0] == 0);
        }

        //[TestCase(new double[] {1,1}, 1)]
//        [Test]
//        public void AndLogicFunction(double[] input, double result)
//        {
//            var network = NeuronalNetworkSerializer.DeserializeFromXml(Resources.and);
//            //todo testcases or :>? 
//            Assert.That(network.Compute(input)[0] == result);
//            //            Assert.That(network.Compute(new double[] { 1, 0 })[0] == 0);
//            //            Assert.That(network.Compute(new double[] { 0, 1 })[0] == 0);
//            //            Assert.That(network.Compute(new double[] { 0, 0 })[0] == 0);
//        }
    }
}
