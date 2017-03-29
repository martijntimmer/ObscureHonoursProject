using GeneticSharp.Domain.Mutations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;

namespace ObscureHonoursProject
{
    class MyMutation : MutationBase
    {
        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            for (int i = 0; i < chromosome.Length; i++)
                chromosome.ReplaceGene(i, chromosome.GenerateGene(i));
        }
    }
}
