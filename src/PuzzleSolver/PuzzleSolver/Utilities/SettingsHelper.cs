﻿using System.Collections.Generic;

namespace PuzzleSolver.Utilities
{
    internal static class SettingsHelper
    {
        internal static int PopulationCapacity { get; set; }
        internal static double ElitismQuotient { get; set; }
        internal static double MutationProbability { get; set; }
        internal static double DiversityQuotient { get; set; }
        internal static double CrossoverProbability { get; set; }
        internal static int TournamentSize { get; set; }
        internal static int MaxIterations { get; set; }
        internal static int OriginallySetPositions { get; set; }
        internal static List<int> IndicesOfNonFixedPositions { get; set; }
    }
}