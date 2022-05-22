namespace PuzzleSolver.ApplicationLogic.Domains
{
    internal sealed class Gene
    {
        public int Value { get; set; }
        public bool IsOriginallySet { get; set; }

        public Gene(int value, bool isOriginallySet = false)
        {
            Value = value;
            IsOriginallySet = isOriginallySet;
        }

        public Gene Clone() => new Gene(Value, IsOriginallySet);
    }
}
