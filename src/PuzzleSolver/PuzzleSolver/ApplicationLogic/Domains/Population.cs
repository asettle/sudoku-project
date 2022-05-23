using PuzzleSolver.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.ApplicationLogic.Domains
{
    internal sealed class Population
    {
        private readonly List<Chromosome> _chromosomes = new List<Chromosome>();

        public Population(Chromosome initialChromosome)
        {
            _chromosomes.Add(initialChromosome);
            while (_chromosomes.Count < SettingsHelper.PopulationCapacity)
            {
                _chromosomes.Add(initialChromosome.Clone().Randomise());
            }
        }

        public Chromosome GetFittestChromosome() => _chromosomes.OrderByDescending(r => r.Fitness).FirstOrDefault();
    }
}
