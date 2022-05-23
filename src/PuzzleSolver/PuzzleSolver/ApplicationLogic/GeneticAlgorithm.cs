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
        private readonly CrossoverOperator _crossoverOperator;

        public GeneticAlgorithm(int populationCapacity, decimal elitismQuotient, double mutationProbability, 
            double diversityQuotient, decimal crossoverProbability, int tournamentSize, int maxIterations)
        {
            _elitismOperator = new ElitismOperator();
            _tournamentOperator = new TournamentOperator();
            _crossoverOperator = new CrossoverOperator();
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
            Chromosome globalBestChromosome = new Chromosome(Enumerable.Repeat(new Gene(0), 16).ToList());
            while (generationSequenceNo < SettingsHelper.MaxIterations)
            {
                var currentBestChromosome = population.GetFittestChromosome();
                if (currentBestChromosome.Fitness > globalBestChromosome.Fitness)
                {
                    globalBestChromosome = currentBestChromosome;
                    ConsoleHelper.OutputStatus(generationSequenceNo, globalBestChromosome, mapping);
                    if (globalBestChromosome.Fitness == 0)
                    {
                        globalBestChromosome.Genes.Select(r => r.Value).ToList().ForEach(r => solution.Add(r));
                        break;
                    }
                }
                _elitismOperator.Apply(population);
                while (population.Chromosomes.Count < SettingsHelper.PopulationCapacity)
                {
                    (var parent1, var parent2) = _tournamentOperator.Apply(population);
                    (var child1, var child2) = _crossoverOperator.Apply(parent1, parent2);
                }
                generationSequenceNo++;
            }
            return MappingHelper.MapToCharacters(solution, mapping);
        }
    }
}
