using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using NeuronalNetworks.Layers;

namespace NeuronalNetworks.Distance
{
    [Serializable()]
    [XmlInclude(typeof(OneDimensionalNeighborhood))]
    [XmlInclude(typeof(TwoDimensionalNeighborhood))]
    public abstract class Neighborhood
    {
        public abstract double GetDistance(int winner, int cur);

        public int Size { get; set; }

        public double Gap { get; set; }

    }
}
