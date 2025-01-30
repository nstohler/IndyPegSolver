﻿
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
        Name = "Indy Game Board 1_2",
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
            { '-', '-', '-', '-', '-', '-', '-', '-', 'O', 'O', '-', '-', '-' },
            { '-', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-' },
            { '-', 'O', 'O', 'O', '-', 'O', 'O', '-', 'O', '-', 'O', 'O', 'O' },
            { 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O' },
            { 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', '-', 'O' },
            { '-', 'O', 'O', 'O', '-', 'O', 'O', '-', 'O', 'O', 'O', '-', 'O' },
            { 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-', 'O' },
            { 'O', '-', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', '-', 'O' },
            { 'O', 'O', 'O', 'O', '-', '-', 'O', 'O', '-', 'O', 'O', 'O', '-' }
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
            { '-', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
            { '-', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O' },
            { '-', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', '-', 'O' },
            { 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', '-' },
            { 'O', 'O', '-', 'O', 'O', '-', 'O', '-', 'O', '-', '-', 'O', 'O', 'O' },
            { '-', '-', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O' },
            { '-', 'O', 'O', 'O', '-', '-', 'O', '-', '-', '-', 'O', 'O', 'O', '-' },
            { '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O' },
            { '-', 'O', '-', 'O', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', 'O' }
        }
    };

    public static BoardMetadata IndyGameBoard4 = new BoardMetadata
    {
        Name = "Indy Game Board 4",
        Description = "Official Indy Game Board 4",
        ExamplePegPlacementSolutions = new List<string>
        {
            // no solutions available yet
        },
        BestSolutionPegCount = 14,
        MinimumPegsToStart = 13,
        Board = new char[,]
        {
            { '-', 'O', 'O', 'O', 'O', 'O', 'O', 'O', '-', '-', 'O', '-', '-', '-' },
            { '-', 'O', '-', 'O', 'O', '-', 'O', 'O', '-', 'O', 'O', 'O', '-', 'O' },
            { '-', '-', 'O', 'O', 'O', '-', '-', 'O', 'O', 'O', '-', 'O', '-', 'O' },
            { '-', '-', 'O', '-', 'O', 'O', 'O', '-', 'O', '-', 'O', '-', 'O', 'O' },
            { '-', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', 'O', 'O', 'O', 'O' },
            { '-', 'O', '-', '-', '-', 'O', 'O', 'O', 'O', 'O', '-', '-', 'O', '-' },
            { 'O', 'O', 'O', 'O', 'O', '-', 'O', '-', 'O', 'O', '-', 'O', 'O', '-' },
            { 'O', '-', 'O', '-', 'O', 'O', '-', '-', 'O', 'O', 'O', '-', 'O', 'O' },
            { '-', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O', 'O', '-', 'O', 'O', 'O' }
        }
    };

    // Add more hardcoded boards here
}
