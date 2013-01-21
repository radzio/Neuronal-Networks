using System;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Networks;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Learning
{
    public class BackPropagationLearning : ISupervisedLearning
    {

        private ActivationNetwork network;

        private double learningRate = 0.1;

        private double momentum = 0.0;


        private double[][] neuronErrors = null;

        private double[][][] weightsUpdates = null;

        private double[][] thresholdsUpdates = null;

        public double LearningRate
        {
            get { return learningRate; }
            set
            {
                learningRate = Math.Max(0.0, Math.Min(1.0, value));
            }
        }

        public double Momentum
        {
            get { return momentum; }
            set
            {
                momentum = Math.Max(0.0, Math.Min(1.0, value));
            }
        }


        public BackPropagationLearning(ActivationNetwork network)
        {
            this.network = network;

            // create error and deltas arrays
            neuronErrors = new double[network.LayersCount][];
            weightsUpdates = new double[network.LayersCount][][];
            thresholdsUpdates = new double[network.LayersCount][];

            // initialize errors and deltas arrays for each layer
            for (int i = 0, n = network.LayersCount; i < n; i++)
            {
                Layer layer = network[i];

                neuronErrors[i] = new double[layer.NeuronsCount];
                weightsUpdates[i] = new double[layer.NeuronsCount][];
                thresholdsUpdates[i] = new double[layer.NeuronsCount];

                // for each neuron
                for (int j = 0; j < layer.NeuronsCount; j++)
                {
                    weightsUpdates[i][j] = new double[layer.InputsCount];
                }
            }
        }


        public double Run(double[] input, double[] output)
        {
 
            network.Compute(input);

  
            double error = CalculateError(output);


            CalculateUpdates(input);

 
            UpdateNetwork();

            return error;
        }

        public double RunEpoch(double[][] input, double[][] output, int steps)
        {
            throw new NotImplementedException();
        }


        public double RunEpoch(double[][] input, double[][] output)
        {
            double error = 0.0;

            for (int i = 0, n = input.Length; i < n; i++)
            {
                error += Run(input[i], output[i]);
            }


            return error;
        }


       
        private double CalculateError(double[] desiredOutput)
        {

            ActivationLayer layer, layerNext;

            double[] errors, errorsNext;

            double error = 0, e, sum;
      
            double output;
     
            int layersCount = network.LayersCount;




            layer = network[layersCount - 1];
            errors = neuronErrors[layersCount - 1];

            for (int i = 0, n = layer.NeuronsCount; i < n; i++)
            {
                output = layer[i].Output;
                var function = layer[i].ActivationFunction;

                e = desiredOutput[i] - output;

                errors[i] = e * function.Derivative(output);

                error += (e * e);
            }


            for (int j = layersCount - 2; j >= 0; j--)
            {
                layer = network[j];
                layerNext = network[j + 1];
                errors = neuronErrors[j];
                errorsNext = neuronErrors[j + 1];
                

                for (int i = 0, n = layer.NeuronsCount; i < n; i++)
                {
                    sum = 0.0;
                    var function = layer[i].ActivationFunction;

                    for (int k = 0, m = layerNext.NeuronsCount; k < m; k++)
                    {
                        sum += errorsNext[k] * layerNext[k][i];
                    }
                    errors[i] = sum * function.Derivative(layer[i].Output);
                }
            }


            return error / 2.0;
        }

        private void CalculateUpdates(double[] input)
        {
            // current neuron
            ActivationNeuron neuron;
            // current and previous layers
            ActivationLayer layer, layerPrev;
            // layer's weights updates
            double[][] layerWeightsUpdates;
            // layer's thresholds updates
            double[] layerThresholdUpdates;
            // layer's error
            double[] errors;
            // neuron's weights updates
            double[] neuronWeightUpdates;

            layer = network[0];
            errors = neuronErrors[0];
            layerWeightsUpdates = weightsUpdates[0];
            layerThresholdUpdates = thresholdsUpdates[0];


            double cachedMomentum = learningRate * momentum;
            double cached1mMomentum = learningRate * (1 - momentum);
            double cachedError;


            for (int i = 0, n = layer.NeuronsCount; i < n; i++)
            {
                neuron = layer[i];
                cachedError = errors[i] * cached1mMomentum;
                neuronWeightUpdates = layerWeightsUpdates[i];


                for (int j = 0, m = neuron.InputsCount; j < m; j++)
                {

                    neuronWeightUpdates[j] = cachedMomentum * neuronWeightUpdates[j] + cachedError * input[j];
                }


                layerThresholdUpdates[i] = cachedMomentum * layerThresholdUpdates[i] + cachedError;
            }

            for (int k = 1, l = network.LayersCount; k < l; k++)
            {
                layerPrev = network[k - 1];
                layer = network[k];
                errors = neuronErrors[k];
                layerWeightsUpdates = weightsUpdates[k];
                layerThresholdUpdates = thresholdsUpdates[k];


                for (int i = 0, n = layer.NeuronsCount; i < n; i++)
                {
                    neuron = layer[i];
                    cachedError = errors[i] * cached1mMomentum;
                    neuronWeightUpdates = layerWeightsUpdates[i];


                    for (int j = 0, m = neuron.InputsCount; j < m; j++)
                    {

                        neuronWeightUpdates[j] = cachedMomentum * neuronWeightUpdates[j] + cachedError * layerPrev[j].Output;
                    }


                    if(layer.HasThreshold)
                        layerThresholdUpdates[i] = cachedMomentum * layerThresholdUpdates[i] + cachedError;
                }
            }
        }

        private void UpdateNetwork()
        {

            ActivationNeuron neuron;

            ActivationLayer layer;

            double[][] layerWeightsUpdates;

            double[] layerThresholdUpdates;

            double[] neuronWeightUpdates;

            // for each layer of the network
            for (int i = 0, n = network.LayersCount; i < n; i++)
            {
                layer = network[i];
                layerWeightsUpdates = weightsUpdates[i];
                layerThresholdUpdates = thresholdsUpdates[i];

                // for each neuron of the layer
                for (int j = 0, m = layer.NeuronsCount; j < m; j++)
                {
                    neuron = layer[j];
                    neuronWeightUpdates = layerWeightsUpdates[j];

                    // for each weight of the neuron
                    for (int k = 0, s = neuron.InputsCount; k < s; k++)
                    {
                        // update weight
                        neuron[k] += neuronWeightUpdates[k];
                    }
                    // update treshold
                    if(layer.HasThreshold)
                        neuron.Threshold += layerThresholdUpdates[j];
                }
            }
        }
    }

}