using System.Collections.Generic;

public class PegPlacementDirectionPositionComparer : IComparer<PegPlacement>
{
    public int Compare(PegPlacement x, PegPlacement y)
    {
        // Explicitly compare SlotState values: Right comes before Left
        int stateComparison = x.State == SlotState.Right ? -1 : (x.State == SlotState.Left ? 1 : 0);
        int otherStateComparison = y.State == SlotState.Right ? -1 : (y.State == SlotState.Left ? 1 : 0);
        int result = stateComparison.CompareTo(otherStateComparison);

        if (result != 0)
        {
            return result;
        }

        int xComparison = x.Position.X.CompareTo(y.Position.X);
        if (xComparison != 0)
        {
            return xComparison;
        }

        return x.Position.Y.CompareTo(y.Position.Y);
    }
}