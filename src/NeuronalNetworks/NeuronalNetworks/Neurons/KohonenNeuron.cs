using System;

namespace NeuronalNetworks.Neurons
{
    public class KohonenNeuron : Neuron
	{

        public KohonenNeuron(int inputs) : base(inputs) { }
        public KohonenNeuron()
        {
            
        }
		public override double Compute( double[] input )
		{
			output = 0.0;

			// compute distance between inputs and weights
			for ( int i = 0; i < inputsCount; i++ )
			{
				output += Math.Abs( weights[i] - input[i] );
			}
			return output;
		}
	}
}