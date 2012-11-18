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
using NeuronalNetworks.Common;
using NeuronalNetworks.Networks;

namespace NeuronalNetworks.Gui
{
    /// <summary>
    /// Interaction logic for RunWindow.xaml
    /// </summary>
    public partial class RunWindow : Window
    {
        public RunWindow(Network network)
        {
            InitializeComponent();
            this.Network = network;
        }

        protected Network Network { get; set; }

        private void OpenAndRun(object sender, RoutedEventArgs e)
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
                double[] inputs = new double[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    inputs[i] = double.Parse(lines[i], CultureInfo.InvariantCulture);
                }

                Input.Text = "Input: \n";
                this.Input.Text += input;

                Network.Compute(inputs);

                Output.Text = "Output: \n";

                foreach (var output in Network.Output)
                {
                    Output.Text += output;
                    Output.Text += "\n";
                }

            }
        }
    }
}
