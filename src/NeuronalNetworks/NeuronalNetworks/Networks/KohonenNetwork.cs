using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Networks
{
    public class KohonenNetwork: Network
	{
		public new KohonenLayer this[int index]
		{
            get { return ((KohonenLayer)layers[index]); }
		}

        public KohonenNetwork()
        {
            
        }

        public KohonenNetwork(int inputsCount, int neuronsCount)
						: base( inputsCount, 1 )
		{
			// create layer
            layers[0] = new KohonenLayer(neuronsCount, inputsCount);
		}

		
		public int GetWinner( )
		{
			// find the MIN value
			double	min = output[0];
			int		minIndex = 0;

			for ( int i = 1, n = output.Length; i < n; i++ )
			{
				if ( output[i] < min )
				{
					// found new MIN value
					min = output[i];
					minIndex = i;
				}
			}

			return minIndex;
		}
	}
}
