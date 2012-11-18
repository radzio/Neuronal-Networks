using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using NeuronalNetworks.ActivationFunctions;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.Gui
{
    /// <summary>
    /// Interaction logic for NewNetworkWindow.xaml
    /// </summary>
    public partial class NewNetworkWindow : Window
    {
        private ObservableCollection<GuiLayer> layers = new ObservableCollection<GuiLayer>();

        public ObservableCollection<GuiLayer> Layers 
        {
            get { return this.layers; }
            private set { this.layers = value; }
        }

        public Network Network { get; private set; }
        private int selectedNetworkType = 0;
        private int selectedInputCount;

        public NewNetworkWindow()
        {
            InitializeComponent();
            
        }

        private void BuildLayers(object sender, RoutedEventArgs e)
        {

            selectedNetworkType = this.networkType.SelectedIndex; ;
            Layers.Clear();
            this.selectedInputCount = Convert.ToInt32(this.iputCounts.Text);
            switch (selectedNetworkType)
            {
                case 0:
                for (int i = 0; i < Convert.ToInt32(this.layersCount.Text); i++)
                    {
                        Layers.Add(new GuiLayer());
                    }
 
                break;
                case 1:
                    Layers.Add(new GuiLayer { ActivationFunction =  GuiActivationFunction.None});
                break;

            }

            LayersDataGrid.ItemsSource = Layers;
            LayersDataGrid.Items.Refresh();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            switch (selectedNetworkType)
            {
                case 0:
                    int[] neuronsCount = Layers.Select(l => l.NeuronsCount).ToArray();
                    ActivationFunction[] activationFunctions = new ActivationFunction[Layers.Count];
                    int i = 0;
                    foreach (var layer in Layers)
                    {
                        if(layer.ActivationFunction == GuiActivationFunction.ThresholdFunction)
                        {
                            activationFunctions[i] = new ThresholdFunction();
                        }
                        else
                        {
                            activationFunctions[i] = new SigmoidFunction();
                        }
                    }

                    this.Network = new ActivationNetwork(activationFunctions, selectedInputCount, neuronsCount);
                    
                    break;
                case 1:
                    this.Network = new KohonenNetwork(this.selectedInputCount, Layers[0].NeuronsCount);
                    break;
            }

            this.Close();
        }
    }


    public class GuiLayer
    {
        public GuiActivationFunction ActivationFunction { get; set; }
        public int NeuronsCount { get; set; }
    }

    public enum GuiActivationFunction
    {
        ThresholdFunction, SigmoidFunction, None
    }
}
