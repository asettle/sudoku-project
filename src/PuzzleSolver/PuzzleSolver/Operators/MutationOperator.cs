using PuzzleSolver.ApplicationLogic.Domains;
using PuzzleSolver.Utilities;
using System;
using System.Linq;

namespace PuzzleSolver.Operators
{
    internal class MutationOperator
    {
        internal void Apply(Chromosome chromosome)
        {
            var rnd = new Random();
            if (rnd.NextDouble() <= decimal.ToDouble(SettingsHelper.MutationProbability))
            {
                var rndIndexOfNonFixedPositions = SettingsHelper.IndicesOfNonFixedPositions.OrderBy(r => rnd.Next()).First();
                chromosome.Genes[rndIndexOfNonFixedPositions].Value = rnd.Next(4) + 1;
            }
        }
    }
}
