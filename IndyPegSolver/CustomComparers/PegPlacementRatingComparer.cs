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
    }

    public int Compare(PegPlacementRating x, PegPlacementRating y)
    {
        int ratingComparison = y.Rating.CompareTo(x.Rating); // Sort by rating in descending order
        if (ratingComparison != 0)
        {
            return ratingComparison;
        }

        return _pegPlacementComparer.Compare(x.PegPlacement, y.PegPlacement);
    }
}