using System.Collections.Generic;

public class PointRatingComparer : IComparer<PointRating>
{
    private readonly IComparer<Point> _pointComparer;

    public PointRatingComparer(IComparer<Point> pointComparer)
    {
        _pointComparer = pointComparer;
    }

    public PointRatingComparer()
    {
        _pointComparer = new PointComparer();
    }

    public int Compare(PointRating? x, PointRating? y)
    {
        if (x == null || y == null)
        {
            return Comparer<PointRating?>.Default.Compare(x, y);
        }

        int ratingComparison = y.Rating.CompareTo(x.Rating); // Sort by rating in descending order
        if (ratingComparison != 0)
        {
            return ratingComparison;
        }

        return _pointComparer.Compare(x.HolePosition, y.HolePosition);
    }
}
