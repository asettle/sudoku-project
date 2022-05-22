using PuzzleSolver.Utilities;
using System.Collections.Generic;

namespace PuzzleSolver.ApplicationLogic.Domains
{
    internal sealed class Population
    {
        private static Population _instance = null;
        private readonly List<Chromosome> _chromosomes = new List<Chromosome>();

        internal static Population CreateInstance(Chromosome initialChromosome)
        {
            if (_instance == null)
            {
                _instance = new Population(initialChromosome);
            }
            return _instance;
        }

        private Population(Chromosome initialChromosome)
        {
            _chromosomes.Add(initialChromosome);
            while (_chromosomes.Count < SettingsHelper.PopulationCapacity)
            {
                _chromosomes.Add(initialChromosome.Clone().Randomise());
            }
        }
    }
}
