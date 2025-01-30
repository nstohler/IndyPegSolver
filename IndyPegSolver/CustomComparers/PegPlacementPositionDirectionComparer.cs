using System.Collections.Generic;

public class PegPlacementPositionDirectionComparer : IComparer<PegPlacement>
{
    public int Compare(PegPlacement x, PegPlacement y)
    {
        int xComparison = x.Point.X.CompareTo(y.Point.X);
        if (xComparison != 0)
        {
            return xComparison;
        }

        int yComparison = x.Point.Y.CompareTo(y.Point.Y);
        if (yComparison != 0)
        {
            return yComparison;
        }

        return x.State.CompareTo(y.State);
    }
}