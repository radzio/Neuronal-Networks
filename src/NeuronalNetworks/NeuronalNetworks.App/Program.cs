using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.App
{
    class Program
    {
        static void Main(string[] args)
        {

//            ActivationNetwork network = new ActivationNetwork(
//                    new ThresholdFunction(), // sigmoid activation function
//                    2,                      // 2 inputs
//                    1);                 // 1 layer with 2 nerons.
//
//            network[0][0].Threshold = -0.73;
//            network[0][0][0] = 0.57;
//            network[0][0][1] = 0.38;
//
//            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(Network));
//
//
//            TextWriter tw = new StreamWriter("network.xml");
//            
//            x.Serialize(tw, network);
//            tw.Close(); 

            XmlSerializer deserializer = new XmlSerializer(typeof(Network));
            TextReader textReader = new StreamReader(@"network.xml");

            var network2 = (Network)deserializer.Deserialize(textReader);
            textReader.Close();

            Console.WriteLine(network2.Compute(new double[] {0.1, 0.1})[0]);
            Console.WriteLine(network2.Compute(new double[] { 1, 1 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 0, 0 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 0, 1 })[0]);
            Console.WriteLine(network2.Compute(new double[] { 1, 0 })[0]);
            Console.Read();

        }
    }
}
