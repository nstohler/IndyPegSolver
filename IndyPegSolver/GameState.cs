using System.Collections.Generic;

public class GameState
{
    public List<PegPlacement> PegPlacements { get; }
    public Board InitialBoard { get; }
    public Board CurrentBoard { get; private set; }
    public int PegCount { get; private set; }
    public int UnfilledHolesCount { get; private set; }

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
        //CurrentBoard.SetSlotState(pegPlacement.Position, pegPlacement.State);
        //if (pegPlacement.State == SlotState.Left)
        //{
        //    CurrentBoard.FillAffectedSlotsOnTurnLeft(pegPlacement.Position);
        //}
        //else if (pegPlacement.State == SlotState.Right)
        //{
        //    CurrentBoard.FillAffectedSlotsOnTurnRight(pegPlacement.Position);
        //}
        UpdateRating();
    }

    public void RemovePegPlacement(PegPlacement pegPlacement)
    {
        PegPlacements.Remove(pegPlacement);
        // Rebuild the board state from the initial board
        CurrentBoard = InitialBoard.Clone();
        foreach (var placement in PegPlacements)
        {
            CurrentBoard.PlacePeg(pegPlacement.Position, pegPlacement.State);
            //CurrentBoard.SetSlotState(placement.Position, placement.State);
            //if (placement.State == SlotState.Left)
            //{
            //    CurrentBoard.FillAffectedSlotsOnTurnLeft(placement.Position);
            //}
            //else if (placement.State == SlotState.Right)
            //{
            //    CurrentBoard.FillAffectedSlotsOnTurnRight(placement.Position);
            //}
        }
        UpdateRating();
    }

    private void UpdateRating()
    {
        PegCount = PegPlacements.Count;
        UnfilledHolesCount = CurrentBoard.CountUnfilledHoles();
    }

    public override string ToString()
    {
        return $"GameState(PegCount: {PegCount}, UnfilledHolesCount: {UnfilledHolesCount})";
    }
}