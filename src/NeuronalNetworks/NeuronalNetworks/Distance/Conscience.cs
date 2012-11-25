using System;
using System.Collections.Generic;

namespace NeuronalNetworks.Distance
{
    public class Conscience
    {
        public double MinimumPotential { get; set; }


        private readonly List<Double> potentials;

        private double gap = 1;

    public Conscience(int neuronsNumber, double minimumPotential) 
    {
        this.MinimumPotential = minimumPotential;

        this.gap = 1.0 / neuronsNumber;

        this.potentials = new List<Double>();

        for (int i = 0; i < neuronsNumber; i++)
            this.potentials.Add(0);
    }

    public bool CanParticipate(int neuronNumber) {
        return (potentials[neuronNumber] >= MinimumPotential );
    }

    public void UpdateConscience(int winnerNeuron) 
    {
        for (int i = 0; i < potentials.Count; i++) 
        {
            if (i == winnerNeuron) 
            {
                this.potentials[i] = potentials[i] - MinimumPotential;
            } 
            else 
            {
                this.potentials[i] = potentials[i] + gap;
            }
        }
    }    
    }
}