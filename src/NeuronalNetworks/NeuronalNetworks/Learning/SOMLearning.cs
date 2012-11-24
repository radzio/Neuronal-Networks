using System;
using System.Collections.Generic;
using System.Linq;
using NeuronalNetworks.Distance;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Networks;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Learning
{
    public class SOMLearning : IUnsupervisedLearning
    {
        // neural network to train
        private KohonenNetwork network;
        // network's dimension
        private int width;
        private int height;

        // learning rate
        private double learningRate = 0.8;



        public double LearningRate
        {
            get { return learningRate; }
            set { learningRate = Math.Max(0.0, Math.Min(1.0, value)); }
        }

        public Neighborhood Neighborhood { get; set; }

        public double LearningRadius
        {
            get { return Neighborhood.LearningRadius; }
            set
            {
                Neighborhood.LearningRadius = value;
            }
        }


        public SOMLearning(KohonenNetwork network)
        {
            // network's dimension was not specified, let's try to guess

            this.Neighborhood = new TwoDimensionalNeighborhood(3, 3);

            int neuronsCount = network[0].NeuronsCount;
            width = (int) Math.Sqrt(neuronsCount);

            if (width*width != neuronsCount)
            {
                throw new ArgumentException("Invalid network size");
            }

            // ok, we got it
            this.network = network;
            this.width = width;
            this.height = height;
        }



        public SOMLearning(KohonenNetwork network, int width, int height)
        {
            // check network size
            if (network[0].NeuronsCount != width*height)
            {
                throw new ArgumentException("Invalid network size");
            }

            this.network = network;
            this.width = width;
            this.height = height;
        }

        public double Run(double[] input)
        {
            double error = 0.0;

            // compute the network
            network.Compute(input);
            int winner = network.GetWinner();

            // get layer of the network
            Layer layer = network[0];

            // check learning radius
            if (LearningRadius == 0.0)
            {
                Neuron neuron = layer[winner];

                // update weight of the winner only
                for (int i = 0, n = neuron.InputsCount; i < n; i++)
                {
                    neuron[i] += (input[i] - neuron[i])*learningRate;
                }
            }
            else
            {
                

                // walk through all neurons of the layer
                for (int j = 0, m = layer.NeuronsCount; j < m; j++)
                {
                    Neuron neuron = layer[j];

                    var factor = Neighborhood.GetFactor(winner, j);
                    // update weight of the neuron
                    for (int i = 0, n = neuron.InputsCount; i < n; i++)
                    {
                        // calculate the error
                        double e = (input[i] - neuron[i])*factor;

                        error += Math.Abs(e);
                        // update weight
                        neuron[i] += e*learningRate;

                    }
                }
            }
            return error;
        }




        private Double etaFunction(int steps, int step)
        {
            double eta = 1.0;
            double a = (0.001 - eta)/(steps - 1);
            double b = eta - a;

            return a*(step + 1) + b;
        }


        public double RunEpoch(double[][] input)
        {
            double error = 0.0;


            foreach (double[] sample in input)
            {
                error += Run(sample);
            }


            return error;
        }



        
    }
}
