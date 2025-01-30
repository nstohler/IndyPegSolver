using Xunit;
using Shouldly;

namespace IndyPegSolver.Tests
{
    public class BoardTests
    {
        [Fact]
        public void Constructor_ValidDimensions_InitializesBoard()
        {
            // Arrange
            int width = 5;
            int height = 5;

            // Act
            var board = new Board(width, height);

            // Assert
            board.Width.ShouldBe(width);
            board.Height.ShouldBe(height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    board.GetSlotState(new Point(i, j)).ShouldBe(SlotState.Hole);
                }
            }
        }

        [Fact]
        public void Constructor_DimensionsExceedMaxSize_ThrowsArgumentException()
        {
            // Arrange
            int width = 16;
            int height = 16;

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => new Board(width, height));
            exception.Message.ShouldBe("Board dimensions cannot exceed 15x15");
        }

        [Fact]
        public void Constructor_InitialState_InitializesBoard()
        {
            // Arrange
            char[,] initialState = new char[,]
            {
            { 'O', 'O', 'O' },
            { 'O', '-', 'O' },
            { 'O', 'O', 'O' }
            };

            // Act
            var board = new Board(initialState);

            // Assert
            board.Width.ShouldBe(3);
            board.Height.ShouldBe(3);
            board.GetSlotState(new Point(0, 0)).ShouldBe(SlotState.Hole);
            board.GetSlotState(new Point(1, 1)).ShouldBe(SlotState.Solid);
        }

        [Fact]
        public void Constructor_InitialStateExceedsMaxSize_ThrowsArgumentException()
        {
            // Arrange
            char[,] initialState = new char[16, 16];

            // Act & Assert
            var exception = Should.Throw<ArgumentException>(() => new Board(initialState));
            exception.Message.ShouldBe("Board dimensions cannot exceed 15x15");
        }

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
            board.GetSlotState(new Point(0, 0)).ShouldBe(SlotState.Hole);
            board.GetSlotState(new Point(2, 2)).ShouldBe(SlotState.Solid);
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
                    clonedBoard.GetSlotState(new Point(i, j)).ShouldBe(board.GetSlotState(new Point(i, j)));
                }
            }
        }

        [Fact]
        public void Clear_ShouldSetNonSolidSlotsToHole()
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
            board.PlacePeg(new Point(8, 2), SlotState.Right);
            board.PlacePeg(new Point(1, 0), SlotState.Left);            

            // Act
            board.Clear();

            // Assert
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (board.GetSlotState(new Point(i, j)) != SlotState.Solid)
                    {
                        board.GetSlotState(new Point(i, j)).ShouldBe(SlotState.Hole);
                    }
                }
            }
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
        public void Board_ShouldDetectUnsolvedState()
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
                { 'X', 'X', 'O', 'X' } // One slot is still a hole
            };
            Board board = new Board(initialState);

            // Act
            bool isSolved = board.IsSolved();

            // Assert
            isSolved.ShouldBeFalse();
        }

        [Fact]
        public void Board_ShouldCountUnfilledHolesCorrectly()
        {
            // Arrange
            // not a valid board state, but it's fine for testing
            char[,] initialState = {                
                { 'O', 'X', 'X', 'R' },
                { 'X', 'O', '-', 'X' },
                { 'X', 'X', '-', 'X' },
                { '-', 'R', 'X', 'O' },
                { '-', '-', 'L', 'X' },
                { '-', 'O', 'X', '-' },
                { '-', '-', 'X', '-' },
                { '-', '-', 'X', '-' },
                { 'X', 'X', 'O', 'X' } // One slot is still a hole
            };
            Board board = new Board(initialState);

            // Act
            int unfilledHoles = board.CountUnfilledHoles();

            // Assert
            unfilledHoles.ShouldBe(5);
        }

        [Fact]
        public void Board_ShouldCombineCorrectly()
        {
            // TODO: parameterized test here as well?

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
            Board board1 = new Board(initialState1);
            Board board2 = new Board(initialState2);

            // Act
            Board combinedBoard = board1.Combine(board2);

            // Assert
            for (int i = 0; i < combinedBoard.Width; i++)
            {
                for (int j = 0; j < combinedBoard.Height; j++)
                {
                    combinedBoard.GetSlotState(new Point(i, j)).ShouldBe(board2.GetSlotState(new Point(i, j)));
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
            board.PlacePeg(new Point(0, 0), SlotState.Left);

            // Assert
            board.GetSlotState(new Point(0, 0)).ShouldBe(SlotState.Left);
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

        [Fact]
        public void FillAffectedSlotsOnTurnLeft_ShouldUpdateBoardCorrectly()
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
            board.PlacePeg(new Point(1, 1), SlotState.Left); // Assuming this method calls FillAffectedSlotsOnTurnLeft internally

            // Assert
            // Add assertions to verify the board state after turning left
            // Example assertions (these should be replaced with actual expected results):
            board.GetSlotState(new Point(1, 1)).ShouldBe(SlotState.Left);
            board.GetSlotState(new Point(0, 0)).ShouldBe(SlotState.Filled); 
            board.GetSlotState(new Point(0, 1)).ShouldBe(SlotState.Filled); 
            board.GetSlotState(new Point(0, 2)).ShouldBe(SlotState.Filled); 
            board.GetSlotState(new Point(1, 0)).ShouldBe(SlotState.Filled);            
            board.GetSlotState(new Point(2, 0)).ShouldBe(SlotState.Filled);
            board.GetSlotState(new Point(2, 1)).ShouldBe(SlotState.Filled);
        }

        [Fact]
        public void FillAffectedSlotsOnTurnLeft_ShouldNotAffectSolidSlots()
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
            board.PlacePeg(new Point(1, 1), SlotState.Left); // Assuming this method calls FillAffectedSlotsOnTurnLeft internally

            // Assert
            // Add assertions to verify that solid slots are not affected
            board.GetSlotState(new Point(2, 2)).ShouldBe(SlotState.Solid);
            board.GetSlotState(new Point(3, 0)).ShouldBe(SlotState.Solid);
        }

        [Fact]
        public void FillAffectedSlotsOnTurnLeft_ShouldThrowExceptionForInvalidPosition()
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

            // Act & Assert
            Should.Throw<InvalidOperationException>(() => board.PlacePeg(new Point(2, 2), SlotState.Left)); // Assuming this method calls FillAffectedSlotsOnTurnLeft internally
        }

        [Theory]
        [MemberData(nameof(GetFillAffectedSlotsOnTurnLeftTestData))]
        public void FillAffectedSlotsOnTurnLeft_ShouldUpdateBoardCorrectlyExt(char[,] initialState, char[,] expectedState, int x, int y)
        {
            // Arrange
            Board board = new Board(initialState);

            // Act
            board.PlacePeg(new Point(x, y), SlotState.Left);

            // Assert
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    board.GetSlotState(new Point(i, j)).ShouldBe(CharToSlotState(expectedState[i, j]));
                }
            }
        }

        public static IEnumerable<object[]> GetFillAffectedSlotsOnTurnLeftTestData()
        {
            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'X', 'X', 'X' },
                    { 'X', 'L', 'X' },
                    { 'X', 'X', 'X' }
                },
                1, 1
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'L', 'X', 'O' },
                    { 'X', 'X', 'O' },
                    { 'O', 'O', 'O' }
                },
                0, 0
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'O', 'X', 'L' },
                    { 'O', 'X', 'X' },
                    { 'O', 'O', 'O' }
                },
                0, 2
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'X', 'X', 'O' },
                    { 'L', 'X', 'O' }
                },
                2, 0
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'X', 'X' },
                    { 'O', 'X', 'L' }
                },
                2, 2
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O', 'O' },
                    { 'O', 'O', '-', 'O' },
                    { 'O', 'O', '-', 'O' },
                    { '-', 'O', 'O', 'O' },
                    { '-', '-', 'O', 'O' },
                    { '-', 'O', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { 'O', 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'X', 'X', 'X', 'O' },
                    { 'X', 'L', '-', 'O' },
                    { 'X', 'X', '-', 'O' },
                    { '-', 'O', 'O', 'O' },
                    { '-', '-', 'O', 'O' },
                    { '-', 'O', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { 'O', 'O', 'O', 'O' }
                },
                1, 1
            };

            // Add more test cases as needed
        }

        [Theory]
        [MemberData(nameof(GetFillAffectedSlotsOnTurnRightTestData))]
        public void FillAffectedSlotsOnTurnRight_ShouldUpdateBoardCorrectly(char[,] initialState, char[,] expectedState, int x, int y)
        {
            // Arrange
            Board board = new Board(initialState);

            // Act
            board.PlacePeg(new Point(x, y), SlotState.Right);

            // Assert
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    board.GetSlotState(new Point(i, j)).ShouldBe(CharToSlotState(expectedState[i, j]));
                }
            }
        }

        public static IEnumerable<object[]> GetFillAffectedSlotsOnTurnRightTestData()
        {
            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O', 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'X', 'R', 'X', 'X', 'X', 'X' }
                },
                0, 1
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', '-', 'O', 'O', 'O', '-', 'O' }
                },
                new char[,]
                {
                    { 'O', '-', 'X', 'R', 'X', '-', 'O' }
                },
                0, 3
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O' },
                    { '-' },
                    { 'O' },
                    { 'O' },
                    { 'O' }
                },
                new char[,]
                {
                    { 'O' },
                    { '-' },
                    { 'X' },
                    { 'R' },
                    { 'X' }
                },
                3, 0
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O' },
                    { 'O' },
                    { 'O' },
                    { '-' },
                    { 'O' }
                },
                new char[,]
                {
                    { 'X' },
                    { 'R' },
                    { 'X' },
                    { '-' },
                    { 'O' }
                },
                1, 0
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' },
                    { 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'R', 'X', 'X' },
                    { 'X', 'O', 'O' },
                    { 'X', 'O', 'O' },
                },
                0, 0
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'X', 'L' },
                    { 'O', 'X', 'X' },
                    { '-', 'O', 'O' }
                },
                new char[,]
                {
                    { 'R', 'X', 'L' },
                    { 'X', 'X', 'X' },
                    { '-', 'O', 'O' },
                },
                0, 0
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O', 'O' },
                    { 'O', 'O', '-', 'O' },
                    { 'O', 'O', '-', 'O' },
                    { '-', 'O', 'O', 'O' },
                    { '-', '-', 'O', 'O' },
                    { '-', 'O', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { 'O', 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'O', 'X', 'O', 'O' },
                    { 'X', 'R', '-', 'O' },
                    { 'O', 'X', '-', 'O' },
                    { '-', 'X', 'O', 'O' },
                    { '-', '-', 'O', 'O' },
                    { '-', 'O', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { 'O', 'O', 'O', 'O' }
                },
                1, 1
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O', 'O' },
                    { 'O', 'O', '-', 'O' },
                    { 'O', 'O', 'O', 'O' },
                    { '-', 'O', 'O', 'O' },
                    { '-', '-', 'O', 'O' },
                    { '-', 'O', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { 'O', 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'O', 'O', 'O', 'O' },
                    { 'O', 'O', '-', 'O' },
                    { 'O', 'O', 'X', 'O' },
                    { '-', 'X', 'R', 'X' },
                    { '-', '-', 'X', 'O' },
                    { '-', 'O', 'X', '-' },
                    { '-', '-', 'X', '-' },
                    { '-', '-', 'X', '-' },
                    { 'O', 'O', 'X', 'O' }
                },
                3, 2
            };

            yield return new object[]
            {
                new char[,]
                {
                    { 'O', 'O', 'O', 'O' },
                    { 'O', 'X', 'X', 'X' },
                    { 'O', 'X', 'L', 'X' },
                    { '-', 'X', 'X', 'X' },
                    { '-', '-', 'O', 'O' },
                    { '-', 'O', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { '-', '-', 'O', '-' },
                    { 'O', 'O', 'O', 'O' }
                },
                new char[,]
                {
                    { 'O', 'O', 'X', 'O' },
                    { 'O', 'X', 'X', 'X' },
                    { 'O', 'X', 'L', 'X' },
                    { '-', 'X', 'X', 'X' },
                    { '-', '-', 'X', 'O' },
                    { '-', 'O', 'X', '-' },
                    { '-', '-', 'X', '-' },
                    { '-', '-', 'X', '-' },
                    { 'X', 'X', 'R', 'X' }
                },
                8, 2
            };

            // Add more test cases as needed
        }

        private SlotState CharToSlotState(char c)
        {
            return c switch
            {
                '-' => SlotState.Solid,
                'O' => SlotState.Hole,
                'L' => SlotState.Left,
                'R' => SlotState.Right,
                'X' => SlotState.Filled,
                _ => throw new ArgumentException("Invalid character for slot state")
            };
        }

        [Fact]
        public void Board_ShouldPrintBoardCorrectly()
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
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                board.PrintBoard();

                // Assert
                var expectedOutput = $"O O O O {Environment.NewLine}O O - O {Environment.NewLine}O O - O {Environment.NewLine}- O O O {Environment.NewLine}- - O O {Environment.NewLine}- O O - {Environment.NewLine}- - O - {Environment.NewLine}- - O - {Environment.NewLine}O O O O {Environment.NewLine}";
                sw.ToString().ShouldBe(expectedOutput);
            }
        }

        [Fact]
        public void Board_ShouldSaveBoardToFileCorrectly()
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
            string filePath = "test_board.txt";

            // Act
            board.SaveBoardToFile(filePath);

            // Assert
            var expectedOutput = $"O O O O {Environment.NewLine}O O - O {Environment.NewLine}O O - O {Environment.NewLine}- O O O {Environment.NewLine}- - O O {Environment.NewLine}- O O - {Environment.NewLine}- - O - {Environment.NewLine}- - O - {Environment.NewLine}O O O O {Environment.NewLine}";
            File.ReadAllText(filePath).ShouldBe(expectedOutput);

            // Clean up
            File.Delete(filePath);
        }

        [Fact]
        public void Board_Equals_ShouldReturnTrue_ForEqualBoards()
        {
            // Arrange
            var board1 = new Board(5, 5);
            var board2 = new Board(5, 5);

            // Act & Assert
            board1.ShouldBe(board2);
            board1.Equals(board2).ShouldBeTrue();
        }

        [Fact]
        public void Board_Equals_ShouldReturnFalse_ForDifferentBoards()
        {
            // Arrange
            var board1 = new Board(5, 5);
            var board2 = new Board(5, 5);
            board2.PlacePeg(new Point(2, 2), SlotState.Left);

            // Act & Assert
            board1.ShouldNotBe(board2);
            board1.Equals(board2).ShouldBeFalse();
        }

        [Fact]
        public void Board_GetHashCode_ShouldReturnSameHashCode_ForEqualBoards()
        {
            // Arrange
            var board1 = new Board(5, 5);
            var board2 = new Board(5, 5);

            // Act & Assert
            board1.GetHashCode().ShouldBe(board2.GetHashCode());
        }

        [Fact]
        public void Board_GetHashCode_ShouldReturnDifferentHashCode_ForDifferentBoards()
        {
            // Arrange
            var board1 = new Board(5, 5);
            var board2 = new Board(5, 5);
            board2.PlacePeg(new Point(2, 2), SlotState.Right);

            // Act & Assert
            board1.GetHashCode().ShouldNotBe(board2.GetHashCode());
        }
    }
}