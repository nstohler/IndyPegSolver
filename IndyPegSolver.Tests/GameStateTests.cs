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

        [Fact]
        public void UpdateRating_ShouldUpdateRatingCorrectly()
        {
            // Arrange
            char[,] board = {
                { 'O', 'O', 'O', 'O' },
                { 'O', 'O', '-', 'O' },
                { 'O', 'O', '-', 'O' },
                { '-', 'O', 'O', 'O' },
                { '-', '-', 'O', 'O' },
                { '-', 'O', 'O', '-' },
                { '-', '-', 'O', '-' },
                { '-', '-', 'O', '-' },
                { 'O', 'O', 'O', 'O' }
            };
            var initialBoard = new Board(board);
            var gameState = new GameState(initialBoard);
            
            // pegs that lead to a real solution
            var pegPlacement1 = new PegPlacement(new Point(1, 1), SlotState.Left);
            var pegPlacement2 = new PegPlacement(new Point(0, 3), SlotState.Right);
            var pegPlacement3 = new PegPlacement(new Point(4, 2), SlotState.Left);
            var pegPlacement4 = new PegPlacement(new Point(8, 2), SlotState.Right);

            // Act / Assert
            gameState.AddPegPlacement(pegPlacement1);
            gameState.Rating.PegCount.ShouldBe(1);
            gameState.Rating.UnfilledHolesCount.ShouldBe(16);
            gameState.Rating.ToString().ShouldBe("[1-16]");

            gameState.AddPegPlacement(pegPlacement2);
            gameState.Rating.PegCount.ShouldBe(2);
            gameState.Rating.UnfilledHolesCount.ShouldBe(11);
            gameState.Rating.ToString().ShouldBe("[2-11]");

            gameState.AddPegPlacement(pegPlacement3);
            gameState.Rating.PegCount.ShouldBe(3);
            gameState.Rating.UnfilledHolesCount.ShouldBe(6);
            gameState.Rating.ToString().ShouldBe("[3-6]");

            gameState.AddPegPlacement(pegPlacement4);            
            
            // Assert (final)
            gameState.Rating.PegCount.ShouldBe(4);
            gameState.Rating.UnfilledHolesCount.ShouldBe(0);
            gameState.Rating.ToString().ShouldBe("[4-0]");
        }
    }    
}
