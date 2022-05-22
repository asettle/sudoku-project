using System.Collections.Generic;
using System.Linq;

namespace PuzzleSolver.Utilities
{
    internal static class MappingHelper
    {
        public static Dictionary<int, char> MapToNumbers(List<char> puzzleTask, out List<int> mappedPuzzleTask)
        {
            var mapping = new Dictionary<int, char>();
            var distinctCharacters = puzzleTask.Distinct().ToList();
            distinctCharacters.Remove('\0');
            mapping.Add(0, '\0');
            distinctCharacters.Select((r, s) => new { character = r,  index = s }).ToList().ForEach(r => 
            {
                mapping.Add(r.index + 1, r.character);
            });
            mappedPuzzleTask = new List<int>();
            foreach (char character in puzzleTask)
            {
                mappedPuzzleTask.Add(mapping.Keys.Where(r => r == character).First());
            }
            return mapping;
        }

        public static List<char> MapToCharacters(List<int> solution, Dictionary<int, char> mapping) => solution.Select(r => mapping[r]).ToList();
    }
}
