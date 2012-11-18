using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Networks
{
    public class ActivationNetwork : Network
	{
		public new ActivationLayer this[int index]
		{
			get { return ( (ActivationLayer) layers[index] ); }
		}

        protected ActivationNetwork()
        {
        }

        public ActivationNetwork( ActivationFunction function, int inputsCount, params int[] neuronsCount )
							: base( inputsCount, neuronsCount.Length )
		{
			// create each layer
			for ( int i = 0; i < layersCount; i++ )
			{
				layers[i] = new ActivationLayer(
					// neurons count in the layer
					neuronsCount[i],
					// inputs count of the layer
					( i == 0 ) ? inputsCount : neuronsCount[i - 1],
					// activation function of the layer
					function );
			}
		}

        public ActivationNetwork(ActivationFunction[] function, int inputsCount, params int[] neuronsCount)
            : base(inputsCount, neuronsCount.Length)
        {
            // create each layer
            for (int i = 0; i < layersCount; i++)
            {
                layers[i] = new ActivationLayer(
                    // neurons count in the layer
                    neuronsCount[i],
                    // inputs count of the layer
                    (i == 0) ? inputsCount : neuronsCount[i - 1],
                    // activation function of the layer
                    function[i]);
            }
        }

	}
}
