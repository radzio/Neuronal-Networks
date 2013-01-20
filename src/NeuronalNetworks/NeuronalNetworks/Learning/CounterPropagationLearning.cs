using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeuronalNetworks.Distance;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.Learning
{
    public class CounterPropagationLearning : ISupervisedLearning
    {
        private double learningRate;

        private CounterPropagationNetwork network;
        private SOMLayerLearning somLearning;


        public Conscience Conscience { get; set; }


        public double RunEpoch(double[][] input, double[][] output)
        {
            throw new NotImplementedException();
        }

        public double LearningRate
        {
            get { return learningRate; }
            set
            { 
                learningRate = Math.Max(0.0, Math.Min(1.0, value));
                somLearning.LearningRate = learningRate;
            }
        }



        public Neighborhood Neighborhood { get; set; }

        public bool DeltaRule { get; set; }

        public double LearningRadius
        {
            get { return Neighborhood.LearningRadius; }
            set
            {
                Neighborhood.LearningRadius = value;
                this.somLearning.Neighborhood = Neighborhood;
            }
        }


        public double ConscienceValue
        {
            get { return Conscience.MinimumPotential; }
            set
            {
                Conscience.MinimumPotential = value;
                this.somLearning.Conscience = Conscience;
            }
        }

        public CounterPropagationLearning(CounterPropagationNetwork network)
        {
            this.network = network;
            this.somLearning = new SOMLayerLearning(this.network);
            this.Neighborhood = new OneDimensionalNeighborhood(3);

            this.Conscience = new Conscience(network.Layers[0].NeuronsCount, 0);

            somLearning.Conscience = Conscience;
            somLearning.Neighborhood = Neighborhood;
        }

        public double Run(double[] input, double[] output)
        {


            somLearning.Run(input);
            var kohonenOutput = network.KohonenLayer.Compute(input);

            

            ISupervisedLearning learning = null;
            if(DeltaRule)
            {
                learning = new DeltaRuleLearning(network);
            }
            else
            {
                learning = new WidrowHoffLearning(network);
            }


            //learning.LearningRate = 0.1;

            return learning.Run(kohonenOutput, output);
        }


        

        public double RunEpoch(double[][] input, double[][] output, int steps)
        {
            double error = 0.0;

            // run learning procedure for all samples

            for (var i = 0; i < steps; i++ )
            {   
                for (int j = 0, n = input.Length; j < n; j++)
                {
                    somLearning.Run(input[j]);
                }
                
            }


            for (var i = 0; i < steps; i++)
            {
                for (int j = 0, n = input.Length; j < n; j++)
                {
                    var inputForLearning = input[j];
                    var kohonenOutput = network.KohonenLayer.Compute(inputForLearning);

                    ISupervisedLearning learning = null;
                    if (DeltaRule)
                    {
                        learning = new DeltaRuleLearning(network);
                    }
                    else
                    {
                        learning = new WidrowHoffLearning(network);
                    }

                    error += learning.Run(kohonenOutput, inputForLearning);
                }
            }

            return error;
        }
    }
}
