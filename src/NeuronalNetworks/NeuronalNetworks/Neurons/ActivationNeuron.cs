using NeuronalNetworks.ActivationFunctions;

namespace NeuronalNetworks.Neurons
{
	using System;

	public class ActivationNeuron : Neuron
	{
		protected double threshold = 0.0f;

		protected IActivationFunction function = null;

		public double Threshold
		{
			get { return threshold; }
			set { threshold = value; }
		}

		public IActivationFunction ActivationFunction
		{
			get { return function; }
		}
		
		public ActivationNeuron( int inputs, IActivationFunction function ) : base( inputs )
		{
			this.function = function;
		}

		public override void Randomize( )
		{
			// randomize weights
			base.Randomize( );
			// randomize threshold
			threshold = rand.NextDouble( ) * ( randRange.Length ) + randRange.Min;
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
			sum += threshold;

			return ( output = function.Function( sum ) );
		}
	}
}
