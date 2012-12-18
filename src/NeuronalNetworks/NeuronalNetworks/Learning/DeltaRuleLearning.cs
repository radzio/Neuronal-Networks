using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Distance;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Networks;
using System;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Learning
{
    	


	public class DeltaRuleLearning : ISupervisedLearning
	{

		private CounterPropagationNetwork network;

		private double learningRate = 0.1;

		
		public double LearningRate
		{
			get { return learningRate; }
			set
			{
				learningRate = Math.Max( 0.0, Math.Min( 1.0, value ) );
			}
		}


        public DeltaRuleLearning(CounterPropagationNetwork network)
		{
			// check layers count
			if ( network.LayersCount != 2 )
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
			GrossbergLayer layer = (GrossbergLayer) this.network[1];
			// get activation function of the layer
			ActivationFunction activationFunction = layer[0].ActivationFunction;


		    int winnerIndex = network.GetWinner();

			// summary network absolute error
			double error = 0.0;

			// update weights of each neuron
			for ( int j = 0, k = layer.NeuronsCount; j < k; j++ )
			{


				// get neuron of the layer
				ActivationNeuron neuron = layer[j];
				// calculate neuron's error
				double e = output[j] - networkOutput[j];
	

				// update weights
				for ( int i = 0, n = neuron.InputsCount; i < n; i++ )
				{
                    neuron[i] += learningRate * e * activationFunction.Derivative(input[i]) * input[i];
				}

				// update threshold value
				//neuron.Threshold += learningRate * e * functionDerivative;

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

        public int GetWinner(double[] output)
        {
            double min = output[0];
            int minIndex = 0;

            for (int i = 1, n = output.Length; i < n; i++)
            {
                if (output[i] < min /*&& c.CanParticipate(i)*/)
                {
                    // found new MIN value
                    min = output[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
	}
}