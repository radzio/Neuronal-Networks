using System;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Distance;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Networks;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Learning
{
    public class SOMLayerLearning : IUnsupervisedLearning
    {


        private KohonenLayer layer;
        // network's dimension
        private int width;
        private int height;

        // learning rate
        private double learningRate = 0.8;
        private CounterPropagationNetwork network;

        public Conscience Conscience { get; set; }


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


        public double ConscienceValue
        {
            get { return Conscience.MinimumPotential; }
            set
            {
                Conscience.MinimumPotential = value;
            }
        }



        public SOMLayerLearning(CounterPropagationNetwork network)
        {
            int neuronsCount = network[0].NeuronsCount;
            width = (int)Math.Sqrt(neuronsCount);

            this.Neighborhood = new TwoDimensionalNeighborhood(width);
            this.Conscience = new Conscience(neuronsCount, 0);

            

            if (width*width != neuronsCount)
            {
                throw new ArgumentException("Invalid network size");
            }

            // ok, we got it
            this.network = network;
            this.width = width;
            this.height = height;
        }


        public double Run(double[] input)
        {
            double error = 0.0;

            // compute the network
            network.Compute(input);
            int winner = network.GetWinner(Conscience);

            Conscience.UpdateConscience(winner);

            // get layer of the network
            Layer layer = network[0];

            // check learning radius
            if (LearningRadius == 0.0)
            {
                Neuron neuron = layer[winner];

                // update weight of the winner only
                for (int i = 0, n = neuron.InputsCount; i < n; i++)
                {
                    neuron[i] += (input[i] - neuron[i]) * learningRate;
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
                        double e = (input[i] - neuron[i]) * factor * learningRate;

                        error += Math.Abs(e);
                        // update weight
                        neuron[i] += e * learningRate;

                    }
                }
            }
            return error;
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