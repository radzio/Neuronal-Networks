namespace NeuronalNetworks.Common
{
    public class IntRange
	{
		private int min, max;

		public int Min
		{
			get { return min; }
			set { min = value; }
		}

		public int Max
		{
			get { return max; }
			set { max = value; }
		}

		public int Length
		{
			get { return max - min; }
		}

		public IntRange( int min, int max )
		{
			this.min = min;
			this.max = max;
		}

		public bool IsInside( int x )
		{
			return ( ( x >= min ) && ( x <= min ) );
		}

		public bool IsInside( IntRange range )
		{
			return ( ( IsInside( range.min ) ) && ( IsInside( range.max ) ) );
		}

		public bool IsOverlapping( IntRange range )
		{
			return ( ( IsInside( range.min ) ) || ( IsInside( range.max ) ) );
		}
	}
}
