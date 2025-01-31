
public static class HardcodedBoards
{
    public static BoardMetadata IndyGameBoard1_1 = new BoardMetadata
    {
        Name = "Indy Game Board 1_1",
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
        /*         0    1    2    3   */ 
        /* 0 */ { 'O', 'O', 'O', 'O' },
        /* 1 */ { 'O', 'O', '-', 'O' },
        /* 2 */ { 'O', 'O', '-', 'O' },
        /* 3 */ { '-', 'O', 'O', 'O' },
        /* 4 */ { '-', '-', 'O', 'O' },
        /* 5 */ { '-', 'O', 'O', '-' },
        /* 6 */ { '-', '-', 'O', '-' },
        /* 7 */ { '-', '-', 'O', '-' },
        /* 8 */ { 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata IndyGameBoard1_2 = new BoardMetadata
    {
        Name = "Indy Game Board 1_2",
        Description = "Official Indy Game Board 1-2",
        ExamplePegPlacementSolutions = new List<string>
        {
            "0-4-R|1-2-R|4-1-R|6-6-L|7-8-R|8-3-R|8-0-R"
        },
        BestSolutionPegCount = 7,
        MinimumPegsToStart = 7,
        Board = new char[,]
        {
        /*         0    1    2    3    4    5    6    7    8   */ 
        /* 0 */ { 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', '-' },
        /* 1 */ { 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-' },
        /* 2 */ { 'O', '-', 'O', '-', 'O', '-', '-', '-', 'O' },
        /* 3 */ { 'O', 'O', 'O', '-', 'O', '-', '-', '-', 'O' },
        /* 4 */ { 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        /* 5 */ { 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O' },
        /* 6 */ { 'O', '-', 'O', 'O', '-', '-', 'O', 'O', 'O' },
        /* 7 */ { 'O', '-', 'O', 'O', '-', 'O', 'O', 'O', 'O' },
        /* 8 */ { 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', '-' }
        }
    };

    public static BoardMetadata IndyGameBoard2 = new BoardMetadata
    {
        Name = "Indy Game Board 2",
        Description = "Official Indy Game Board 2",
        ExamplePegPlacementSolutions = new List<string>
        {
            "6-3-L|4-5-R|3-6-R|5-9-L|1-8-R|2-1-R|3-12-R|8-0-R|1-10-L|8-7-R|7-10-L"
        },
        BestSolutionPegCount = 11,
        MinimumPegsToStart = 11,
        Board = new char[,]
        {
        /*         0    1    2    3    4    5    6    7    8    9   10   11   12    */ 
        /* 0 */ { '-', '-', '-', '-', '-', '-', '-', '-', 'O', 'O', '-', '-', '-' },
        /* 1 */ { '-', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-' },
        /* 2 */ { '-', 'O', 'O', 'O', '-', 'O', 'O', '-', 'O', '-', 'O', 'O', 'O' },
        /* 3 */ { 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O' },
        /* 4 */ { 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', '-', 'O' },
        /* 5 */ { '-', 'O', 'O', 'O', '-', 'O', 'O', '-', 'O', 'O', 'O', '-', 'O' },
        /* 6 */ { 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-', 'O' },
        /* 7 */ { 'O', '-', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', '-', 'O' },
        /* 8 */ { 'O', 'O', 'O', 'O', '-', '-', 'O', 'O', '-', 'O', 'O', 'O', '-' }
        }
    };

    public static BoardMetadata IndyGameBoard3 = new BoardMetadata
    {
        Name = "Indy Game Board 3",
        Description = "Official Indy Game Board 3",
        ExamplePegPlacementSolutions = new List<string>
        {
            "0-8-R|1-2-L|1-6-L|1-10-R|3-4-R|3-12-L|4-1-L|5-10-R|6-1-R|7-3-R|7-11-R|8-6-R|8-12-R"
        },
        BestSolutionPegCount = 13,
        MinimumPegsToStart = 12,
        Board = new char[,]
        {
        /*         0    1    2    3    4    5    6    7    8    9   10   11   12   13    */ 
        /* 0 */ { '-', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        /* 1 */ { '-', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O' },
        /* 2 */ { '-', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', '-', 'O' },
        /* 3 */ { 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', '-' },
        /* 4 */ { 'O', 'O', '-', 'O', 'O', '-', 'O', '-', 'O', '-', '-', 'O', 'O', 'O' },
        /* 5 */ { '-', '-', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
        /* 6 */ { '-', 'O', 'O', 'O', '-', '-', 'O', '-', '-', '-', 'O', 'O', 'O', '-' },
        /* 7 */ { '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O' },
        /* 8 */ { '-', 'O', '-', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata IndyGameBoard4 = new BoardMetadata
    {
        Name = "Indy Game Board 4",
        Description = "Official Indy Game Board 4",
        ExamplePegPlacementSolutions = new List<string>
        {
            "6-0-R|4-1-R|0-1-R|8-2-R|2-3-L|7-5-L|4-6-L|2-7-L|6-9-L|1-10-L|4-11-L|7-12-L|4-13-R|8-6-R",
            "6-0-R|4-1-R|0-1-R|8-2-R|2-3-L|7-5-L|4-6-L|2-7-L|6-9-L|1-10-L|4-11-L|7-12-L|4-13-R|8-8-R",
        },
        BestSolutionPegCount = 14,
        MinimumPegsToStart = 13,
        Board = new char[,]
        {
        /*         0    1    2    3    4    5    6    7    8    9   10   11   12   13    */ 
        /* 0 */ { '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-', 'O', '-', '-', '-' },
        /* 1 */ { '-', 'O', '-', 'O', 'O', '-', 'O', 'O', '-', 'O', 'O', 'O', '-', 'O' },
        /* 2 */ { '-', '-', 'O', 'O', 'O', '-', '-', 'O', 'O', 'O', '-', 'O', '-', 'O' },
        /* 3 */ { '-', '-', 'O', '-', 'O', 'O', 'O', '-', 'O', '-', 'O', '-', 'O', 'O' },
        /* 4 */ { '-', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O' },
        /* 5 */ { '-', 'O', '-', '-', '-', 'O', 'O', 'O', 'O', 'O', '-', '-', 'O', '-' },
        /* 6 */ { 'O', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', '-', 'O', 'O', '-' },
        /* 7 */ { 'O', '-', 'O', '-', 'O', 'O', '-', '-', 'O', 'O', 'O', '-', 'O', 'O' },
        /* 8 */ { '-', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata TestBoard1 = new BoardMetadata
    {
        Name = "Test Board 1",
        Description = "Board 1 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "1-1-L",            
        },
        BestSolutionPegCount = 1,
        MinimumPegsToStart = 1,
        Board = new char[,]
        {
            { 'O', 'O', 'O' },
            { 'O', 'O', 'O' },
            { 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata TestBoard2 = new BoardMetadata
    {
        Name = "Test Board 2",
        Description = "Board 2 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "1-1-R",
        },
        BestSolutionPegCount = 1,
        MinimumPegsToStart = 1,
        Board = new char[,]
        {
            { '-', 'O', '-' },
            { 'O', 'O', 'O' },
            { '-', 'O', '-' }
        }
    };

    public static BoardMetadata TestBoard3 = new BoardMetadata
    {
        Name = "Test Board 3",
        Description = "Board 3 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "1-1-L|2-3-R",
            "0-3-R|2-1-R",
            "1-1-L|1-3-L",
        },
        BestSolutionPegCount = 2,
        MinimumPegsToStart = 1,
        Board = new char[,]
        {
            { 'O', 'O', 'O', 'O' },
            { '-', 'O', '-', 'O' },
            { 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata TestBoard4 = new BoardMetadata
    {
        Name = "Test Board 4",
        Description = "Board 4 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "0-0-R|3-3-R",
            "0-3-R|3-0-R",            
        },
        BestSolutionPegCount = 2,
        MinimumPegsToStart = 1,
        Board = new char[,]
        {
            { 'O', 'O', 'O', 'O' },
            { 'O', '-', '-', 'O' },
            { 'O', '-', '-', 'O' },
            { 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata TestBoard5 = new BoardMetadata
    {
        Name = "Test Board 5",
        Description = "Board 5 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "0-0-R|4-4-R|2-2-L",
        },
        BestSolutionPegCount = 3,
        MinimumPegsToStart = 2,
        Board = new char[,]
        {
            { 'O', 'O', 'O', 'O', 'O' },
            { 'O', '-', 'O', '-', 'O' },
            { 'O', 'O', 'O', 'O', 'O' },
            { 'O', '-', 'O', '-', 'O' },
            { 'O', 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata TestBoard6 = new BoardMetadata
    {
        Name = "Test Board 6",
        Description = "Board 6 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "0-0-R|4-4-R|2-2-L",
        },
        BestSolutionPegCount = 3,
        MinimumPegsToStart = 2,
        Board = new char[,]
        {
            { 'O', 'O', 'O', 'O', 'O' },
            { 'O', 'O', '-', '-', 'O' },
            { 'O', '-', 'O', 'O', 'O' },
            { 'O', '-', 'O', '-', 'O' },
            { 'O', 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata TestBoard7 = new BoardMetadata
    {
        Name = "Test Board 7",
        Description = "Board 7 for unit tests",
        ExamplePegPlacementSolutions = new List<string>
        {
            "1-2-L|4-2-L|7-2-L|0-0-R",
            "1-2-L|4-2-L|7-3-L|8-0-R",
        },
        BestSolutionPegCount = 4,
        MinimumPegsToStart = 3,
        Board = new char[,]
        {
        /*         0    1    2    3   */ 
        /* 0 */ { 'O', '-', 'O', 'O' },
        /* 1 */ { 'O', 'O', 'O', 'O' },
        /* 2 */ { 'O', '-', 'O', '-' },
        /* 3 */ { 'O', 'O', 'O', '-' },
        /* 4 */ { 'O', 'O', 'O', 'O' },
        /* 5 */ { 'O', 'O', 'O', 'O' },
        /* 6 */ { 'O', '-', 'O', 'O' },
        /* 7 */ { 'O', '-', 'O', 'O' },
        /* 8 */ { 'O', 'O', '-', 'O' }
        }
    };


    // Add more hardcoded boards here
}
