using System.Collections.Generic;

public class PointRating
{
    public Point HolePosition { get; }
    public List<PegPlacement> Fillers { get; } // TODO: sort later when PegPlacementRating is here!

    public PointRating(Point holePosition)
    {
        HolePosition = holePosition;
        Fillers = new List<PegPlacement>();
    }

    public void AddFiller(PegPlacement filler)
    {
        Fillers.Add(filler);
    }

    public override string ToString()
    {
        return $"Hole: {HolePosition}, Fillers: [{string.Join(", ", Fillers)}]";
    }
}