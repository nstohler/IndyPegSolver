using System.Collections.Generic;

public class PegPlacementRatingComparer : IComparer<PegPlacementRating>
{
    private readonly IComparer<PegPlacement> _pegPlacementComparer;

    public PegPlacementRatingComparer(IComparer<PegPlacement> pegPlacementComparer)
    {
        _pegPlacementComparer = pegPlacementComparer;
    }

    public PegPlacementRatingComparer()
    {
        _pegPlacementComparer = new PegPlacementPositionDirectionComparer();        
        //_pegPlacementComparer = new PegPlacementDirectionPositionComparer();        
    }

    public int Compare(PegPlacementRating x, PegPlacementRating y)
    {
        //// org
        int ratingComparison = y.Rating.CompareTo(x.Rating); // Sort by rating in descending order
        if (ratingComparison != 0)
        {
            return ratingComparison;
        }

        return _pegPlacementComparer.Compare(x.PegPlacement, y.PegPlacement);

        //// TODO: still ok? needs testing?
        //// let's give some bonus to R-pegs over L-pegs
        //var bonusForRPegs = 2;
        //var xRating = x.Rating + (x.PegPlacement.State == SlotState.Right ? bonusForRPegs : 0);
        //var yRating = y.Rating + (y.PegPlacement.State == SlotState.Right ? bonusForRPegs : 0);
        //int ratingComparison = yRating.CompareTo(xRating); // Sort by rating in descending order
        //if (ratingComparison != 0)
        //{
        //    return ratingComparison;
        //}

        //return _pegPlacementComparer.Compare(x.PegPlacement, y.PegPlacement);
    }
}