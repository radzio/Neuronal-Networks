using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Layers
{
    public class GrossbergLayer : ActivationLayer
    {
        public GrossbergLayer(int neuronsCount, int inputsCount)
            : this(neuronsCount, inputsCount, new SigmoidFunction())
        {
           
        }

        public GrossbergLayer()
        {
            
        }

        public GrossbergLayer(int neuronsCount, int inputsCount, ActivationFunction activationFunction)
            : this(new SigmoidFunction(), neuronsCount, inputsCount, activationFunction)
        {
            
        }


        public GrossbergLayer(ActivationFunction function, int neuronsCount, int inputsCount, ActivationFunction activationFunction)
            : base(neuronsCount, inputsCount, activationFunction)
        {
            // create each neuron
            for (int i = 0; i < neuronsCount; i++)
                neurons[i] = new ActivationNeuron(inputsCount, function);
        }

    }
}