using PuzzleSolver.ApplicationLogic.Domains;
using PuzzleSolver.Utilities;
using System;
using System.Linq;

namespace PuzzleSolver.Operators
{
    internal class TournamentOperator
    {
        internal (Chromosome, Chromosome) Apply(Population population)
        {
            var rnd = new Random();
            Chromosome parent1 = null;
            Chromosome parent2 = null;

            Enumerable.Range(0, SettingsHelper.TournamentSize).ToList().ForEach(r =>
            {
                var rndParent1Index = rnd.Next(population.Chromosomes.Count);
                var rndParent2Index = rnd.Next(population.Chromosomes.Count);
                while (rndParent1Index == rndParent2Index)
                {
                    rndParent2Index = rnd.Next(population.Chromosomes.Count);
                }
                if (parent1 == null || parent2 == null)
                {
                    parent1 = population.Chromosomes[rndParent1Index];
                    parent2 = population.Chromosomes[rndParent2Index];
                }
                else
                {
                    parent1 = parent1.Fitness > population.Chromosomes[rndParent1Index].Fitness ? parent1 : population.Chromosomes[rndParent1Index];
                    parent2 = parent2.Fitness > population.Chromosomes[rndParent2Index].Fitness ? parent2 : population.Chromosomes[rndParent2Index];
                }
            });
            return (parent1, parent2);            
        }
    }
}
