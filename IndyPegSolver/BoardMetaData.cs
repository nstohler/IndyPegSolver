
public class BoardMetadata
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<string> ExampleSolutions { get; set; } = new List<string>();

    public int BestSolutionPegCount { get; set; }
    public int MinimumPegsToStart { get; set; }
    
    public char[,] Board { get; set; } = new char[0, 0];
}