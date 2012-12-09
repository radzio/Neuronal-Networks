using System;
using System.Collections.Generic;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Distance;

namespace NeuronalNetworks.Neurons
{
    public class KohonenNeuron : Neuron
    {

        public double Conscience = 1.0;
        public KohonenNeuron(int inputs) : base(inputs) {  }
        public KohonenNeuron()
        {
        }
		public override double Compute( double[] input )
		{
			output = 0.0;


		    output =  EuclideanDistance.Distance(new List<double>(weights), new List<double>(input));
			return output;
		}

        /*
        public void IncreaseConscience()
        {
            Conscience = Math.Min(Conscience + 1.0 / 4.0, 1.0);
        }

        public void DecreaseConscience()
        {
            Conscience -= 0.1;
        }

        public bool CanCompete()
        {
            return Conscience >= 0.1;
        }*/


	}
}