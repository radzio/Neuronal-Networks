using System;

namespace NeuronalNetworks.Distance
{
    public class TwoDimensionalNeighborhood : Neighborhood
    {
        public TwoDimensionalNeighborhood()
        {
            
        }


        public int Columns { get; set; }

        public TwoDimensionalNeighborhood(int size, int columns)
        {
            Columns = columns;
            Size = size;
            Gap = 1.0 / (1 + size);
        }

        public override double GetDistance(int winner, int cur) 
        {
            int wx = winner % Columns;
            int wy = winner / Columns;

            int cx = cur % Columns;
            int cy = cur / Columns;

            double dist = Math.Sqrt(Math.Pow(wx-cx, 2) + Math.Pow(wy-cy, 2));

            if( dist > Size )
                return 0.0;

            //return 1.0 - dist * Gap;
            return dist;
        }
    }
}