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

        public List<Gene> GetRowComponent(int rowIndex) => Enumerable.Range(0, 4).ToList().Select(r => Genes[4 * rowIndex + r]).ToList();

        public List<Gene> GetColumnComponent(int columnIndex) => Enumerable.Range(0, 4).ToList().Select(r => Genes[columnIndex + 4 * r]).ToList();

        public List<Gene> GetSubGridComponent(int rowIndex, int columnIndex)
        {
            if (rowIndex < 2 && columnIndex < 2)
            {
                return GetSubGridComponent(0);
            }
            else if (rowIndex < 2 && columnIndex > 1)
            {
                return GetSubGridComponent(1);
            }
            else if (rowIndex > 1 && columnIndex < 2)
            {
                return GetSubGridComponent(2);
            }
            return GetSubGridComponent(3);
        }

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
        public bool HasError(List<Gene> genes, int index) =>
            !genes[index].IsOriginallySet && genes.Count(t => t.Value == genes[index].Value) != 1;

        private int CalculateRowRelatedErrorPoints() =>
            Enumerable.Range(0, 4).ToList().Select(r =>
            {
                var row = GetRowComponent(r);
                return (-1) * Enumerable.Range(0, 4).ToList().Select(s => HasError(row, s)).Count(s => s == true);
            }).Sum();

        private int CalculateColumnRelatedErrorPoints() =>
            Enumerable.Range(0, 4).ToList().Select(r =>
            {
                var column = GetColumnComponent(r);
                return (-1) * Enumerable.Range(0, 4).ToList().Select(s => HasError(column, s)).Count(s => s == true);
            }).Sum();

        private int CalculateSubGridRelatedErrorPoints() =>
            Enumerable.Range(0, 4).ToList().Select(r =>
            {
                var subGrid = GetSubGridComponent(r);
                return (-1) * Enumerable.Range(0, 4).ToList().Select(s => HasError(subGrid, s)).Count(s => s == true);
            }).Sum();
        #endregion
    }
}
