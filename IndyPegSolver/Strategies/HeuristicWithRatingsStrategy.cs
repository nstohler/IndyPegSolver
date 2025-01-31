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
        //Console.WriteLine($"trying - {gameState.GetPegPlacementInOrderString()}");

        if (bestPegCount < int.MaxValue 
            && bestUnfilledHoles < int.MaxValue
            && gameState.PegPlacements.Count() < bestPegCount 
            //&& gameState.PegPlacements.Count() <= bestPegCount 
            && gameState.Rating.UnfilledHolesCount <= bestUnfilledHoles)
        {
            Console.WriteLine($"{gameState.Rating} - {gameState.GetPegPlacementInOrderString()}");
        }

        if (gameState.CurrentBoard.IsSolved())
        {
            if (bestPegCount > gameState.PegPlacements.Count())
            {
                solutions.Clear(); // only keep the best solutions
                bestGameState = gameState.Clone();

                bestPegCount = gameState.PegPlacements.Count();
                bestUnfilledHoles = gameState.Rating.UnfilledHolesCount;

                Console.WriteLine($"Solution: {gameState.Rating} - {gameState.GetPegPlacementInOrderString()}");

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
        var pointRatings = gameState.CurrentBoard.GeneratePointRatings();

        var lPegHighRatingThreshold = 5;

        // new:
        // - use all pegPlacements with ratings > 8 first (8 is max of an L peg placement)
        // - then use the previous strategy 
        var highRatedPegPlacementRatings = pegPlacementRatings
            .Where(rating => rating.Rating > 8)            
            .ToList();
        var lowRatedPegPlacementRatings = pegPlacementRatings
            .Where(rating => rating.Rating <= 8)            
            .ToList();        

        foreach (var pointRating in pointRatings)
        {
            pointRating.AddPegPlacementRatings(pegPlacementRatings);
        }

        // buggy? consider the rating.PegPlacementRatings instead of Rating property?
        var flatPegPlacementRatings = pointRatings
            .SelectMany(pointRating => pointRating.PegPlacementRatings)
            .GroupBy(pegPlacementRating => pegPlacementRating.PegPlacement)
            .Select(group => group.First())
            .ToList();

        var highPointRatings = flatPegPlacementRatings
            .Where(rating => rating.Rating > lPegHighRatingThreshold)
            .ToList();
        var lowPointRatings = flatPegPlacementRatings
            .Where(rating => rating.Rating <= lPegHighRatingThreshold)
            .ToList();
        
        var seenPegPlacements = new HashSet<PegPlacement>();
        var possiblePlacements = new List<PegPlacement>();
        
        foreach (var pegPlacementRating in highPointRatings)
        {
            //foreach (var pegPlacementRating in pointRating.PegPlacementRatings)
            {
                if (seenPegPlacements.Add(pegPlacementRating.PegPlacement))
                {
                    possiblePlacements.Add(pegPlacementRating.PegPlacement);
                }
            }
        }

        foreach (var pegPlacementRating in highRatedPegPlacementRatings)
        {
            if (seenPegPlacements.Add(pegPlacementRating.PegPlacement))
            {
                possiblePlacements.Add(pegPlacementRating.PegPlacement);
            }
        }

        foreach (var pegPlacementRating in lowPointRatings)
        {
            //foreach (var pegPlacementRating in pointRating.PegPlacementRatings)
            {
                if (seenPegPlacements.Add(pegPlacementRating.PegPlacement))
                {
                    possiblePlacements.Add(pegPlacementRating.PegPlacement);
                }
            }
        }

        foreach (var pegPlacementRating in lowRatedPegPlacementRatings)
        {
            if (seenPegPlacements.Add(pegPlacementRating.PegPlacement))
            {
                possiblePlacements.Add(pegPlacementRating.PegPlacement);
            }
        }

        // foreach (var pointRating in pointRatings)
        // {
        //     foreach (var pegPlacementRating in pointRating.PegPlacementRatings)
        //     {
        //         if (seenPegPlacements.Add(pegPlacementRating.PegPlacement))
        //         {
        //             possiblePlacements.Add(pegPlacementRating.PegPlacement);
        //         }
        //     }
        // }

        // // combine the lists / disabled for now, didnt seem to help
        // possiblePlacements = highRatedPegPlacementRatings
        //     .Select(rating => rating.PegPlacement)
        //     .Concat(possiblePlacements)
        //     .ToList();

        // new idea:
        // fill x holes with lowest rating first
        // then fill the rest with the highest rating

        // old comment: add pointRating points as well at the end. needs more investigation if they should be included in their own PegPlacementRatings or not...

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