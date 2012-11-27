using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NeuronalNetworks.Common;
using NeuronalNetworks.Gui.ViewModel;
using NeuronalNetworks.Layers;
using NeuronalNetworks.Networks;
using NeuronalNetworks.Neurons;

namespace NeuronalNetworks.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Network network;

        public MainWindow()
        {
            InitializeComponent();
            this.Weights = new ObservableCollection<WeightViewModel>();
            this.LayersBox.SelectionChanged += LayersBoxOnSelectionChanged;
            this.NeuronsBox.SelectionChanged += NeuronsBoxOnSelectionChanged;
            this.WeightsBox.CurrentCellChanged += WeightsBoxOnCellEditEnding;

        }

        private void WeightsBoxOnCellEditEnding(object sender, EventArgs dataGridCellEditEndingEventArgs)
        {
            ((Neuron) NeuronsBox.SelectedItem).Weights = Weights.Select(v => v.Weight).ToArray();
            foreach (var weightViewModel in ((Neuron) NeuronsBox.SelectedItem).Weights)
            {
                Console.WriteLine(weightViewModel);
            }
            Console.WriteLine(((WeightViewModel)WeightsBox.Items[0]).Weight);
        }

        private void NeuronsBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (NeuronsBox.SelectedItem != null)
            {
                this.Weights.Clear();
                var items = ((Neuron) NeuronsBox.SelectedItem).Weights.Select(v => new WeightViewModel(v));
                foreach (var weightViewModel in items)
                {
                    Weights.Add(weightViewModel);
                }
                WeightsBox.ItemsSource = Weights;
                WeightsBox.Items.Refresh();
            }
        }

        private void LayersBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            try
            {
                this.NeuronsBox.ItemsSource = this.Network.Layers[LayersBox.SelectedIndex].Neurons;
                
            }
            catch (Exception)
            {
                this.NeuronsBox.ItemsSource = null;

            }
            NeuronsBox.Items.Refresh();
        }


        public Network Network
        {
            get
            {
                return this.network;
            }
            private set
            {
                this.network = value;
                this.LayersBox.ItemsSource = this.Network.Layers;
                LayersBox.Items.Refresh();
            }
        }
        public Layer[] Layers {
            get
        {
                if (Network == null)
                {
                    return new Layer[0];
                }
            return Network.Layers;

        }}

        public ObservableCollection<WeightViewModel> Weights { get; private set; }

        private void FileMenuOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "NewNetwork":
                    var newNetworkWindow = new NewNetworkWindow();
                    newNetworkWindow.ShowDialog();
                    if (newNetworkWindow.Network != null)
                    {
                        this.Network = newNetworkWindow.Network;
                    }
                     
                    break;
                case "OpenNetwork":
                    this.OpenNetworkFromFile();
                    break;
                case "SaveNetwork":
                    this.SaveNetworkToFile();
                    break;
            }
        }
        private void SaveNetworkToFile()
        {
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "network"; 
            dlg.DefaultExt = ".xml"; 
            dlg.Filter = "XML documents (.xml)|*.xml"; 

            bool? result2 = dlg.ShowDialog();

            if (result2 == true)
            {
                NeuronalNetworkSerializer.SerializeToXml(this.Network, dlg.FileName);
            }
        }

        private void OpenNetworkFromFile()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "network";
            dlg.DefaultExt = ".xml"; 
            dlg.Filter = "XML documents (.xml)|*.xml"; 

          
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                this.Network = NeuronalNetworkSerializer.DeserializeFromXmlFile(dlg.FileName);
            }
        }


        private void DataMenuOnClick(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.Source as MenuItem;

            switch (mi.Name)
            {
                case "OpenFromFile":
                    var newNetworkWindow = new RunWindow(Network);
                    newNetworkWindow.Show();

                    break;
            }
        }

        private void LearnMenuOnClick(object sender, RoutedEventArgs e)
        {
            var learningWindow = new LearningWindow(Network);
            learningWindow.Show();
        }

        private void RandomizeNetwork(object sender, RoutedEventArgs e)
        {
            double min = MinBox.Text != null ? double.Parse(MinBox.Text, CultureInfo.InvariantCulture) : 0.0;
            double max = MaxBox.Text != null ? double.Parse(MaxBox.Text, CultureInfo.InvariantCulture) : 0.0;

            network.Randomize(new DoubleRange(min, max));
        }
    }


}
