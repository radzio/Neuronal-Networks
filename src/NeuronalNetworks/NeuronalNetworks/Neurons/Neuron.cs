using System.Xml.Serialization;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Common;

namespace NeuronalNetworks.Neurons
{
	using System;

    [Serializable()]
    [XmlInclude(typeof(ActivationNeuron))]
	public abstract class Neuron
	{
		protected int		inputsCount = 0;
		protected double[]	weights = null;
		protected double	output = 0;				


		protected static Random	rand = new Random( (int) DateTime.Now.Ticks );

		protected static DoubleRange randRange = new DoubleRange( 0.0, 1.0 );
        protected ActivationFunction function;

        public static Random RandGenerator
		{
			get { return rand; }
			set
			{
				if ( value != null )
				{
					rand = value;
				}
			}
		}

        protected Neuron()
        {
            
        }

		public static DoubleRange RandRange
		{
			get { return randRange; }
			set
			{
				if ( value != null )
				{
					randRange = value;
				}
			}
		}

		public int InputsCount
		{
			get { return inputsCount; }
            set { inputsCount = value; }
		}

		public double Output
		{
			get { return output; }
		}

        public ActivationFunction ActivationFunction
        {
            get { return function; }
            set {  function = value; }
        }

		public double this[int index]
		{
			get { return weights[index]; }
			set { weights[index] = value; }
		}

        public double[] Weights
        {
            get { return weights;}
            set { weights = value; }
        }

		public Neuron( int inputs )
		{
			// allocate weights
			inputsCount = Math.Max( 1, inputs );
			weights = new double[inputsCount];
			// randomize the neuron
			Randomize( );
		}


		public virtual void Randomize( )
		{
			double d = randRange.Length;

			// randomize weights
			for ( int i = 0; i < inputsCount; i++ )
				weights[i] = rand.NextDouble( ) * d + randRange.Min;
		}

		public abstract double Compute( double[] input );
	}
}
