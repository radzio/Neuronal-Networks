using System;

namespace NeuronalNetworks.Gui.ViewModel
{
    public class WeightViewModel
    {
        public string Test { get { return "Weight"; } }
        public double Weight { get; set; }
        public WeightViewModel(double weight)
        {
            this.Weight = weight;
        }
    }
}