using System;
using System.Collections.Generic;
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
            get { return learningRadius; }
            set
            {
                learningRadius = Math.Max(0, value);
                squaredRadius2 = 2*learningRadius*learningRadius;
            }
        }

       
        public SOMLearning(KohonenNetwork network)
        {
            // network's dimension was not specified, let's try to guess

            this.Neighborhood = new TwoDimensionalNeighborhood(3,9);

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
                // winner's X and Y
                int wx = winner%width;
                int wy = winner/width;

                // walk through all neurons of the layer
                for (int j = 0, m = layer.NeuronsCount; j < m; j++)
                {
                    Neuron neuron = layer[j];

                    int dx = (j%width) - wx;
                    int dy = (j/width) - wy;

                    // update factor ( Gaussian based )
                    double factor = Math.Exp(-(double) (dx*dx + dy*dy)/squaredRadius2);

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
            double a = (0.001 - eta) / (steps - 1);
            double b = eta - a;

            return a * (step + 1) + b;
        }

        public void  Run2(double[] input, int step, int steps)
        {
            network.Compute(input);
            //List<Double> distances = calculateDistance(new List<double>(input));

            int min = network.GetWinner();


            var neurons = network.Layers[0].Neurons;

            // modify weight of winning neuron and all neighbours
            for (int i = 0; i < network.Layers[0].Neurons.Length; i++)
            {
                if (i == min)
                {
//                    double[] neuronConnections = neurons[min].Weights;
//
//                    for (int j = 0; j < neuronConnections.Length; j++)
//                    {
//                        
//
//                        double newWeight = neuronConnections[j] + etaFunction(steps, step)
//                                * (input[j] - neuronConnections[j]);
//
//                        neurons[min].Weights[j] = newWeight;
//                    }

                    Neuron neuron = network.Layers[0][min];

                    // update weight of the winner only
                    for (int j = 0, n = neuron.InputsCount; j < n; j++)
                    {
                        neuron[i] += (input[j] - neuron[j]) * learningRate;
                    }
                }
                else
                {
                    var distance = this.Neighborhood.GetDistance(min, i);
//                    if (neighbourFunction(steps, step, min) > distance)
//                    {
//                        double[] neuronConnections = this.network.Layers[0].Neurons[i].Weights;
//
//                        for (int j = 0; j < neuronConnections.Length; j++)
//                        {
//
//                            double learning2 = learningRate/2;
//                            double newWeight = (input[j] - this.network.Layers[0].Neurons[i][j]) * learning2;
//
//                            this.network.Layers[0].Neurons[i].Weights[j] = newWeight;
//                        }
//                    }

                    /*
                     * f (neighbourFunction(steps, step, min) > countDistance(min, i))
                    {
                        double[] neuronConnections = this.network.Layers[0].Neurons[i].Weights;

                        for (int j = 0; j < neuronConnections.Length; j++)
                        {


                            double newWeight = neuronConnections[j] + etaFunction(steps, step)
                                   * (input[j] - neuronConnections[j]);

                            this.network.Layers[0].Neurons[i].Weights[j] = newWeight;
                       }
                    }*/

                    var nDistance = Neighborhood.GetDistance(min, i);
                    var nFunction = neighbourFunction(steps, step, min);
                    if (nFunction > nDistance)
                    {
                        double[] neuronConnections = this.network.Layers[0].Neurons[i].Weights;

                        for (int j = 0; j < neuronConnections.Length; j++)
                        {


                            double newWeight = neuronConnections[j] + etaFunction(steps, step)
                                   * (input[j] - neuronConnections[j]);

                            this.network.Layers[0].Neurons[i].Weights[j] = newWeight;
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


        private double countDistance(int Q, int P)
        {
//            if (neighbourMatrixDimension == 2)
//            {
            int a = 3;
                int x1 = (Q % a);
                int y1 = (Q / a);

                int x2 = (P % a);
                int y2 = (P / a);
            var res = Math.Sqrt(Math.Pow(x1 - x2, 2.0) + Math.Pow(y1 - y2, 2.0));
                return res ;
           // }
//            else
//            {
//                return Math.abs(Q - P);
//            }
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
