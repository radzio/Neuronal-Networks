using System;
using System.Globalization;
using System.Windows;
using NeuronalNetworks.Learning;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.Gui
{
    /// <summary>
    /// Interaction logic for LearningWindow.xaml
    /// </summary>
    public partial class BPLearningWindow : Window
    {
        private Network network;
        private BackPropagationLearning bpLearning;
        private double[][] inputs;
        private double[][] outputs;
        public BPLearningWindow(Network network)
        {
            InitializeComponent();
            this.network = network;
            this.bpLearning = new BackPropagationLearning((ActivationNetwork)network);
        }

        private void GetSamples(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "input";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Txt documents (.txt)|*.txt";


            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string input = System.IO.File.ReadAllText(dlg.FileName);
                string[] lines = input.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                inputs = new double[lines.Length][];
                outputs = new double[lines.Length][];
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var parts = line.Split('|');

                    var ipts = parts[0];

                    var outps = parts[1];
                    
                    var values = ipts.Split(' ');
                    var values2 = outps.Split(' ');
                    var y = 0;
                    inputs[i] = new double[values.Length];
                    outputs[i] = new double[values2.Length];
                    foreach (var value in values)
                    {
                        inputs[i][y] = double.Parse(value, CultureInfo.InvariantCulture);
                        y++;
                    }

                    y = 0;
                    
                    foreach (var value in values2)
                    {
                        outputs[i][y] = double.Parse(value, CultureInfo.InvariantCulture);
                        y++;
                    }
                    
                }

            }
        }

        private void StartLearn(object sender, RoutedEventArgs e)
        {
            this.bpLearning.Momentum = MomentumBox.Text != null
                                 ? double.Parse(MomentumBox.Text, CultureInfo.InvariantCulture)
                                 : 0.0;

            this.bpLearning.LearningRate = AlphaBox.Text != null
                                 ? double.Parse(AlphaBox.Text, CultureInfo.InvariantCulture)
                                 : 0.0;
                                 

            int steps = int.Parse(StepsBox.Text);

            for (int i = 0; i < steps; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    var input = inputs[j];
                    var output = outputs[j];
                    this.bpLearning.Run(input, output);
                }
                
            }
        }
    }
}
