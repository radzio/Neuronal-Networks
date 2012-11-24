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
             Gap = 1.0 - 1.0/(size + 1)/(size + 1);
         }
   

        public override double GetDistance(int winner, int cur)
        {
            var distance = Math.Abs(winner - cur);
            if( distance  > Size )
            {
                return 0.0;
            }

            //return 1.0 - (double)Gap * Math.Abs(winner-cur);
            return distance;
        }
    }
}