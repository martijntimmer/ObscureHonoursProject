using System;

namespace ObscureHonoursProject
{
    internal class MyProblemFitness
    {
        NeuralNetwork network;
        public MyProblemFitness()
        {
            network = new NeuralNetwork(2, 1);
            
   
        }

        public double Evaluate(NeuralNetwork network)
        {
            double[][] inputData = { new double[] { 0, 0 }, new double[] { 1, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 } };
            int[] outputData = { 0, 1, 1, 0 };
            double error = 0;
            for (int setIndex = 0; setIndex < 4; setIndex++)
            {
                double[] output = network.Evaluate(inputData[setIndex]);
                double de = Math.Abs(output[0] - outputData[setIndex]);
                error += Math.Abs(output[0] - outputData[setIndex]);
            }
            return 1 / error;
        }
    }
}