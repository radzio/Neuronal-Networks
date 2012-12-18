using System;
using System.Xml.Serialization;
using NeuronalNetworks.Layers;

namespace NeuronalNetworks.ActivationFunctions
{
    [Serializable()]
    [XmlInclude(typeof(ThresholdFunction))]
    [XmlInclude(typeof(SigmoidFunction))]
    [XmlInclude(typeof(LinearFunction))]
    public abstract  class ActivationFunction
	{
		public abstract double Function( double x );

        public abstract double Derivative(double x);

        public abstract double Derivative2(double y);
	}
}
