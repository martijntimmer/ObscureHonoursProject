using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Comment
            NeuralNetwork[] individuals = new NeuralNetwork[100];
            for (int i = 0; i < individuals.Length; i++) individuals[i] = new NeuralNetwork(2, 1);
            int[][] inputData = { new int[] { 0, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } };
            int[] outputData = { 0, 1, 1, 0 };
            //bruteforceBenchmark(inputData, outputData);
            //            Population population = new Population(individuals);
            /* Selector selector = new StochasticAcceptanceSelector();
             Crossover crossover = new UniformCrossover(0.5);
             Mutation mutation = new Mutation(0.3);    */
            var selection = new TournamentSelection();
            var crossover = new UniformCrossover();
            var mutation = new UniformMutation();
            var fitness = new MyProblemFitness();
            var chromosome = new MyProblemChromosome(9);
            var population = new Population(80, 100, chromosome);
            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new FitnessThresholdTermination(7);
            ga.MutationProbability = 1f;
            ga.GenerationRan += delegate
            {
                MyProblemChromosome bestChromosome = (MyProblemChromosome)ga.Population.BestChromosome;
                Console.WriteLine("Generations: {0}", ga.Population.GenerationsNumber);
                Console.WriteLine("Fitness: {0,10}", bestChromosome.Fitness);
                Console.WriteLine("Time: {0}", ga.TimeEvolving);
                Console.WriteLine("------------");
            };

            ga.Start();
            Console.WriteLine("Best solution found has {0} fitness.", ga.BestChromosome.Fitness);
            //double minError = double.MaxValue;
            int steps = 0;

            Console.ReadKey();
        }

        private static void bruteforceBenchmark(int[][] inputData, int[] outputData)
        {
            NeuralNetwork reference = new NeuralNetwork(2, 1);
            double[] weights = new double[] { -6.60, 6.48, -3.48, -6.22, 6.42, 3.12, 10.79, -10.53, 5.04 };
            reference.UpdateWeights(weights, 0);
            double[] out1 = reference.Evaluate(new int[] { 0, 0 });
            double[] out2 = reference.Evaluate(new int[] { 1, 0 });
            double[] out3 = reference.Evaluate(new int[] { 0, 1 });
            double[] out4 = reference.Evaluate(new int[] { 1, 1 });

            double minError = double.MaxValue;
            int tries = 0;
            while (true)
            {
                tries++;
                double[] rndmWeights = new double[9];
                for (int i = 0; i < 9; i++)
                    rndmWeights[i] = Helper.GetRandom() * 30 - 15;
                reference.UpdateWeights(rndmWeights, 0);

                double error = 0;
                for (int setIndex = 0; setIndex < 4; setIndex++)
                {
                    double[] output = reference.Evaluate(inputData[setIndex]);
                    error += Math.Abs(output[0] - outputData[setIndex]);
                }

                if (error < minError)
                {
                    Console.WriteLine($"In {tries}: {error}");
                    minError = error;
                }
            }
        }
    }
}
