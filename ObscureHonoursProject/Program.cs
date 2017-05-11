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
            //MartijnTest();
            //ToonTest();
            RealMainEatCheese();
        }

        private static void RealMainEatCheese()
        {
            TimedSearcher searcher = new TimedSearcher();
            ZobristHasher zobristHasher = new ZobristHasher(9, 9, 2);
            UTTTState currentState = null;

            int timePerMove;
            int timebank;
            String field = null;
            String macroboard = null;
            bool weArePlayerOne = false;

            while (Console.KeyAvailable)
            {
                String line = Console.In.ReadLine();
                if (line.Length == 0) { continue; }
                String[] parts = line.Split(' ');
                switch (parts[0])
                {
                    case "settings":
                        switch (parts[1])
                        {
                            case "time_per_move":
                                timePerMove = int.Parse(parts[2]);
                                break;
                            case "timebank":
                                timebank = int.Parse(parts[2]);
                                break;
                            case "field":
                                field = parts[2];
                                break;
                            case "macroboard":
                                macroboard = parts[2];
                                break;
                            case "your_botid":
                                weArePlayerOne = (int.Parse(parts[2]) == 1);
                                break;
                        }
                        break;
                    case "update":
                        switch (parts[2])
                        {
                            case "field":
                                field = parts[2];
                                break;
                            case "macroboard":
                                macroboard = parts[2];
                                break;
                        }
                        break;
                    case "action":
                        currentState = new UTTTState(field, macroboard, zobristHasher, weArePlayerOne);
                        UTTTMove chosen = searcher.FindBestMove(currentState, 10000);
                        Console.WriteLine($"place_move {chosen.x} {chosen.y}");
                        Console.Out.Flush();
                        break;
                    default:
                        // error
                        break;
                }
            }
        }

        private static void ToonTest()
        {
            UTTTState state = new UTTTState(
                "0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
                "-1, -1, 0, 0, 0, 0, 0, 0, 0");
            UTTTMove[] moveArray = state.GetPossibleMoves().ToArray();
            foreach (UTTTMove move in moveArray)
            {
                Console.WriteLine(move.ToString());
            }
            Console.ReadKey();
        }

        private static void MartijnTest()
        {
            // Comment
            NeuralNetwork[] individuals = new NeuralNetwork[100];
            for (int i = 0; i < individuals.Length; i++) individuals[i] = new NeuralNetwork(2, 1);
            int[][] inputData = { new int[] { 0, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 } };
            int[] outputData = { 0, 1, 1, 0 };
            //bruteforceBenchmark(inputData, outputData);
            Population population = new Population(individuals);
            Selector selector = new StochasticAcceptanceSelector();
            Crossover crossover = new UniformCrossover(0.5);
            Mutation mutation = new Mutation(0.3);            
            MyProblemFitness fitness = new MyProblemFitness();
            foreach (NeuralNetwork net in population.individuals)
            {
                net.Fitness = fitness.Evaluate(net);
            }
            int numIters = 0;
            while (population.Fittest.Fitness < 80)
            {
                numIters++;
                List<NeuralNetwork> newPop = new List<NeuralNetwork>();
                for(int i = 0; i < 100; i+=2)
                {
                    List<NeuralNetwork> parents = selector.Select(population, 2);
                    double[][] newWeights = crossover.Cross(parents[0].WriteWeights(), parents[1].WriteWeights());
                    newWeights[0] = mutation.Mutate(newWeights[0]);
                    newWeights[1] = mutation.Mutate(newWeights[1]);
                    NeuralNetwork child1 = new NeuralNetwork(2, 1);
                    NeuralNetwork child2 = new NeuralNetwork(2, 1);
                    child1.UpdateWeights(newWeights[0], 0);
                    child2.UpdateWeights(newWeights[1], 0);
                 
                    newPop.Add(child1);
                    newPop.Add(child2);                   
                }
                newPop.Add(population.Fittest);
                population.individuals = newPop.ToArray();
                foreach (NeuralNetwork net in population.individuals)
                {
                    net.Fitness = fitness.Evaluate(net);
                }
                if (population.Fittest.Fitness > 30)
                    Console.WriteLine($"Fittest: {population.Fittest.Fitness}");
            }
            Console.WriteLine($"NUmber of iterations {numIters}");
            int steps = 0;
            double dx = 0.1;
            double dy = 0.1;

            NeuralNetwork network = population.Fittest;
            for (double y = 0; y <= 1; y += dy)
            {
                for (double x = 0; x <= 1; x += dx)
                {
                    double[] output = network.Evaluate(new double[] { x,y });
                    if (output[0] < 0.4)
                        Console.Write("-");
                    else if (output[0] > 0.6)
                        Console.Write("+");
                    else Console.Write("?");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            Main(null);
        }

    /*    private static void bruteforceBenchmark(int[][] inputData, int[] outputData)
        {
            NeuralNetwork reference = new NeuralNetwork(2, 1);
            double[] weights = new double[] { -6.60, 6.48, -3.48, -6.22, 6.42, 3.12, 10.79, -10.53, 5.04 };
            reference.UpdateWeights(weights, 0);
            double[] out1 = reference.Evaluate(new double[] { 0, 0 });
            double[] out2 = reference.Evaluate(new double[] { 1, 0 });
            double[] out3 = reference.Evaluate(new double[] { 0, 1 });
            double[] out4 = reference.Evaluate(new double[] { 1, 1 });

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
        }*/
    }
}
