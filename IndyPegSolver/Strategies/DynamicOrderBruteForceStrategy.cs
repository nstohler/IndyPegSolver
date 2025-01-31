using System.Collections.Generic;
using System.Linq;

public class DynamicOrderBruteForceStrategy : IStrategy
{
    // not a good strategy so far, needs more work to be useful

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
                //Console.WriteLine($"    {gameState.Rating} - {gameState.GetSortedPegPlacementString()}");
            }

            solutions.Add(gameState.Clone());
            //return true; // keep on searching for better/more solutions
        }

        if (gameState.PegPlacements.Count() >= bestPegCount && gameState.Rating.UnfilledHolesCount >= bestUnfilledHoles)
        {
            return false; // Fast fail if the current solution is worse than the best found so far
        }

        var possiblePlacements = GetPossiblePegPlacements(gameState).ToList();

        for (int i = 0; i < possiblePlacements.Count; i++)
        {
            var pegPlacement = possiblePlacements[i];
            gameState.AddPegPlacement(pegPlacement);
            if (Solve(gameState, bestGameState, solutions, ref bestPegCount, ref bestUnfilledHoles))
            {
                return true;
            }
            gameState.RemovePegPlacement(pegPlacement);

            // Rotate the order of peg placements
            //possiblePlacements = RotateList(possiblePlacements);
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
                    possiblePlacements.Add(new PegPlacement(position, SlotState.Right));
                    possiblePlacements.Add(new PegPlacement(position, SlotState.Left));                    
                }
            }
        }

        return possiblePlacements;
    }

    private List<PegPlacement> RotateList(List<PegPlacement> list)
    {
        if (list.Count == 0)
        {
            return list;
        }

        var firstElement = list[0];
        list.RemoveAt(0);
        list.Add(firstElement);

        return list;
    }
}