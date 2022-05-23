using PuzzleSolver.ApplicationLogic.Domains;
using PuzzleSolver.Utilities;
using System.Linq;

namespace PuzzleSolver.Operators
{
    internal class ElitismOperator
    {
        public void Apply(Population population)
        {
            var numberOfChromosomes = (int)(SettingsHelper.ElitismQuotient * SettingsHelper.PopulationCapacity);
            var relevantChromosomes = population.Chromosomes.OrderByDescending(r => r.Fitness).Take(numberOfChromosomes);
            var eliteChromosomes = relevantChromosomes.Select(r => r.Clone()).ToList();
            population.Chromosomes.Clear();
            eliteChromosomes.ForEach(r => population.Chromosomes.Add(r));
        }
    }
}
