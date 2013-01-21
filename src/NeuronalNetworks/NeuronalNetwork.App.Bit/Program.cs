using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Back;

namespace NeuronalNetwork.App.Bit
{
    public class Program
    {

        public static double[][] XORInput = {
                                                new[] {0.0, 0.0, 0.0},
                                                new[] {0.0, 0.0, 1.0},
                                                new[] {0.0, 1.0, 0.0},
                                                new[] {1.0, 0.0, 0.0},

                                                new[] {0.0, 1.0, 1.0},
                                                new[] {1.0, 1.0, 0.0},
                                                new[] {1.0, 1.0, 1.0},
                                                new[] {1.0, 0.0, 1.0},
                                            };


        public static double[][] XORIdeal = {
                                                new[] {0.0},
                                                new[] {1.0},
                                                new[] {1.0},
                                                new[] {1.0},

                                                new[] {0.0},
                                                new[] {0.0},
                                                new[] {1.0},
                                                new[] {0.0},
                                            };

        private static void Main(string[] args)
        {
            // create a neural network, without using a factory
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, 3));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 3));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, 1));
            network.Structure.FinalizeStructure();
            network.Reset();

            // create training data
            IMLDataSet trainingSet = new BasicMLDataSet(XORInput, XORIdeal);

            // train the neural network
            IMLTrain train = new Backpropagation(network, trainingSet, 0.2, 0.5);

            int epoch = 1;

            do
            {
                train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Error:" + train.Error);
                epoch++;
            } while (train.Error > 0.01);

            // test the neural network

            Console.WriteLine(@"Neural Network Results:");
            foreach (IMLDataPair pair in trainingSet)
            {
                IMLData output = network.Compute(pair.Input);
                Console.WriteLine(pair.Input[0] + @"," + pair.Input[1]
                                  + @", actual=" + output[0] + @",ideal=" + pair.Ideal[0]);
            }

            Console.Read();
        }
    }
}
