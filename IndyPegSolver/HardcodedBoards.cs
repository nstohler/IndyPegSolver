
public static class HardcodedBoards
{
    public static BoardMetadata IndyGameBoard1_1 = new BoardMetadata
    {
        Name = "IndyGameBoard1_1",
        Description = "Official Indy Game Board 1-1",
        ExamplePegPlacementSolutions = new List<string>
        {
            "1-1-L|0-3-R|4-2-L|8-2-R",
            "8-2-R|0-3-R|2-1-L|5-1-L",
            "8-2-R|0-3-R|2-1-L|5-1-R",
            "5-2-R|8-0-R|0-3-R|2-1-L",
        },
        BestSolutionPegCount = 4,
        MinimumPegsToStart = 3,
        Board = new char[,]
        {
            { 'O', 'O', 'O', 'O' },
            { 'O', 'O', '-', 'O' },
            { 'O', 'O', '-', 'O' },
            { '-', 'O', 'O', 'O' },
            { '-', '-', 'O', 'O' },
            { '-', 'O', 'O', '-' },
            { '-', '-', 'O', '-' },
            { '-', '-', 'O', '-' },
            { 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata IndyGameBoard1_2 = new BoardMetadata
    {
        Name = "IndyGameBoard1_2",
        Description = "Official Indy Game Board 1-2",
        ExamplePegPlacementSolutions = new List<string>
        {
            "0-4-R|1-2-R|4-1-R|6-6-L|7-8-R|8-3-R|8-0-R"
        },
        BestSolutionPegCount = 4,
        MinimumPegsToStart = 3,
        Board = new char[,]
        {
            { 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', '-' },
            { 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-' },
            { 'O', '-', 'O', '-', 'O', '-', '-', '-', 'O' },
            { 'O', 'O', 'O', '-', 'O', '-', '-', '-', 'O' },
            { 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
            { 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O' },
            { 'O', '-', 'O', 'O', '-', '-', 'O', 'O', 'O' },
            { 'O', '-', 'O', 'O', '-', 'O', 'O', 'O', 'O' },
            { 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', '-' }
        }
    };

    // Add more hardcoded boards here
}
