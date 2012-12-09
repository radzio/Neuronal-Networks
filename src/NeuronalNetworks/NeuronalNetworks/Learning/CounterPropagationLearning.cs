using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronalNetworks.Learning
{
    public class CounterPropagationLearning : ISupervisedLearning
    {
         // todo
        public double Run(double[] input, double[] output)
        {
            /*
             * 
             * 
             * double kohonenOutput[] = kohonenNet.isLearningFinished() ? kohonenNet.compute(train) : kohonenNet.learnWithOutput(train, null);
                if (kohonenNet.getEpoch() > configuration.getKohonenInitializationEpochs() && !grossbergNet.isLearningFinished()) {
                        grossbergNet.learnWithOutput(kohonenOutput, test);
                }

             */

            return 0.0;
        }


        /*
         * 
         * ublic void learn(int step, int steps) throws BadNeuronInitiationException {
                for (Neuron neuron : neurons) {
                        double deltaWeight = learningFactor * neuron.error * neuron.activationFunction.derivative(neuron.input);
                        for (Connection connection : neuron.getInputConnections()) {
                                double weightChange = deltaWeight * connection.getInputNeuron().output + momentum
                                                * connection.previousChange;
                                connection.previousChange = weightChange;
                                connection.setWeight(connection.getWeight() + weightChange);
                        }
                        double biasChange = deltaWeight * neuron.bias * neuron.BIAS + momentum * neuron.previousBiasChange;
                        neuron.previousBiasChange = biasChange;
                        neuron.bias += biasChange;
                }

                updateLearningFactor(step, steps);

                List<Connection> connections = neurons.get(0).getOutputConnections();
                if (connections.size() > 0) {
                        int layerNr = connections.get(0).getOutputNeuron().layerNr;
                        Layer layer = Net.getInstance().layersMap.get(layerNr);
                        if (layer != null)
                                layer.learn(step, steps);
                }
        }

         * 
         */

        public double RunEpoch(double[][] input, double[][] output)
        {
            throw new NotImplementedException();
        }
    }
}
