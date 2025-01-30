using System.Collections.Generic;

public class PegPlacementPositionDirectionComparer : IComparer<PegPlacement>
{
    public int Compare(PegPlacement x, PegPlacement y)
    {
        int xComparison = x.Position.X.CompareTo(y.Position.X);
        if (xComparison != 0)
        {
            return xComparison;
        }

        int yComparison = x.Position.Y.CompareTo(y.Position.Y);
        if (yComparison != 0)
        {
            return yComparison;
        }

        return x.State.CompareTo(y.State);
    }
}