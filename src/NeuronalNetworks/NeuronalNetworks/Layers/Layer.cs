using System.Xml.Serialization;
using NeuronalNetworks.Networks;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Layers
{
	using System;

    [Serializable()]
    [XmlInclude(typeof(ActivationLayer))]
    [XmlInclude(typeof(KohonenLayer))]
	public abstract class Layer
	{
		protected int		inputsCount = 0;

		protected int		neuronsCount = 0;

		protected Neuron[]	neurons;

		protected double[]	output;

        public Neuron[] Neurons
        {
            get { return neurons; }
            set { neurons = value; }
        }

		public int InputsCount
		{
			get { return inputsCount; }
            set { inputsCount = value; }
		}

		public int NeuronsCount
		{
			get { return neuronsCount; }
            set { neuronsCount = value; }
		}

		public double[] Output
		{
			get { return output; }
		}

		public Neuron this[int index]
		{
			get { return neurons[index]; }
		}

        protected Layer()
        {
            
        }

		protected Layer( int neuronsCount, int inputsCount )
		{
			this.inputsCount	= Math.Max( 1, inputsCount );
			this.neuronsCount	= Math.Max( 1, neuronsCount );
			// create collection of neurons
			neurons = new Neuron[this.neuronsCount];
			// allocate output array
			output = new double[this.neuronsCount];
		}

		public virtual double[] Compute( double[] input )
		{
			if(output == null)
			{
                output = new double[this.neuronsCount];
			}
            // compute each neuron
			for ( int i = 0; i < neuronsCount; i++ )
				output[i] = neurons[i].Compute( input );

			return output;
		}

		public virtual void Randomize( )
		{
			foreach ( Neuron neuron in neurons )
				neuron.Randomize( );
		}
	}
}
