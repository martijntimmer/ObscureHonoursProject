using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class Neuron
    {
        static Random random = new Random();
        Neuron[] connections;
        double[] weight;
        public double Value;
        
        public Neuron()
        {

        }

        public Neuron(Neuron[] connections)
        {
            this.connections = connections;
            weight = new double[connections.Length + 1];
            RandomizeWeights();
        }
        
        public void RandomizeWeights()
        {
            for (int i = 0; i < weight.Length; i++)
            {
                weight[i] = random.NextDouble() * 20 - 10;
            }
        }        

        public void Evaluate()
        {
            Value = 0;
            for (int i = 0; i < connections.Length; i++)
            {
                Value += connections[i].Value * weight[i];
            }
            Value += 1 * weight[weight.Length -1]; // Bias
            Value = 1.0 / (1 + Math.Exp(-Value));
        }        
       
        /// <returns>Position where next can start writing</returns>
        public int WriteWeights(double[] output, int start)
        {
            if (weight == null)
                return start;
            Array.Copy(weight, 0, output, start, weight.Length);
            return start + weight.Length;
        }

        public int UpdateWeights(double[] input, int start)
        {
            if (weight == null)
                return start;
            Array.Copy(input, start, weight, 0, weight.Length);
            return start + weight.Length;
        }
    }    
}
