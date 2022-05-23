using PuzzleSolver.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.ApplicationLogic.Domains
{
    internal sealed class Population
    {
        public List<Chromosome> Chromosomes = new List<Chromosome>();

        public Population(Chromosome initialChromosome)
        {
            Chromosomes.Add(initialChromosome);
            while (Chromosomes.Count < SettingsHelper.PopulationCapacity)
            {
                Chromosomes.Add(initialChromosome.Clone().Randomise());
            }
        }

        public Chromosome GetFittestChromosome() => Chromosomes.OrderByDescending(r => r.Fitness).FirstOrDefault();
    }
}
