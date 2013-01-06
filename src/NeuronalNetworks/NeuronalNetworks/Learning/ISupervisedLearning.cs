namespace NeuronalNetworks.Learning
{
    public interface ISupervisedLearning
    {
        double Run(double[] input, double[] output);

        double RunEpoch(double[][] input, double[][] output, int steps);
        double RunEpoch(double[][] input, double[][] output);
        double LearningRate { get; set; }
    }
}
