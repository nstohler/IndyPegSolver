//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

public class ExampleUsage
{
    public static void Main(string[] args)
    {
        //var initialBoard = new Board(new char[,]
        //{
        //    { 'O', 'O', 'O', 'O' },
        //    { 'O', 'O', '-', 'O' },
        //    { 'O', 'O', '-', 'O' },
        //    { '-', 'O', 'O', 'O' },
        //    { '-', '-', 'O', 'O' },
        //    { '-', 'O', 'O', '-' },
        //    { '-', '-', 'O', '-' },
        //    { '-', '-', 'O', '-' },
        //    { 'O', 'O', 'O', 'O' }
        //});
        var board = HardcodedBoards.TestBoard1;

        var gameState = new GameState(new Board(board.Board));
        var solver = new GameSolver(new BruteForceStrategy(1));

        var solution = solver.Solve(gameState);

        if (solution != null)
        {
            Console.WriteLine("Solution found:");
            foreach (var pegPlacement in solution)
            {
                Console.WriteLine(pegPlacement);
            }
        }
        else
        {
            Console.WriteLine("No solution found.");
        }
    }
}