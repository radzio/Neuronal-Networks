using System.Xml.Serialization;
using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Networks
{
	using System;

    [Serializable()]
    [XmlInclude(typeof(ActivationNetwork))]
    [XmlInclude(typeof(KohonenNetwork))]
	public abstract class Network
	{
		protected int	inputsCount;

		protected int	layersCount;

		protected Layer[]	layers;

		protected double[]	output;

		public int InputsCount
		{
			get { return inputsCount; }
            set { inputsCount = value; }
		}

		public int LayersCount
		{
			get { return layersCount; }
            set { layersCount = value; }
		}

		public double[] Output
		{
			get { return output; }
		}

        public Layer[] Layers
        {
            get { return layers; }
            set { layers = value; }
        }

		public Layer this[int index]
		{
			get { return layers[index]; }
		}

	    protected Network()
	    {
	        
	    }

		protected Network( int inputsCount, int layersCount )
		{
			this.inputsCount = Math.Max( 1, inputsCount );
			this.layersCount = Math.Max( 1, layersCount );
			// create collection of layers
			layers = new Layer[this.layersCount];
		}

		public virtual double[] Compute( double[] input )
		{
			output = input;

			// compute each layer
			foreach ( Layer layer in layers )
			{
				output = layer.Compute( output );
			}

			return output;
		}
		
		public virtual void Randomize( )
		{
			foreach ( Layer layer in layers )
			{
				layer.Randomize();
			}
		}
	}
}
