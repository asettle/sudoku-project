using PuzzleSolver.ApplicationLogic.Domains;
using PuzzleSolver.Utilities;
using System;
using System.Linq;

namespace PuzzleSolver.Operators
{
    internal class CrossoverOperator
    {
        internal (Chromosome, Chromosome) Apply(Chromosome parent1, Chromosome parent2)
        {
            var rnd = new Random();
            if (rnd.NextDouble() > decimal.ToDouble(SettingsHelper.CrossoverProbability))
            {
                return (parent1, parent2);
            }
            (var child1, var child2) = (parent1.Clone(), parent2.Clone());
            Enumerable.Range(0, rnd.Next(16)).ToList().ForEach(r =>
            {
                child1.Genes[r] = parent2.Genes[r].Clone();
            });
            Enumerable.Range(0, rnd.Next(16)).ToList().ForEach(r =>
            {
                child2.Genes[r] = parent1.Genes[r].Clone();
            });
            return (child1, child2);
        }
    }
}
