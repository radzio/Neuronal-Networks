using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Common;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var network = new ActivationNetwork(
                    new ThresholdFunction(), // sigmoid activation function
                    2,                      // 2 inputs
                    1);                 // 1 layer with 1 neuron.

            network[0][0].Threshold = -0.73;
            network[0][0][0] = 0.57;
            network[0][0][1] = 0.38;

            NeuronalNetworkSerializer.SerializeToXml(network, @"network.xml");



            var network2 = NeuronalNetworkSerializer.DeserializeFromXml(@"network.xml");


            Console.WriteLine(network2.Compute(new double[] {0.1, 0.1})[0]);
            Console.WriteLine(network2.Compute(new double[] { 1, 1 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 0, 0 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 0, 1 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 1, 0 })[0]);
            Console.Read();

        }
    }
}
