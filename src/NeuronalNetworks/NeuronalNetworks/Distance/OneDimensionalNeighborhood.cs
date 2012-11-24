using System;

namespace NeuronalNetworks.Distance
{
    public class OneDimensionalNeighborhood : Neighborhood
    {
         public OneDimensionalNeighborhood()
         {
             
         }

         public OneDimensionalNeighborhood(int size)
         {
             this.Size = size;
         }


        public override double GetFactor(int winner, int neuron)
        {
            // winner's X and Y
            int wx = winner;

            int dx = neuron - wx;

            // update factor ( Gaussian based )
            double factor = Math.Exp(-(double)(dx * dx) / squaredRadius2);

            return factor;
        }

        public override double GetDistance(int winner, int cur)
        {
            var distance = Math.Abs(winner - cur);

            if( distance  > Size )
            {
                return 0.0;
            }

            return distance;
        }
    }
}