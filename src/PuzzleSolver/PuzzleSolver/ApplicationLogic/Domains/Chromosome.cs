using PuzzleSolver.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.ApplicationLogic.Domains
{
    internal sealed class Chromosome
    {
        public int Fitness { get; private set; }
        public List<Gene> Genes { get; set; }

        public Chromosome(List<Gene> genes)
        {
            Genes = genes;
            Fitness = ComputeFitnessFuction();
        }

        public Chromosome Clone()
        {
            var clonedGenes = Genes.Select(r => r.Clone()).ToList();
            return new Chromosome(Genes);
        }

        public Chromosome Randomise() 
        {
            var rnd = new Random();
            SettingsHelper.IndicesOfNonFixedPositions.ForEach(r => Genes[r].Value = rnd.Next(4) + 1);
            return this;
        }

        private List<Gene> GetRowComponent(int rowIndex) => Enumerable.Range(0, 4).ToList().Select(r => Genes[4 * rowIndex + r]).ToList();

        private List<Gene> GetColumnComponent(int columnIndex) => Enumerable.Range(0, 4).ToList().Select(r => Genes[columnIndex + 4 * r]).ToList();

        private List<Gene> GetSubGridComponent(int subGridIndex)
        {
            int[] indices = null;
            switch (subGridIndex)
            {
                case 0:
                    indices = new[] { 0, 1, 4, 5 };
                    break;
                case 1:
                    indices = new[] { 2, 3, 6, 7 };
                    break;
                case 2:
                    indices = new[] { 8, 9, 12, 13 };
                    break;
                case 3:
                    indices = new[] { 10, 11, 14, 15 };
                    break;
            }
            return indices.Select(r => Genes[r]).ToList();
        }

        private int ComputeFitnessFuction()
        {
            if (Genes.Any(r => r.Value == 0))
            {
                return int.MinValue;
            }
            return CalculateRowRelatedErrorPoints() + CalculateColumnRelatedErrorPoints() + CalculateSubGridRelatedErrorPoints();
        }

        #region Evaluation of Puzzle Rule Violations
        private int CalculateRowRelatedErrorPoints() =>
            Enumerable.Range(0, 4).ToList().Select(r =>
            {
                var row = GetRowComponent(r);
                return row.Select(s => s.IsOriginallySet || row.Count(t => t == s) == 1 ? 0 : -1).Sum();
            }).Sum();

        private int CalculateColumnRelatedErrorPoints() =>
            Enumerable.Range(0, 4).ToList().Select(r =>
            {
                var column = GetColumnComponent(r);
                return column.Select(s => s.IsOriginallySet || column.Count(t => t == s) == 1 ? 0 : -1).Sum();
            }).Sum();

        private int CalculateSubGridRelatedErrorPoints() =>
            Enumerable.Range(0, 4).ToList().Select(r =>
            {
                var subGrid = GetSubGridComponent(r);
                return subGrid.Select(s => s.IsOriginallySet || subGrid.Count(t => t == s) == 1 ? 0 : -1).Sum();
            }).Sum();
        #endregion
    }
}
