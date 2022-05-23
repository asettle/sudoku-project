using PuzzleSolver.ApplicationLogic.Domains;
using PuzzleSolver.Operators;
using PuzzleSolver.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.ApplicationLogic
{
    internal sealed class GeneticAlgorithm
    {
        private readonly ElitismOperator _elitismOperator;
        private readonly TournamentOperator _tournamentOperator;

        public GeneticAlgorithm(int populationCapacity, decimal elitismQuotient, double mutationProbability, 
            double diversityQuotient, double crossoverProbability, int tournamentSize, int maxIterations)
        {
            _elitismOperator = new ElitismOperator();
            _tournamentOperator = new TournamentOperator();
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
            var population = new Population(initialChromosome);
            var generationSequenceNo = 1;
            Chromosome bestChromosome = new Chromosome(Enumerable.Repeat(new Gene(0), 16).ToList());
            while (bestChromosome.Fitness < 0 && generationSequenceNo < SettingsHelper.MaxIterations)
            {
                bestChromosome = population.GetFittestChromosome();
                if (bestChromosome.Fitness == 0)
                {
                    ConsoleHelper.OutputStatus(generationSequenceNo, bestChromosome, mapping);
                    bestChromosome.Genes.Select(r => r.Value).ToList().ForEach(r => solution.Add(r));
                }
                _elitismOperator.Apply(population);
                while (population.Chromosomes.Count < SettingsHelper.PopulationCapacity)
                {
                    (var parent1, var parent2) = _tournamentOperator.Apply(population);
                }
                generationSequenceNo++;
            }
            return MappingHelper.MapToCharacters(solution, mapping);
        }
    }
}
