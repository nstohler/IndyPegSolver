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

    public GameState Clone()
    {
        var clonedGameState = new GameState(InitialBoard.Clone());
        foreach (var pegPlacement in PegPlacements)
        {
            clonedGameState.AddPegPlacement(pegPlacement.Clone());
        }        
        return clonedGameState;
    }

    public void AddPegPlacement(PegPlacement pegPlacement)
    {
        PegPlacements.Add(pegPlacement);
        CurrentBoard.PlacePeg(pegPlacement.Point, pegPlacement.State);
        UpdateRating();
    }

    public void RemovePegPlacement(PegPlacement pegPlacementToRemove)
    {
        PegPlacements.Remove(pegPlacementToRemove);
        // Rebuild the board state from the initial board
        CurrentBoard = InitialBoard.Clone();
        foreach (var placement in PegPlacements)
        {
            CurrentBoard.PlacePeg(placement.Point, placement.State);
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

    public string GetSortedPegPlacementString()
    {        
        var sortedPlacements = PegPlacements.OrderBy(p => p, new PegPlacementPositionDirectionComparer()).ToList();
        return string.Join("|", sortedPlacements);
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
            CurrentBoard.PlacePeg(placement.Point, placement.State);
        }
        UpdateRating();
    }

    public string GetLeftRightPegCount()
    {
        int leftCount = PegPlacements.Count(p => p.State == SlotState.Left);
        int rightCount = PegPlacements.Count(p => p.State == SlotState.Right);
        return $"{leftCount}L/{rightCount}R";
    }
}