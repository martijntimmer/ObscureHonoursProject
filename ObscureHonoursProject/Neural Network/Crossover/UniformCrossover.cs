using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject.Neural_Network
{
    class UniformCrossover : Crossover
    {
        double mixProbability;

        public UniformCrossover(double mixProbability)
        {
            this.mixProbability = mixProbability;
        }

        public double[][] Cross(double[] weightsP1, double[] weightsP2)
        {
            double[][] children = new double[2][];
            children[0] = new double[weightsP1.Length];
            children[1] = new double[weightsP2.Length];
            for (int i = 0; i < weightsP1.Length; i++)
            {
                if (Helper.GetRandom() < mixProbability)
                {
                    children[0][i] = weightsP2[i];
                    children[1][i] = weightsP1[i];
                }
                else
                {
                    children[0][i] = weightsP1[i];
                    children[1][i] = weightsP2[i];
                }
            }
            return children;
        }
    }
}
