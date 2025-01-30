using System.Collections.Generic;

public class GameState
{
    public List<PegPlacement> PegPlacements { get; }    
    public Board InitialBoard { get; }
    public Board CurrentBoard { get; private set; }
    public BoardRating Rating { get; private set; }
    public int InitialHolesCount => InitialBoard.CountUnfilledHoles();

    public GameState(Board initialBoard)
    {
        PegPlacements = new List<PegPlacement>();
        InitialBoard = initialBoard.Clone();
        CurrentBoard = initialBoard.Clone();
        UpdateRating();
    }

    public void AddPegPlacement(PegPlacement pegPlacement)
    {
        PegPlacements.Add(pegPlacement);
        CurrentBoard.PlacePeg(pegPlacement.Position, pegPlacement.State);
        UpdateRating();
    }

    public void RemovePegPlacement(PegPlacement pegPlacementToRemove)
    {
        PegPlacements.Remove(pegPlacementToRemove);
        // Rebuild the board state from the initial board
        CurrentBoard = InitialBoard.Clone();
        foreach (var placement in PegPlacements)
        {
            CurrentBoard.PlacePeg(placement.Position, placement.State);
        }
        UpdateRating();
    }

    private void UpdateRating()
    {
        int pegCount = PegPlacements.Count;
        int unfilledHolesCount = CurrentBoard.CountUnfilledHoles();
        Rating = new BoardRating(pegCount, unfilledHolesCount);
    }

    public override string ToString()
    {
        return $"GameState(Rating: {Rating})";
    }

    public string GetPegPlacementInOrderString()
    {
        return string.Join("|", PegPlacements);
    }

    public void SetPegPlacementsFromString(string pegPlacementString)
    {
        PegPlacements.Clear();
        var placements = pegPlacementString.Split('|');
        foreach (var placement in placements)
        {
            var pegPlacement = PegPlacement.FromString(placement);
            PegPlacements.Add(pegPlacement);
        }
        // Rebuild the board state from the initial board
        CurrentBoard = InitialBoard.Clone();
        foreach (var placement in PegPlacements)
        {
            CurrentBoard.PlacePeg(placement.Position, placement.State);
        }
        UpdateRating();
    }
}