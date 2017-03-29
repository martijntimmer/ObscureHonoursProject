using System;
using GeneticSharp.Domain.Chromosomes;

namespace ObscureHonoursProject
{
    class MyProblemChromosome : ChromosomeBase
    {
        bool isFirst = true;
        public MyProblemChromosome(int length) : base(length)
        {
            CreateGenes();
            isFirst = false;
        }

        public override IChromosome CreateNew()
        {
            return new MyProblemChromosome(Length);
        }

        public override Gene GenerateGene(int geneIndex)
        {
            double val;
             if (isFirst)
               val = Helper.GetRandom() * 30 - 15;
            else val = (double)GetGene(geneIndex).Value + Helper.GetRandom() * 5 - 2.5;//Helper.GetRandom() * 5 - 2.5;

            return new Gene(val);
        }
    }
}