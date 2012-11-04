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
                    new ThresholdFunction(), // threshold activation function
                    2,                      // 2 inputs
                    2,
                    1);                 // 1 layer with 1 neuron.

            network[0][0].Threshold = 3.9;
            network[0][0].Bias = 0;

            network[0][1].Threshold = 6.86;
            network[0][1].Bias = 0;

            network[1][0].Threshold = 4.33;
            network[1][0].Bias = 0;

            network[0][0][0] = 6.54;
            network[0][0][1] = 6.51;

            network[0][1][0] = 4.47;
            network[0][1][1] = 4.46;

            network[1][0][0] = 9.45;
            network[1][0][1] = -10.22;

            NeuronalNetworkSerializer.SerializeToXml(network, @"network.xml");



            var network2 = NeuronalNetworkSerializer.DeserializeFromXmlFile(@"network.xml");


            Console.WriteLine(network2.Compute(new double[] {0.1, 0.1})[0]);
            Console.WriteLine(network2.Compute(new double[] { 1, 1 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 0, 0 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 0, 1 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 1, 0 })[0]);
            Console.Read();

        }
    }
}
