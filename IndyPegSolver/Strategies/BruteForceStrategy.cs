using System.Collections.Generic;

public class BruteForceStrategy : IStrategy
{
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
            }
            
            solutions.Add(gameState.Clone());
            //return true; // keep on searching for better/more solutions
        }

        if (gameState.PegPlacements.Count() >= bestPegCount && gameState.Rating.UnfilledHolesCount >= bestUnfilledHoles)
        {
            return false; // Fast fail if the current solution is worse than the best found so far
        }

        foreach (var pegPlacement in GetPossiblePegPlacements(gameState))
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

    private IEnumerable<PegPlacement> GetPossiblePegPlacements(GameState gameState)
    {
        var possiblePlacements = new List<PegPlacement>();

        for (int x = 0; x < gameState.CurrentBoard.Width; x++)
        {
            for (int y = 0; y < gameState.CurrentBoard.Height; y++)
            {
                var position = new Point(x, y);
                if (gameState.CurrentBoard.GetSlotState(position) == SlotState.Hole)
                {
                    // try mixing l/r placements
                    var lastPlacedPegState = (gameState.PegPlacements.Any()) ? gameState.PegPlacements.Last().State : SlotState.Left;
                    if (lastPlacedPegState == SlotState.Left)
                    {
                        possiblePlacements.Add(new PegPlacement(position, SlotState.Right));
                        possiblePlacements.Add(new PegPlacement(position, SlotState.Left));
                    }
                    else
                    {
                        possiblePlacements.Add(new PegPlacement(position, SlotState.Left));
                        possiblePlacements.Add(new PegPlacement(position, SlotState.Right));
                    }

                    // original code
                    //possiblePlacements.Add(new PegPlacement(position, SlotState.Right));
                    //possiblePlacements.Add(new PegPlacement(position, SlotState.Left));
                }
            }
        }

        return possiblePlacements;
    }
}
// {
//     public List<PegPlacement> FindSolution(GameState gameState)
//     {
//         var solution = new List<PegPlacement>();
//         if (Solve(gameState, solution))
//         {
//             return solution;
//         }
//         return new List<PegPlacement>(); // No solution found
//     }

//     private bool Solve(GameState gameState, List<PegPlacement> solution)
//     {
//         if (gameState.CurrentBoard.IsSolved())
//         {
//             return true;
//         }

//         foreach (var pegPlacement in GetPossiblePegPlacements(gameState))
//         {
//             gameState.AddPegPlacement(pegPlacement);
//             solution.Add(pegPlacement);

//             if (Solve(gameState, solution))
//             {
//                 return true;
//             }

//             solution.Remove(pegPlacement);
//             gameState.RemovePegPlacement(pegPlacement);
//         }

//         return false;
//     }

//     private IEnumerable<PegPlacement> GetPossiblePegPlacements(GameState gameState)
//     {
//         var possiblePlacements = new List<PegPlacement>();

//         for (int x = 0; x < gameState.CurrentBoard.Width; x++)
//         {
//             for (int y = 0; y < gameState.CurrentBoard.Height; y++)
//             {
//                 var position = new Point(x, y);
//                 if (gameState.CurrentBoard.GetSlotState(position) == SlotState.Hole)
//                 {
//                     possiblePlacements.Add(new PegPlacement(position, SlotState.Left));
//                     possiblePlacements.Add(new PegPlacement(position, SlotState.Right));
//                 }
//             }
//         }

//         return possiblePlacements;
//     }
// }