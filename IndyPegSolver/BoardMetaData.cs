
public class BoardMetadata : IEquatable<BoardMetadata>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<string> ExamplePegPlacementSolutions { get; set; } = new List<string>();

    public int BestSolutionPegCount { get; set; }
    public int MinimumPegsToStart { get; set; }
    
    public char[,] Board { get; set; } = new char[0, 0];

    public bool Equals(BoardMetadata? other)
    {
        if (other == null) return false;

        return Name == other.Name &&
               Description == other.Description &&
               ExamplePegPlacementSolutions.SequenceEqual(other.ExamplePegPlacementSolutions) &&
               BestSolutionPegCount == other.BestSolutionPegCount &&
               MinimumPegsToStart == other.MinimumPegsToStart &&
               Board.Rank == other.Board.Rank &&
               Enumerable.Range(0, Board.Rank).All(d => Board.GetLength(d) == other.Board.GetLength(d)) &&
               Board.Cast<char>().SequenceEqual(other.Board.Cast<char>());
    }

    public override bool Equals(object? obj)
    {
        if (obj is BoardMetadata other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Name);
        hash.Add(Description);
        foreach (var solution in ExamplePegPlacementSolutions)
        {
            hash.Add(solution);
        }
        hash.Add(BestSolutionPegCount);
        hash.Add(MinimumPegsToStart);
        foreach (var value in Board)
        {
            hash.Add(value);
        }
        return hash.ToHashCode();
    }    
}