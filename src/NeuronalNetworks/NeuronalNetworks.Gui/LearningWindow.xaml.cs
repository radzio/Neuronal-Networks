using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NeuronalNetworks.Learning;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.Gui
{
    /// <summary>
    /// Interaction logic for LearningWindow.xaml
    /// </summary>
    public partial class LearningWindow : Window
    {
        private Network network;
        private SOMLearning somLearning;
        private double[][] inputs;
        public LearningWindow(Network network)
        {
            InitializeComponent();
            this.network = network;
            this.somLearning = new SOMLearning((KohonenNetwork) network);
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
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var values = line.Split(' ');
                    var y = 0;
                    inputs[i] = new double[values.Length];

                    foreach (var value in values)
                    {
                        inputs[i][y] = double.Parse(value, CultureInfo.InvariantCulture);
                        y++;
                    }
                    
                }

            }
        }

        private void StartLearn(object sender, RoutedEventArgs e)
        {
            somLearning.LearningRadius = NeighborhoodBox.Text != null
                                             ? double.Parse(NeighborhoodBox.Text, CultureInfo.InvariantCulture)
                                             : 0.0;

            somLearning.ConscienceValue = ConscienceBox.Text != null
                                 ? double.Parse(ConscienceBox.Text, CultureInfo.InvariantCulture)
                                 : 0.0;

            somLearning.LearningRate = AlphaBox.Text != null
                                 ? double.Parse(AlphaBox.Text, CultureInfo.InvariantCulture)
                                 : 0.0;

            int steps = int.Parse(StepsBox.Text);

            for (int i = 0; i < steps; i++)
            {
                foreach (var input in inputs)
                {
                    somLearning.Run(input);
                }
            }
        }
    }
}
