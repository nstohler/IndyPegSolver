using System.Collections.Generic;

public struct PegPlacementRating
{
    public PegPlacement PegPlacement { get; }
    public List<Point> FilledPoints { get; } // TODO: sorting? 
    
    public int Rating => FilledPoints.Count;

    public PegPlacementRating(PegPlacement pegPlacement)
    {
        PegPlacement = pegPlacement;
        FilledPoints = new List<Point>();
    }
    
    public void AddFilledPoint(Point point)
    {
        FilledPoints.Add(point);
    }

    public override string ToString()
    {
        return $"PegPlacement: {PegPlacement}, FilledPoints: [{string.Join(", ", FilledPoints)}], Rating: {Rating}";
    }
}