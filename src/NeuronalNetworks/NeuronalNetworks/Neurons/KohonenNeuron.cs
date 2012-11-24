using System;
using System.Collections.Generic;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Distance;

namespace NeuronalNetworks.Neurons
{
    public class KohonenNeuron : Neuron
	{

        public KohonenNeuron(int inputs) : base(inputs) { this.function = new SigmoidFunction(); }
        public KohonenNeuron()
        {
            this.function = new SigmoidFunction();
        }
		public override double Compute( double[] input )
		{
			output = 0.0;

			// compute distance between inputs and weights
//			for ( int i = 0; i < inputsCount; i++ )
//			{
//				output += Math.Abs( weights[i] - input[i] );
//			}


//			double sum = 0.0;
//
//			// compute weighted sum of inputs
//			for ( int i = 0; i < inputsCount; i++ )
//			{
//				sum += weights[i] * input[i];
//			}
//
//			return ( output =  sum  );


		    output =  EuclideanDistance.Distance(new List<double>(weights), new List<double>(input));
			return output;
		}
	}
}