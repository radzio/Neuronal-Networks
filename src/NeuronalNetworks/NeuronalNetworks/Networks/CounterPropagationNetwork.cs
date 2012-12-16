using NeuronalNetworks.Distance;
using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Networks
{
    public class CounterPropagationNetwork :  Network
    {

        private double[] kohonenOutput;
        
        public CounterPropagationNetwork(int inputsCount, int neuronsCount)
						: base( inputsCount, 3 )
		{
			// create layer
            layers[0] = new KohonenLayer(neuronsCount, inputsCount);
            layers[1] = new GrossbergLayer(neuronsCount, inputsCount);
		}

        public KohonenLayer KohonenLayer
        {
            get { return (KohonenLayer) layers[0]; }
        }

        public GrossbergLayer GrossbergLayer
        {
            get { return (GrossbergLayer)layers[1]; }
        }

        public double[] Compute(double[] input) 
        {
                kohonenOutput = KohonenLayer.Compute(input);
                return GrossbergLayer.Compute(kohonenOutput);
        }

        public int GetWinner()
        {
            // find the MIN value
            double min = kohonenOutput[0];
            int minIndex = 0;

            for (int i = 1, n = kohonenOutput.Length; i < n; i++)
            {
                if (kohonenOutput[i] < min)
                {
                    // found new MIN value
                    min = kohonenOutput[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        public int GetWinner(Conscience c)
        {
            double min = kohonenOutput[0];
            int minIndex = 0;

            for (int i = 1, n = kohonenOutput.Length; i < n; i++)
            {
                if (kohonenOutput[i] < min && c.CanParticipate(i))
                {
                    // found new MIN value
                    min = kohonenOutput[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }


    }
}