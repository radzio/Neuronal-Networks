using System;
using System.Collections.Generic;
using System.Linq;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Common;
using NeuronalNetworks.Distance;
using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Networks
{
    public class CounterPropagationNetwork :  Network
    {

        private double[] kohonenOutput;
        
        public CounterPropagationNetwork(int inputsCount, ActivationFunction function,  params int[] neuronsCount)
						: base( inputsCount, 2 )
		{
			// create layer
            layers[0] = new KohonenLayer(neuronsCount[0], inputsCount);
            layers[1] = new GrossbergLayer(neuronsCount[1], neuronsCount[0], function);
		}

        public CounterPropagationNetwork()
        {
            
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


                        var winner = this.GetWinner ();
                        for (int i = 0; i < kohonenOutput.Length; i++)
                        {
                            if (i != winner)
                                kohonenOutput[i] = 0.0;
                            else
                            {
                                kohonenOutput[i] = 1.0;
                            }

                        }

            this.output =  GrossbergLayer.Compute(kohonenOutput);


            return this.Output;
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