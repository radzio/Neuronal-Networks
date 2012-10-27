namespace NeuronalNetworks.Common
{
    public class DoubleRange
	{
		private double min, max;

		public double Min
		{
			get { return min; }
			set { min = value; }
		}

		public double Max
		{
			get { return max; }
			set { max = value; }
		}

		public double Length
		{
			get { return max - min; }
		}

		public DoubleRange( double min, double max )
		{
			this.min = min;
			this.max = max;
		}

		public bool IsInside( double x )
		{
			return ( ( x >= min ) && ( x <= min ) );
		}

		public bool IsInside( DoubleRange range )
		{
			return ( ( IsInside( range.min ) ) && ( IsInside( range.max ) ) );
		}

		public bool IsOverlapping( DoubleRange range )
		{
			return ( ( IsInside( range.min ) ) || ( IsInside( range.max ) ) );
		}
	}
}
