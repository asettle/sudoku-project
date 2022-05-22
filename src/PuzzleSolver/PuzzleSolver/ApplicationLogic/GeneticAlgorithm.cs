using PuzzleSolver.ApplicationLogic.Domains;
using PuzzleSolver.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.ApplicationLogic
{
    internal sealed class GeneticAlgorithm
    {
        public GeneticAlgorithm(int populationCapacity, double elitismQuotient, double mutationProbability, 
            double diversityQuotient, double crossoverProbability, int tournamentSize, int maxIterations)
        {
            SettingsHelper.PopulationCapacity = populationCapacity;
            SettingsHelper.ElitismQuotient = elitismQuotient;
            SettingsHelper.MutationProbability = mutationProbability;
            SettingsHelper.DiversityQuotient = diversityQuotient;
            SettingsHelper.CrossoverProbability = crossoverProbability;
            SettingsHelper.TournamentSize = tournamentSize;
            SettingsHelper.MaxIterations = maxIterations;
        }

        public List<char> Solve(List<char> puzzleTask)
        {
            var solution = new List<int>();
            var mapping = MappingHelper.MapToNumbers(puzzleTask, out List<int> puzzleTaskInt);
            SettingsHelper.IndicesOfNonFixedPositions = puzzleTaskInt.Select((value, index) => (index, value)).Where(r => r.value == 0).Select(r => r.index).ToList();
            var initialGenes = puzzleTaskInt.Select(r => new Gene(r, r != 0)).ToList();
            var initialChromosome = new Chromosome(initialGenes);
            var population = Population.CreateInstance(initialChromosome);
            return MappingHelper.MapToCharacters(solution, mapping);
        }
    }
}
