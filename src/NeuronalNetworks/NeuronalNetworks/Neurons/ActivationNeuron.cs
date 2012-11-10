using NeuronalNetworks.ActivationFunctions;

namespace NeuronalNetworks.Neurons
{
	using System;

	public class ActivationNeuron : Neuron
	{
		protected double threshold = 0.0f;
        protected double bias = 0.0f;

        public ActivationNeuron()
        {
            
        }

		public double Threshold
		{
			get { return threshold; }
			set { threshold = value; }
		}
        //todo wywaliæ Bias / Tesholdhreshold
        public double Bias
        {
            get { return bias; }
            set { bias = value; }
        }


		
		public ActivationNeuron( int inputs, ActivationFunction function ) : base( inputs )
		{
			this.function = function;
		}

		public override void Randomize( )
		{
			// randomize weights
			base.Randomize( );
			// randomize threshold
			threshold = rand.NextDouble( ) * ( randRange.Length ) + randRange.Min;

            //randomize bias
            bias = rand.NextDouble() * (randRange.Length) + randRange.Min;
		}


		public override double Compute( double[] input )
		{
			// check for corrent input vector
			if ( input.Length != inputsCount )
				throw new ArgumentException( );

			// initial sum value
			double sum = 0.0;

			// compute weighted sum of inputs
			for ( int i = 0; i < inputsCount; i++ )
			{
				sum += weights[i] * input[i];
			}
			sum -= threshold;
		    sum += bias;

			return ( output = function.Function( sum ) );
		}
	}
}
