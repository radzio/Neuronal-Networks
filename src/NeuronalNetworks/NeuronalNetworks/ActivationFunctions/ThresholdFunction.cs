using System;

namespace NeuronalNetworks.ActivationFunctions
{
    [Serializable()]
    public class ThresholdFunction : ActivationFunction
	{
        public override double Function( double x )
		{
			return ( x >= 0 ) ? 1 : 0;
		}

		public override double Derivative( double x )
		{
			double y = Function( x );

			return 0;
		}

		public override double Derivative2( double y )
		{
			return 0;
		}
	}
}
