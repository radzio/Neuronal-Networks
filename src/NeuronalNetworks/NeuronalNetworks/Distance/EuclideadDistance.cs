using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuronalNetworks.Distance
{
    public class EuclideanDistance
    {

        public static double Distance(List<Double> v1, List<Double> v2)
        {
            double res = v1.Select((t, i) => (t - v2[i])*(t - v2[i])).Sum();

            return Math.Sqrt(res);
        }
    }
}