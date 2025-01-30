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
        var solver = new GameSolver(new BruteForceStrategy());

        var gameStateSolutions = solver.Solve(gameState);

        if (gameStateSolutions.Count() > 0)
        {
            Console.WriteLine("Solution found:");
            foreach (var solutionGameState in gameStateSolutions)
            {
                Console.WriteLine(solutionGameState.GetPegPlacementInOrderString());
            }
            Console.WriteLine("Best solution:");
            Console.WriteLine(gameStateSolutions.Last().GetPegPlacementInOrderString());
        }
        else
        {
            Console.WriteLine("No solution found.");
        }
    }
}