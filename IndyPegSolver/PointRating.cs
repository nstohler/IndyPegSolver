using System.Collections.Generic;

public class PointRating
{
    public Point HolePosition { get; }
    public List<PegPlacement> Fillers { get; } // TODO: sort later when PegPlacementRating is here!
    public List<PegPlacementRating> PegPlacementRatings { get; }

    public int Rating => Fillers.Count; // Rating is the number of fillers, so low rating means less options to get this point filled

    public PointRating(Point holePosition)
    {
        HolePosition = holePosition;
        Fillers = new List<PegPlacement>();
        PegPlacementRatings = new List<PegPlacementRating>();
    }

    public void AddFiller(PegPlacement filler)
    {
        Fillers.Add(filler);
    }

    public override string ToString()
    {
        return $"Rating: {Rating}, Hole: {HolePosition}, Fillers: [{string.Join(", ", Fillers)}]";
    }

    public void AddPegPlacementRatings(List<PegPlacementRating> ratings)
    {
        foreach (var rating in ratings)
        {
            if (Fillers.Contains(rating.PegPlacement))
            {
                PegPlacementRatings.Add(rating);
            }
        }
        PegPlacementRatings.Sort(new PegPlacementRatingComparer()); // in-place sort fine here?        
    }
}