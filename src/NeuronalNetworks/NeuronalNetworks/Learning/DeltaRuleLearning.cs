﻿using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Networks;
using System;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Learning
{
    	


	public class DeltaRuleLearning : ISupervisedLearning
	{

		private ActivationNetwork network;

		private double learningRate = 0.1;

		
		public double LearningRate
		{
			get { return learningRate; }
			set
			{
				learningRate = Math.Max( 0.0, Math.Min( 1.0, value ) );
			}
		}

		
		public DeltaRuleLearning( ActivationNetwork network )
		{
			// check layers count
			if ( network.LayersCount != 1 )
			{
				throw new ArgumentException( "Invalid nuaral network. It should have one layer only." );
			}

			this.network = network;
		}

	
		public double Run( double[] input, double[] output )
		{
			// compute output of network
			double[] networkOutput = network.Compute( input );

			// get the only layer of the network
			ActivationLayer layer = network[0];
			// get activation function of the layer
			ActivationFunction activationFunction = layer[0].ActivationFunction;

			// summary network absolute error
			double error = 0.0;

			// update weights of each neuron
			for ( int j = 0, k = layer.NeuronsCount; j < k; j++ )
			{
				// get neuron of the layer
				ActivationNeuron neuron = layer[j];
				// calculate neuron's error
				double e = output[j] - networkOutput[j];
				// get activation function's derivative
				double functionDerivative = activationFunction.Derivative2( networkOutput[j] );

				// update weights
				for ( int i = 0, n = neuron.InputsCount; i < n; i++ )
				{
					neuron[i] += learningRate * e * functionDerivative * input[i];
				}

				// update threshold value
				neuron.Threshold += learningRate * e * functionDerivative;

				// sum error
				error += ( e * e );
			}

			return error / 2;
		}


		public double RunEpoch( double[][] input, double[][] output )
		{
			double error = 0.0;

			// run learning procedure for all samples
			for ( int i = 0, n = input.Length; i < n; i++ )
			{
				error += Run( input[i], output[i] );
			}

			// return summary error
			return error;
		}
	}
}

}