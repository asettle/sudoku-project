using System;
using System.Collections.Generic;

namespace PuzzleSolver.ApplicationLogic
{
    internal class GeneticAlgorithm
    {
        private readonly int _populationCapacity;
        private readonly double _elitismQuotient;
        private readonly double _mutationProbability;
        private readonly double _diversityQuotient;
        private readonly double _crossoverProbability;
        private readonly int _tournamentSize;
        private readonly int _maxIterations;

        public GeneticAlgorithm(int populationCapacity, double elitismQuotient, double mutationProbability, 
            double diversityQuotient, double crossoverProbability, int tournamentSize, int maxIterations)
        {
            _populationCapacity = populationCapacity;
            _elitismQuotient = elitismQuotient;
            _mutationProbability = mutationProbability;
            _diversityQuotient = diversityQuotient;
            _crossoverProbability = crossoverProbability;
            _tournamentSize = tournamentSize;
            _maxIterations = maxIterations;
        }

        public List<char> Solve(List<char> puzzleTask)
        {
            throw new NotImplementedException();
        }
    }
}
