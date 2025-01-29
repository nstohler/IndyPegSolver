using Xunit;
using Shouldly;

namespace IndyPegSolver.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Board_ShouldInitializeCorrectly()
        {
            // Arrange
            char[,] initialState = {
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

            // Act
            Board board = new Board(initialState);

            // Assert
            board.Width.ShouldBe(9);
            board.Height.ShouldBe(4);
            board.GetSlotState(0, 0).ShouldBe(SlotState.Hole);
            board.GetSlotState(2, 2).ShouldBe(SlotState.Solid);
        }

        [Fact]
        public void Board_ShouldCloneCorrectly()
        {
            // Arrange
            char[,] initialState = {
                { 'O', 'O', 'O', 'O' },
                { 'O', 'O', '-', 'O' },
                { 'O', 'O', '-', 'O' },
                { '-', 'X', 'X', 'X' },
                { '-', '-', 'L', 'X' },
                { '-', 'X', 'X', '-' },
                { '-', '-', 'X', '-' },
                { '-', '-', 'X', '-' },
                { 'X', 'X', 'R', 'X' }
            };
            Board board = new Board(initialState);

            // Act
            Board clonedBoard = board.Clone();

            // Assert
            clonedBoard.Width.ShouldBe(board.Width);
            clonedBoard.Height.ShouldBe(board.Height);
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    clonedBoard.GetSlotState(i, j).ShouldBe(board.GetSlotState(i, j));
                }
            }
        }

        [Fact]
        public void Board_ShouldSetSlotStateCorrectly()
        {
            // Arrange
            Board board = new Board(5, 5);

            // Act
            board.SetSlotState(2, 2, SlotState.Hole);

            // Assert
            board.GetSlotState(2, 2).ShouldBe(SlotState.Hole);
        }

        [Fact]
        public void Board_ShouldDetectSolvedState()
        {
            // Arrange
            char[,] initialState = {
                { 'X', 'X', 'X', 'R' },
                { 'X', 'X', '-', 'X' },
                { 'X', 'X', '-', 'X' },
                { '-', 'R', 'X', 'X' },
                { '-', '-', 'L', 'X' },
                { '-', 'X', 'X', '-' },
                { '-', '-', 'X', '-' },
                { '-', '-', 'X', '-' },
                { 'X', 'X', 'R', 'X' }
            };
            Board board = new Board(initialState);

            // Act
            bool isSolved = board.IsSolved();

            // Assert
            isSolved.ShouldBeTrue();
        }

        [Fact]
        public void Board_ShouldCombineCorrectly()
        {
            // Arrange
            char[,] initialState1 = {
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
            char[,] initialState2 = {
                { 'X', 'X', 'X', 'X' },
                { 'X', 'X', '-', 'X' },
                { 'X', 'X', '-', 'X' },
                { '-', 'X', 'X', 'X' },
                { '-', '-', 'X', 'X' },
                { '-', 'X', 'X', '-' },
                { '-', '-', 'X', '-' },
                { '-', '-', 'X', '-' },
                { 'X', 'X', 'X', 'X' }
            };
            Board board1 = new Board(initialState1);
            Board board2 = new Board(initialState2);

            // Act
            Board combinedBoard = board1.Combine(board2);

            // Assert
            for (int i = 0; i < combinedBoard.Width; i++)
            {
                for (int j = 0; j < combinedBoard.Height; j++)
                {
                    combinedBoard.GetSlotState(i, j).ShouldBe(board2.GetSlotState(i, j));
                }
            }
        }

        [Fact]
        public void Board_ShouldPlacePegCorrectly()
        {
            // Arrange
            char[,] initialState = {
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
            Board board = new Board(initialState);

            // Act
            board.PlacePeg(0, 0, SlotState.Left);

            // Assert
            board.GetSlotState(0, 0).ShouldBe(SlotState.Left);
            // Additional assertions can be added here to verify the effect of placing the peg
        }

        [Theory]
        [InlineData(SlotState.Solid, SlotState.Solid, SlotState.Solid)]
        [InlineData(SlotState.Hole, SlotState.Hole, SlotState.Hole)]
        [InlineData(SlotState.Hole, SlotState.Filled, SlotState.Filled)]
        [InlineData(SlotState.Hole, SlotState.Left, SlotState.Left)]
        [InlineData(SlotState.Hole, SlotState.Right, SlotState.Right)]
        [InlineData(SlotState.Filled, SlotState.Filled, SlotState.Filled)]
        [InlineData(SlotState.Filled, SlotState.Left, SlotState.Left)]
        [InlineData(SlotState.Filled, SlotState.Right, SlotState.Right)]
        [InlineData(SlotState.Left, SlotState.Hole, SlotState.Left)]
        [InlineData(SlotState.Left, SlotState.Filled, SlotState.Left)]        
        [InlineData(SlotState.Right, SlotState.Hole, SlotState.Right)]
        [InlineData(SlotState.Right, SlotState.Filled, SlotState.Right)]       
        public void Board_CombineSlotStates_ShouldReturnExpectedResult(SlotState state1, SlotState state2, SlotState expected)
        {
            // Arrange
            Board board = new Board(1, 1);

            // Act
            SlotState result = board.CombineSlotStates(state1, state2);

            // Assert
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(SlotState.Solid, SlotState.Hole)]
        [InlineData(SlotState.Solid, SlotState.Filled)]
        [InlineData(SlotState.Solid, SlotState.Left)]
        [InlineData(SlotState.Solid, SlotState.Right)]
        [InlineData(SlotState.Hole, SlotState.Solid)]
        [InlineData(SlotState.Filled, SlotState.Solid)]
        [InlineData(SlotState.Left, SlotState.Solid)]
        [InlineData(SlotState.Right, SlotState.Solid)]
        [InlineData(SlotState.Left, SlotState.Right)]
        [InlineData(SlotState.Right, SlotState.Left)]
        [InlineData(SlotState.Left, SlotState.Left)]
        [InlineData(SlotState.Right, SlotState.Right)]
        public void Board_CombineSlotStates_ShouldThrowInvalidOperationException(SlotState state1, SlotState state2)
        {
            // Arrange
            Board board = new Board(1, 1);

            // Act & Assert
            Should.Throw<InvalidOperationException>(() => board.CombineSlotStates(state1, state2));
        }
    }
}