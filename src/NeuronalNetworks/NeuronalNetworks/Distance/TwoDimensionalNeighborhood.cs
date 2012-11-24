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
        }

        public override double GetFactor(int winner, int neuron)
        {
            // winner's X and Y
            int wx = winner % Columns;
            int wy = winner / Columns;

            int dx = (neuron % Columns) - wx;
            int dy = (neuron / Columns) - wy;

            // update factor ( Gaussian based )
            double factor = Math.Exp(-(double)(dx * dx + dy * dy) / squaredRadius2);
            if(Double.IsNaN(factor))
            {
                Console.WriteLine("a");
            }
            return factor;

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

            return dist;
        }
    }
}