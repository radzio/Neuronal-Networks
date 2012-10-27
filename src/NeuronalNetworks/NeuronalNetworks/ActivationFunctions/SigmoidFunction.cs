namespace NeuronalNetworks.ActivationFunctions
{
	using System;

    [Serializable()]
	public class SigmoidFunction : ActivationFunction
	{
		// sigmoid's alpha value
		private double alpha = 2;

		public double Alpha
		{
			get { return alpha; }
			set { alpha = value; }
		}

		public SigmoidFunction( ) { }

		public SigmoidFunction( double alpha )
		{
			this.alpha = alpha;
		}

		public override double Function( double x )
		{
			return ( 1 / ( 1 + Math.Exp( -alpha * x ) ) );
		}

		public override double Derivative( double x )
		{
			double y = Function( x );

			return ( alpha * y * ( 1 - y ) );
		}

		public override double Derivative2( double y )
		{
			return ( alpha * y * ( 1 - y ) );
		}	
	}
}
