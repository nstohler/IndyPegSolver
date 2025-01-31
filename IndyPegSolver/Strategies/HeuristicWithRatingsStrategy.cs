using System.Collections.Generic;
using System.Linq;

public class HeuristicWithRatingsStrategy : IStrategy
{
    private readonly int targetPegCount;

    public HeuristicWithRatingsStrategy(int targetPegCount)
    {
        this.targetPegCount = targetPegCount;
    }

    public List<GameState> FindSolution(GameState gameState)
    {
        var solutions = new List<GameState>();
        var bestGameState = gameState.Clone();
        var bestPegCount = int.MaxValue;
        var bestUnfilledHoles = int.MaxValue;
        Solve(gameState, bestGameState, solutions, ref bestPegCount, ref bestUnfilledHoles);
        return solutions;
    }

    private bool Solve(GameState gameState, GameState bestGameState, List<GameState> solutions, ref int bestPegCount, ref int bestUnfilledHoles)
    {
        //Console.WriteLine($"trying - {gameState.GetSortedPegPlacementString()}");

        if (gameState.PegPlacements.Count() < bestPegCount && gameState.Rating.UnfilledHolesCount <= bestUnfilledHoles)
        {
            Console.WriteLine($"{gameState.Rating} - {gameState.GetSortedPegPlacementString()}");
        }

        if (gameState.CurrentBoard.IsSolved())
        {
            if (bestPegCount > gameState.PegPlacements.Count())
            {
                solutions.Clear(); // only keep the best solutions
                bestGameState = gameState.Clone();

                bestPegCount = gameState.PegPlacements.Count();
                bestUnfilledHoles = gameState.Rating.UnfilledHolesCount;

                Console.WriteLine($"Solution: {gameState.Rating} - {gameState.GetSortedPegPlacementString()}");

                if (bestPegCount > targetPegCount)
                    Console.WriteLine($"-> looking for better solutions still...");
            }

            solutions.Add(gameState.Clone());

            // Continue searching for better solutions
            if (bestPegCount <= targetPegCount)
            {
                return true; // Stop if we found a solution with the target peg count or fewer
            }
        }

        if (gameState.PegPlacements.Count() >= bestPegCount && gameState.Rating.UnfilledHolesCount >= bestUnfilledHoles)
        {
            return false; // Fast fail if the current solution is worse than the best found so far
        }

        // TODO: use the following rating generation methods to prioritize peg placements
        // gameState.CurrentBoard.GeneratePegPlacementRatings();
        // gameState.CurrentBoard.GeneratePointRatings();
        var pegPlacementRatings = gameState.CurrentBoard.GeneratePegPlacementRatings();

        var possiblePlacements = pegPlacementRatings
            .Select(r => r.PegPlacement)            
            .ToList();

        //---
        //var possiblePlacements = GetPossiblePegPlacements(gameState)
        //    .OrderByDescending(p => EvaluatePlacement(gameState, p))
        //    .ToList();

        foreach (var pegPlacement in possiblePlacements)
        {
            gameState.AddPegPlacement(pegPlacement);
            if (Solve(gameState, bestGameState, solutions, ref bestPegCount, ref bestUnfilledHoles))
            {
                return true;
            }
            gameState.RemovePegPlacement(pegPlacement);
        }

        return false;
    }
}