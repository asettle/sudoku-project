using PuzzleSolver.ApplicationLogic.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.Utilities
{
    internal static class ConsoleHelper
    {
        public static void OutputStatus(int genSeqNo, Chromosome chromosome, Dictionary<int, char> mapping)
        {
            var currentForegroundColor = Console.ForegroundColor;
            Console.WriteLine($"+ + + Generation no. {genSeqNo} + + +");
            Console.WriteLine();
            Enumerable.Range(0, 4).ToList().ForEach(r =>
            {
                Enumerable.Range(0, 4).ToList().ForEach(s =>
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    var currentGene = chromosome.Genes[4 * r + s];
                    var row = chromosome.GetRowComponent(r);
                    var column = chromosome.GetColumnComponent(s);
                    var subGrid = chromosome.GetSubGridComponent(r, s);
                    var hasRowError = chromosome.HasError(row, s);
                    var hasColumnError = chromosome.HasError(column, r);
                    var hasSubGridError = chromosome.HasError(subGrid, s);
                    if (currentGene.IsOriginallySet)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (hasRowError || hasColumnError || hasSubGridError)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write(mapping[currentGene.Value]);
                    Console.ForegroundColor = currentForegroundColor;
                    Console.Write(" ");
                });
                Console.WriteLine();
            });
            Console.WriteLine();
        }
    }
}