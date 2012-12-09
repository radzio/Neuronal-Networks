using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Networks
{
    public class CounterPropagationNetwork :  Network
    {
        public CounterPropagationNetwork(int inputsCount, int neuronsCount)
						: base( inputsCount, 3 )
		{
			// create layer
            layers[0] = new KohonenLayer(neuronsCount, inputsCount);
            layers[1] = new GrossbergLayer(neuronsCount, inputsCount);
		}

        private KohonenLayer KohonenLayer
        {
            get { return (KohonenLayer) layers[0]; }
        }

        private GrossbergLayer GrossbergLayer
        {
            get { return (GrossbergLayer)layers[1]; }
        }

        public double[] Compute(double[] input) 
        {
                double[] kohonenOutput = KohonenLayer.Compute(input);
                return GrossbergLayer.Compute(kohonenOutput);
        }


    }
}