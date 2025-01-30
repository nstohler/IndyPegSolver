using System.Collections.Generic;

public class BruteForceStrategy : IStrategy
{
    private int goalPegCount;

    public BruteForceStrategy(int goalPegCount)
    {
        this.goalPegCount = goalPegCount;
    }

    public List<PegPlacement> FindSolution(GameState gameState)
    {
        var solution = new List<PegPlacement>();
        if (Solve(gameState, solution))
        {
            return solution;
        }
        return new List<PegPlacement>(); // No solution found
    }

    private bool Solve(GameState gameState, List<PegPlacement> solution)
    {
        Console.WriteLine($"{gameState.Rating} - {gameState.GetSortedPegPlacementString()}");

        if (gameState.CurrentBoard.IsSolved() && solution.Count == goalPegCount)
        {
            return true;
        }
        
        foreach (var pegPlacement in GetPossiblePegPlacements(gameState))
        {
            gameState.AddPegPlacement(pegPlacement);
            solution.Add(pegPlacement);

            if (Solve(gameState, solution))
            {
                return true;
            }

            solution.Remove(pegPlacement);
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
                    possiblePlacements.Add(new PegPlacement(position, SlotState.Left));
                    possiblePlacements.Add(new PegPlacement(position, SlotState.Right));
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