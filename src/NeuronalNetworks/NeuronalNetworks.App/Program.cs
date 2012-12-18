using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Common;
using NeuronalNetworks.Distance;
using NeuronalNetworks.Learning;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.App
{
    class Program
    {
        static void Main(string[] args)
        {

//            var network = new ActivationNetwork(
//                    new SigmoidFunction(), // threshold activation function
//                    2,                      // 2 inputs
//                    2,
//                    1);                 // 1 layer with 1 neuron.
//

            //var net = new ActivationNetwork(new SigmoidFunction(2), 2, new int[1] {1});

//            network[0][0].Threshold = 3.9;
//            network[0][0].Bias = 0;
//
//            network[0][1].Threshold = 6.86;
//            network[0][1].Bias = 0;
//
//            network[1][0].Threshold = 4.33;
//            network[1][0].Bias = 0;
//
//            network[0][0][0] = 6.54;
//            network[0][0][1] = 6.51;
//
//            network[0][1][0] = 4.47;
//            network[0][1][1] = 4.46;
//
//            network[1][0][0] = 9.45;
//            network[1][0][1] = -10.22;
//
//            NeuronalNetworkSerializer.SerializeToXml(network, @"network.xml");



//            var network2 = NeuronalNetworkSerializer.DeserializeFromXmlFile(@"E:\Users\radoslaw.piekarz\Documents\GitHub\Neuronal-Networks\src\NeuronalNetworks\NeuronalNetworks.Tests\Resources\and.xml");
//
//
//           
//
//
//            //var network2 = NeuronalNetworkSerializer.DeserializeFromXmlFile(@"network.xml");
//
//
//            Console.WriteLine(network2.Compute(new double[] {0.5, 0.5})[0]);
//            Console.WriteLine(network2.Compute(new double[] { 1, 1 })[0]);
//            Console.WriteLine(network2.Compute(new double[] { 0, 0 })[0]);
//            Console.WriteLine(network2.Compute(new double[] { 0, 1 })[0]);
//            Console.WriteLine(network2.Compute(new double[] { 1, 0 })[0]);
//            Console.Read();

//            KohonenNetwork network = new KohonenNetwork(9, 2 * 2);
//            // create learning algorithm
//            SOMLearning trainer = new SOMLearning(network);
//
//            double[][] inputs = new double[4][];
//
//            for (int i = 0; i < inputs.Length; i++)
//            {
//                inputs[i] = new double[9];
//            }
//
//
//            inputs[0][0] = 0;
//            inputs[0][1] = 0;
//            inputs[0][2] = 1;
//            inputs[0][3] = 0;
//            inputs[0][4] = 0;
//            inputs[0][5] = 1;
//            inputs[0][6] = 0;
//            inputs[0][7] = 0;
//            inputs[0][8] = 1;
//
//
//            inputs[1][0] = 0;
//            inputs[1][1] = 1;
//            inputs[1][2] = 0;
//            inputs[1][3] = 1;
//            inputs[1][4] = 1;
//            inputs[1][5] = 1;
//            inputs[1][6] = 0;
//            inputs[1][7] = 1;
//            inputs[1][8] = 0;
//
//            inputs[2][0] = 1;
//            inputs[2][1] = 1;
//            inputs[2][2] = 1;
//            inputs[2][3] = 1;
//            inputs[2][4] = 0;
//            inputs[2][5] = 1;
//            inputs[2][6] = 1;
//            inputs[2][7] = 1;
//            inputs[2][8] = 1;
//
//            inputs[3][0] = 1;
//            inputs[3][1] = 0;
//            inputs[3][2] = 0;
//            inputs[3][3] = 0;
//            inputs[3][4] = 1;
//            inputs[3][5] = 0;
//            inputs[3][6] = 0;
//            inputs[3][7] = 0;
//            inputs[3][8] = 1;
//
//
//            network.Randomize();
//            network.Randomize();
//            //trainer.ConscienceValue = 0.3;
//            for (int i = 0; i < 32000; i++ )
//            {
//                if (i == 0)
//                    trainer.LearningRadius = 1.0;
//
//                if (i == 8000)
//                    trainer.LearningRadius = 0.5;
//
//                if (i == 16000)
//                    trainer.LearningRadius = 0.25;
//
//                if (i == 24000)
//                    trainer.LearningRadius = 0.0;
//
//                foreach (var input in inputs)
//                                {
//                    
//                        trainer.Run(input);
//                
//                                }
//            }
//
//
//                //            trainer.LearningRate = 0.1;
//                //            trainer.LearningRadius = 2;
//                //
//                //            var c = new Conscience(4, 1);
//                //
//                //            var n = new TwoDimensionalNeighborhood(6, 3);
//                //            for (int i = 0; i < 8000; i++)
//                //            {
//                //                foreach (var input in inputs)
//                //                {
//                //                    trainer.Run2(input, i, 32000, c);
//                //                }
//                //            }
//                //
//                //            trainer.LearningRate = 0.2;
//                //            trainer.LearningRadius = 1;
//                //            for (int i = 8000; i < 16000; i++)
//                //            {
//                //                foreach (var input in inputs)
//                //                {
//                //                    trainer.Run2(input, i, 32000, c);
//                //                }
//                //            }
//                //
//                //            trainer.LearningRate = 0.1;
//                //            trainer.LearningRadius = 0.1;
//                //            for (int i = 16000; i < 32000; i++)
//                //            {
//                //                foreach (var input in inputs)
//                //                {
//                //                    trainer.Run2(input, i, 32000, c);
//                //                }
//                //            }
//
//
//
//
//
//
//
//
//                //            network.Layers[0].Neurons[0].Weights = new double[] { 0.0, 0.0, 0.5773, 0.0, 0.0, 0.5773, 0.0, 0.0, 0.5773};
//                //            network.Layers[0].Neurons[1].Weights = new double[] { 0.0, 0.4472, 0.0, 0.4472, 0.4472, 0.4472, 0.0, 0.4472, 0.0 };
//                //            network.Layers[0].Neurons[2].Weights = new double[] { 0.5773, 0.0, 0.0, 0.0, 0.5773, 0.0, 0.0, 0.0, 0.5773 };
//                //            network.Layers[0].Neurons[3].Weights = new double[] { 0.3535, 0.3535, 0.3535, 0.3535, 0.0, 0.3535, 0.3535, 0.3535, 0.3535 };
//
//
//                foreach (var input in inputs)
//                {
//                    network.Compute(input);
//                    Console.WriteLine("Winner " + network.GetWinner());
//                    foreach (var d in network.Output)
//                    {
//                        Console.WriteLine(d);
//                    }
//                    Console.WriteLine("--------");
//                }
//
//            Console.WriteLine("--------");
//
//            foreach (var neuron in network.Layers[0].Neurons)
//            {
//                foreach (var input in neuron.Weights)
//                {
//                    Console.WriteLine("W " + input);
//                }
//
//                Console.WriteLine(" - ");
//            }


            double[][] inputs = new double[7][]
                                    {
                                        new double[] {0, 0, 0},
                                        new double[] {0, 0, 1},
                                        new double[] {0, 1, 0},
                                        new double[] {0, 1, 1},
                                        new double[] {1, 0, 0},
//                                        new double[] {1, 0, 1},
                                        new double[] {1, 1, 0},
                                        new double[] {1, 1, 1},
                                    };


            double[][] outputs = new double[7][] {
                                                new double[]{0},
                                                new double[]{1},
                                                new double[]{1},
                                                new double[]{0},
                                                new double[]{1},
//                                                new double[]{0},
                                                new double[]{0},
                                                new double[]{1}
            };


            double[][] tests = new double[8][]
                                   {
                                       new double[] {0, 0, 0},
                                       new double[] {0, 0, 1},
                                       new double[] {0, 1, 0},
                                       new double[] {0, 1, 1},
                                       new double[] {1, 0, 0},
                                       new double[] {1, 0, 1},
                                       new double[] {1, 1, 0},
                                       new double[] {1, 1, 1},
                                   };



            double[][] testsResults = new double[8][]
                                   {
                                       new double[] {0, 0, 0},
                                       new double[] {0, 0, 1},
                                       new double[] {0, 1, 0},
                                       new double[] {0, 1, 1},
                                       new double[] {1, 0, 0},
                                       new double[] {1, 0, 1},
                                       new double[] {1, 1, 0},
                                       new double[] {1, 1, 1},
                                   };





            CounterPropagationNetwork network =
                (CounterPropagationNetwork) NeuronalNetworkSerializer.DeserializeFromXmlFile(
                    @"D:\Projects\neuronalnetworks\src\NeuronalNetworks\NeuronalNetworks.Tests\Resources\cpnetwork_good.xml");
            //network.Randomize(new DoubleRange(0.0, 1.0));
            NeuronalNetworkSerializer.SerializeToXml(network, "aaa.xml");
            var cpLearning = new CounterPropagationLearning(network);
            cpLearning.LearningRadius = 0.1;

            cpLearning.ConscienceValue = 0.0;

            cpLearning.LearningRate = 0.15;

            cpLearning.DeltaRule = false;


           

            for (int i = 0; i < 3000; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    var input = inputs[j];
                    var output = outputs[j];
                    cpLearning.Run(input, output);
                }

            }

            cpLearning.LearningRate = 0.10;
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    var input = inputs[j];
                    var output = outputs[j];
                    cpLearning.Run(input, output);
                }

            }


            cpLearning.LearningRate = 0.05;
            for (int i = 0; i < 15000; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    var input = inputs[j];
                    var output = outputs[j];
                    cpLearning.Run(input, output);
                }

            }



            var ii = 0;
            foreach (var input in inputs)
            {
                network.Compute(input);
                Console.WriteLine(string.Format("{0} - {1}", outputs[ii][0], network.GrossbergLayer.Output[0]));
                ii++;
            }

            Console.WriteLine("-------------");

            ii = 0;
            foreach (var input in tests)
            {
                network.Compute(input);
                Console.WriteLine(string.Format("{0} - {1}", testsResults[ii][0], network.GrossbergLayer.Output[0]));
                ii++;
            }

            Console.Read();

        }
    }
}
