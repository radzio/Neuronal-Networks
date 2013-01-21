using NeuronalNetworks.ActivationFunctions;

namespace NeuronalNetworks.Neurons
{
	using System;

	public class ActivationNeuron : Neuron
	{
		protected double threshold = 0.0f;

        public ActivationNeuron()
        {
            
        }

        public double Error { get; set; }


		public double Threshold
		{
			get { return threshold; }
			set { threshold = value; }
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
            threshold = 0.0;//rand.NextDouble( ) * ( randRange.Length ) + randRange.Min;

            //randomize bias
            //bias = rand.NextDouble() * (randRange.Length) + randRange.Min;
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

			return ( output = function.Function( sum ) );
		}
	}
}
