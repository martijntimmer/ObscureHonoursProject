using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject.Neural_Network
{
    class Mutation
    {
        double mutationRate;

        public Mutation(double rate)
        {
            this.mutationRate = rate;
        }
        public double[] Mutate(double[] input)
        {
            double[] output = new double[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                if (Helper.GetRandom() < mutationRate)
                    output[i] += Helper.GetRandom() * 5 - 2.5;
                else output[i] = input[i];
            }
            return output;
        }
    }
}
