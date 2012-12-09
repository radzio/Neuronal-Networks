using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Layers
{
    public class GrossbergLayer : Layer
    {
        public GrossbergLayer(int neuronsCount, int inputsCount)
            : base(neuronsCount, inputsCount)
        {
            // create each neuron
            for (int i = 0; i < neuronsCount; i++)
                neurons[i] = new ActivationNeuron(inputsCount, new SigmoidFunction());
        }
    }
}