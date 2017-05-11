using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    //https://en.wikipedia.org/wiki/Fitness_proportionate_selection
    class StochasticAcceptanceSelector : Selector
    {

        public List<NeuralNetwork> Select(Population population, int amount)
        {
            double max = population.Fittest.Fitness;
            List<NeuralNetwork> selected = new List<NeuralNetwork>();
            for(int i = 0; i < amount; i++)
            {
                bool found = false;
                int index = 0;
                while (!found)
                {
                    index = Helper.GetRandomInt(population.Size);
                    if (Helper.GetRandom() < population[index].Fitness)
                        found = true;
                }
                selected.Add(population[i]);
            }

            return selected;
        }
    }
}
