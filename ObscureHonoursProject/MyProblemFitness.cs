using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

namespace ObscureHonoursProject
{
    internal class MyProblemFitness : IFitness
    {
        NeuralNetwork network;
        public MyProblemFitness()
        {
            network = new NeuralNetwork(2, 1);
        }

        public double Evaluate(IChromosome chromosome)
        {
             double[] weights = new double[chromosome.Length];
             for(int i = 0; i < chromosome.Length; i++)
             {
                 weights[i] = (double)chromosome.GetGene(i).Value;
             }
             network.UpdateWeights(weights, 0);
             int[][] inputData = { new int[] { 0, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } };
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