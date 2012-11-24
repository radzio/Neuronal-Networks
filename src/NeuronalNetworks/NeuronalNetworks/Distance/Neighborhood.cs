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
        private double learningRadius;
        protected double squaredRadius2;

        public double LearningRadius
        {
            get { return learningRadius; }
            set
            {
                learningRadius = Math.Max(0, value);
                squaredRadius2 = 2 * learningRadius * learningRadius;
            }
        }

        public abstract double GetFactor(int winner, int neuron);

        public abstract double GetDistance(int winner, int cur);

        public int Size { get; set; }

        
    }
}
