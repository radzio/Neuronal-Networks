namespace NeuronalNetworks.ActivationFunctions
{
    public class LinearFunction : ActivationFunction
    {

        public double A { get; set; }
        public double B { get; set; }
        
        
        public LinearFunction()
        {
            
        }

        public override double Function(double x)
        {
           return  (A * x) + B;
        }

        public override double Derivative(double x)
        {
            return A;
        }

        public override double Derivative2(double y)
        {
            return 0.0;
        }
    }
}