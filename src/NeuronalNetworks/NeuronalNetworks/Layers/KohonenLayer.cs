using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Layers
{
    public class KohonenLayer : Layer
    {

        public new KohonenNeuron this[int index]
        {
            get { return (KohonenNeuron) neurons[index]; }
        }

        public KohonenLayer()
        {
            
        }

        public KohonenLayer(int neuronsCount, int inputsCount)
            : base(neuronsCount, inputsCount)
        {
            // create each neuron
            for (int i = 0; i < neuronsCount; i++)
                neurons[i] = new KohonenNeuron(inputsCount);
        }
    }
}