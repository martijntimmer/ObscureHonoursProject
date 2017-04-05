using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class Population
    {
        public NeuralNetwork[] individuals;

        NeuralNetwork fittest;
        public NeuralNetwork Fittest
        {
            get
            {
              //  if (fittest != null)
               //     return fittest;

                fittest = individuals[0];
                for (int i = 0; i < individuals.Length; i++)
                {
                    if (fittest.Fitness < individuals[i].Fitness)
                        fittest = individuals[i];
                }
                return fittest;
            }
        }

        public int Size
        {
            get { return individuals.Length; }
        }

        public Population(NeuralNetwork[] individuals)
        {
            this.individuals = individuals;
        }

        public Population(int amount, params NeuralNetwork[] individuals)
        {
            this.individuals = individuals;
        }

        public NeuralNetwork this[int i]
        {
            get { return individuals[i]; }
        }

        public void Evaluate(double[] input)
        {
            foreach (NeuralNetwork indiv in individuals)
                indiv.Evaluate(input);
        }
    }
}
