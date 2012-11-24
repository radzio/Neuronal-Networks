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
        // learning radius
        private double learningRadius = 4;

        private double squaredRadius2 = 2*4*4;


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
            if (learningRadius == 0)
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

        private int GetWinner(Conscience c)
        {
            int min = network.GetWinner();

            while (!c.CanParticipate(min))
            {
                network.Output[min] = double.MaxValue;
                min = Array.IndexOf(network.Output, network.Output.Min());
            }
            c.UpdateConscience(min);
            return min;
        }

    public void  Run2(double[] input, int step, int steps, Conscience c)
        {
            network.Compute(input);
            //List<Double> distances = calculateDistance(new List<double>(input));

            //var min = GetWinner(c);
        int winner = network.GetWinner();
            double winnderVal = double.MaxValue;
            var neurons = network.Layers[0].Neurons;
        KohonenNeuron bestNeuron = (KohonenNeuron) neurons[winner];
            for (int n = 0; n < neurons.Length; n++ )
            {
                KohonenNeuron neuron = (KohonenNeuron) neurons[n];
                
                //Console.WriteLine(neuron.Conscience);
                neuron.IncreaseConscience();
                if (neuron.CanCompete())
                {
                    if(network.Output[n] < winnderVal)
                    {
                        winnderVal = network.Output[n];
                        winner = n;
                        bestNeuron = (KohonenNeuron) neurons[n];
                    }
                }

            }



            
            bestNeuron.DecreaseConscience();
        var min = winner;

                // modify weight of winning neuron and all neighbours
                for (int i = 0; i < network.Layers[0].Neurons.Length; i++)
                {
                    if (i == min)
                    {
                        Neuron neuron = network.Layers[0][min];

                        for (int j = 0, n = neuron.InputsCount; j < n; j++)
                        {
                            neuron[j] += (input[j] - neuron[j]) * learningRate;

                        }
                    }
                    else
                    {

                        var nDistance = Neighborhood.GetDistance(min, i);
                        var nFunction = neighbourFunction(steps, step, min);
                        if (step > 5000)
                            //Console.WriteLine(string.Format("{0} - {1}", nDistance, nFunction));
                        if (nFunction > nDistance)
                        {
                            double[] neuronConnections = this.network.Layers[0].Neurons[i].Weights;

                            for (int j = 0; j < neuronConnections.Length; j++)
                            {


                                //                            double newWeight = neuronConnections[j] + etaFunction(steps, step)
                                //                                   * (input[j] - neuronConnections[j]);
                                //
                                //                            this.network.Layers[0].Neurons[i].Weights[j] = newWeight;

                                this.network.Layers[0].Neurons[i].Weights[j] += (input[j] - this.network.Layers[0].Neurons[i].Weights[j]) * learningRate / 3;

                            }
                        }

                    }
                }
        }


        private double neighbourFunction(int steps, int step, int neuronNr)
        {
            double a;
            double b;

           
                b = Math.Sqrt(network.Layers[0].Neurons[neuronNr].Weights.Length);
                a = -3.0 * b / steps;

            var res = a*step + b;
            return res ;
        }


        public double Run(double[] input, double speed, Neighborhood n, Conscience c)
        {
            int winner = -1;
            double minDistance = double.MaxValue;
            var neurons = network.Layers[0].Neurons;
            for(int i =0; i < network.Layers[0].Neurons.Length; i++)
            {
                if (!c.CanParticipate(i))
                    continue;

                double distance = EuclideanDistance.Distance(new List<double>(input), new List<double>(neurons[i].Weights));

                if (distance < minDistance)
                {
                    minDistance = distance;
                    winner = i;
                }

            }

            c.UpdateConscience(winner);

            for (int i = 0; i < neurons.Length; i++)
            {
                double change = speed * n.GetDistance(i, winner);

                if (change > 0.0)
                {
                    var neuron = neurons[i];
                    for (int j = 0; j < neuron.Weights.Length; j++)
                    {
                        neuron[i] = neuron[i] + change * (input[i] - neuron[i]);
                    }
                }
            }
            return 0;

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


        public double RunEpoch(double[][] input, double speed, Neighborhood n, Conscience c)
        {
            double error = 0.0;

            foreach (double[] sample in input)
            {
                error += Run(sample, speed, n, c);
            }

            return error;
        }


        
    }
}
