using Xunit;
using System.Collections.Generic;

namespace IndyPegSolver.Tests
{
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
            Assert.NotNull(gameState.PegPlacements);
            Assert.Empty(gameState.PegPlacements);
            Assert.Equal(initialBoard, gameState.InitialBoard);
            Assert.Equal(initialBoard, gameState.CurrentBoard);
            Assert.Equal(new BoardRating(0, initialBoard.CountUnfilledHoles()), gameState.Rating);
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
            Assert.Contains(pegPlacement, gameState.PegPlacements);
            Assert.Equal(SlotState.Left, gameState.CurrentBoard.GetSlotState(pegPlacement.Position));
            Assert.Equal(new BoardRating(1, gameState.CurrentBoard.CountUnfilledHoles()), gameState.Rating);
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
            Assert.DoesNotContain(pegPlacement, gameState.PegPlacements);
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(pegPlacement.Position));
            Assert.Equal(new BoardRating(0, gameState.CurrentBoard.CountUnfilledHoles()), gameState.Rating);
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
            Assert.DoesNotContain(pegPlacement1, gameState.PegPlacements);
            Assert.Contains(pegPlacement2, gameState.PegPlacements);
            
            // removed items
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(pegPlacement1.Position));
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(new Point(3,2)));
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(new Point(4,0)));
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(new Point(4,1)));
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(new Point(4,2)));
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(new Point(4,3)));
            Assert.Equal(SlotState.Hole, gameState.CurrentBoard.GetSlotState(new Point(4,4)));

            // remaining items            
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(0, 0)));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(0, 1)));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(0, 2)));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(1, 0)));
            Assert.Equal(SlotState.Left, gameState.CurrentBoard.GetSlotState(pegPlacement2.Position));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(1, 2)));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(2, 0)));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(2, 1)));
            Assert.Equal(SlotState.Filled, gameState.CurrentBoard.GetSlotState(new Point(2, 2)));

            Assert.Equal(new BoardRating(1, gameState.CurrentBoard.CountUnfilledHoles()), gameState.Rating);
        }
    }
}
