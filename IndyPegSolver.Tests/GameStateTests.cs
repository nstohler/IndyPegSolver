using Xunit;
using System.Collections.Generic;

namespace IndyPegSolver.Tests
{
    using Shouldly;

    public class GameStateTests
    {
        [Fact]
        public void GameState_ShouldInitializeCorrectly()
        {
            // Arrange
            var initialBoard = new Board(5, 5);

            // Act
            var gameState = new GameState(initialBoard);

            // Assert
            gameState.PegPlacements.ShouldNotBeNull();
            gameState.PegPlacements.ShouldBeEmpty();
            gameState.InitialBoard.ShouldBe(initialBoard);
            gameState.CurrentBoard.ShouldBe(initialBoard);
            gameState.Rating.ShouldBe(new BoardRating(0, initialBoard.CountUnfilledHoles()));
        }

        [Fact]
        public void AddPegPlacement_ShouldUpdateBoardAndRating()
        {
            // Arrange
            var initialBoard = new Board(5, 5);
            var gameState = new GameState(initialBoard);
            var pegPlacement = new PegPlacement(new Point(2, 2), SlotState.Left);

            // Act
            gameState.AddPegPlacement(pegPlacement);

            // Assert
            gameState.PegPlacements.ShouldContain(pegPlacement);
            gameState.CurrentBoard.GetSlotState(pegPlacement.Position).ShouldBe(SlotState.Left);
            gameState.Rating.ShouldBe(new BoardRating(1, gameState.CurrentBoard.CountUnfilledHoles()));
        }

        [Fact]
        public void RemovePegPlacement_ShouldUpdateBoardAndRating()
        {
            // Arrange
            var initialBoard = new Board(5, 5);
            var gameState = new GameState(initialBoard);
            var pegPlacement = new PegPlacement(new Point(2, 2), SlotState.Left);
            gameState.AddPegPlacement(pegPlacement);

            // Act
            gameState.RemovePegPlacement(pegPlacement);

            // Assert
            gameState.PegPlacements.ShouldNotContain(pegPlacement);
            gameState.CurrentBoard.GetSlotState(pegPlacement.Position).ShouldBe(SlotState.Hole);
            gameState.Rating.ShouldBe(new BoardRating(0, gameState.CurrentBoard.CountUnfilledHoles()));
        }

        [Fact]
        public void AddAndRemoveMultiplePegPlacements_ShouldUpdateBoardAndRating()
        {
            // Arrange
            var initialBoard = new Board(5, 5);
            var gameState = new GameState(initialBoard);
            var pegPlacement1 = new PegPlacement(new Point(4, 2), SlotState.Right);
            var pegPlacement2 = new PegPlacement(new Point(1, 1), SlotState.Left);

            // Act
            gameState.AddPegPlacement(pegPlacement1);
            gameState.AddPegPlacement(pegPlacement2);
            gameState.RemovePegPlacement(pegPlacement1);

            // Assert
            gameState.PegPlacements.ShouldNotContain(pegPlacement1);
            gameState.PegPlacements.ShouldContain(pegPlacement2);
            
            // removed items
            gameState.CurrentBoard.GetSlotState(pegPlacement1.Position).ShouldBe(SlotState.Hole);
            gameState.CurrentBoard.GetSlotState(new Point(3,2)).ShouldBe(SlotState.Hole);
            gameState.CurrentBoard.GetSlotState(new Point(4,0)).ShouldBe(SlotState.Hole);
            gameState.CurrentBoard.GetSlotState(new Point(4,1)).ShouldBe(SlotState.Hole);
            gameState.CurrentBoard.GetSlotState(new Point(4,2)).ShouldBe(SlotState.Hole);
            gameState.CurrentBoard.GetSlotState(new Point(4,3)).ShouldBe(SlotState.Hole);
            gameState.CurrentBoard.GetSlotState(new Point(4,4)).ShouldBe(SlotState.Hole);

            // remaining items            
            gameState.CurrentBoard.GetSlotState(new Point(0, 0)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(new Point(0, 1)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(new Point(0, 2)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(new Point(1, 0)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(pegPlacement2.Position).ShouldBe(SlotState.Left);
            gameState.CurrentBoard.GetSlotState(new Point(1, 2)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(new Point(2, 0)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(new Point(2, 1)).ShouldBe(SlotState.Filled);
            gameState.CurrentBoard.GetSlotState(new Point(2, 2)).ShouldBe(SlotState.Filled);

            gameState.Rating.ShouldBe(new BoardRating(1, gameState.CurrentBoard.CountUnfilledHoles()));
        }
    }
}
