﻿//// See https://aka.ms/new-console-template for more information
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

        var boardsToSolve = new List<BoardMetadata>()
        {
            HardcodedBoards.IndyGameBoard1_1,
            HardcodedBoards.IndyGameBoard1_2,
            HardcodedBoards.IndyGameBoard2
        };

        foreach (var board in boardsToSolve)
        {

            // board 1_2                        
            var gameState = new GameState(new Board(board.Board));

            //var solver = new GameSolver(new BruteForceStrategy());
            //var solver = new GameSolver(new DynamicOrderBruteForceStrategy());
            //var solver = new GameSolver(new HeuristicStrategy(7));
            var solver = new GameSolver(new HeuristicWithRatingsStrategy(board.BestSolutionPegCount));

            var gameStateSolutions = solver.Solve(gameState);

            if (gameStateSolutions.Count() > 0)
            {
                //Console.WriteLine("Solution found:");
                //foreach (var solutionGameState in gameStateSolutions)
                //{
                //    Console.WriteLine(solutionGameState.GetPegPlacementInOrderString());
                //}
                //Console.WriteLine();
                //Console.WriteLine($"Best solution variations found: {gameStateSolutions.Count()}");
                //Console.WriteLine("Best solution example:");
                //Console.WriteLine(gameStateSolutions.Last().GetPegPlacementInOrderString());

                Console.WriteLine();
                HashSet<string> uniqueSolutions = new HashSet<string>(gameStateSolutions.Select(s => s.GetSortedPegPlacementString()));
                Console.WriteLine($"Unique solution variations found: {uniqueSolutions.Count()}");
                Console.WriteLine("some unique solutions:");
                foreach (var solution in uniqueSolutions.Take(int.Min(uniqueSolutions.Count(), 6)))
                {
                    Console.WriteLine(solution);
                }
            }
            else
            {
                Console.WriteLine("No solution found.");
            }
        }

    }
}