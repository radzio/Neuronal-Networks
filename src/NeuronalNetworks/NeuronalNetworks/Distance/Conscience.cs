using System;
using System.Collections.Generic;

namespace NeuronalNetworks.Distance
{
    public class Conscience
    {
        public double pMin { get; set; }

        public double gap { get; set; }

        private List<Double> p;

    public Conscience(int neuronsNumber, double pMin) 
    {
        this.pMin = pMin;

        gap = (double) 1 / neuronsNumber;

        p = new List<Double>();

        for (int i = 0; i < neuronsNumber; i++)
            p.Add(pMin);
    }

    public bool CanParticipate(int neuronNumber) {
        return (p[neuronNumber] >= pMin);
    }

    public void UpdateConscience(int winnerNeuron) 
    {
        for (int i = 0; i < p.Count; i++) 
        {
            if (i == winnerNeuron) 
            {
                p[i] = p[i] - pMin;
            } 
            else 
            {
                p[i] = p[i] + gap;
            }
        }
    }    
    }
}