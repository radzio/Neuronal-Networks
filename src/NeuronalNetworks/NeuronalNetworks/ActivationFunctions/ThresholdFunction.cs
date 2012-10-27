namespace NeuronalNetworks.ActivationFunctions
{
    public class ThresholdFunction : IActivationFunction
	{
        public double Function( double x )
		{
			return ( x >= 0 ) ? 1 : 0;
		}

		public double Derivative( double x )
		{
			double y = Function( x );

			return 0;
		}

		public double Derivative2( double y )
		{
			return 0;
		}
	}
}
