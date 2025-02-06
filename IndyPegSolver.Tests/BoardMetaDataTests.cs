using System;
using System.Collections.Generic;
using Xunit;

namespace IndyPegSolver.Tests
{
    public class BoardMetadataTest
    {
        [Fact]
        public void Equals_WithSameProperties_ReturnsTrue()
        {
            var board1 = new BoardMetadata
            {
                Name = "Test Board",
                Description = "Test Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution1", "Solution2" },
                BestSolutionPegCount = 5,
                MinimumPegsToStart = 3,
                Board = new char[,] { { 'X', 'O' }, { 'O', 'X' } }
            };

            var board2 = new BoardMetadata
            {
                Name = "Test Board",
                Description = "Test Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution1", "Solution2" },
                BestSolutionPegCount = 5,
                MinimumPegsToStart = 3,
                Board = new char[,] { { 'X', 'O' }, { 'O', 'X' } }
            };

            Assert.True(board1.Equals(board2));
        }

        [Fact]
        public void Equals_WithDifferentProperties_ReturnsFalse()
        {
            var board1 = new BoardMetadata
            {
                Name = "Test Board",
                Description = "Test Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution1", "Solution2" },
                BestSolutionPegCount = 5,
                MinimumPegsToStart = 3,
                Board = new char[,] { { 'X', 'O' }, { 'O', 'X' } }
            };

            var board2 = new BoardMetadata
            {
                Name = "Different Board",
                Description = "Different Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution3", "Solution4" },
                BestSolutionPegCount = 6,
                MinimumPegsToStart = 4,
                Board = new char[,] { { 'O', 'X' }, { 'X', 'O' } }
            };

            Assert.False(board1.Equals(board2));
        }

        [Fact]
        public void GetHashCode_WithSameProperties_ReturnsSameHashCode()
        {
            var board1 = new BoardMetadata
            {
                Name = "Test Board",
                Description = "Test Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution1", "Solution2" },
                BestSolutionPegCount = 5,
                MinimumPegsToStart = 3,
                Board = new char[,] { { 'X', 'O' }, { 'O', 'X' } }
            };

            var board2 = new BoardMetadata
            {
                Name = "Test Board",
                Description = "Test Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution1", "Solution2" },
                BestSolutionPegCount = 5,
                MinimumPegsToStart = 3,
                Board = new char[,] { { 'X', 'O' }, { 'O', 'X' } }
            };

            Assert.Equal(board1.GetHashCode(), board2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentProperties_ReturnsDifferentHashCode()
        {
            var board1 = new BoardMetadata
            {
                Name = "Test Board",
                Description = "Test Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution1", "Solution2" },
                BestSolutionPegCount = 5,
                MinimumPegsToStart = 3,
                Board = new char[,] { { 'X', 'O' }, { 'O', 'X' } }
            };

            var board2 = new BoardMetadata
            {
                Name = "Different Board",
                Description = "Different Description",
                ExamplePegPlacementSolutions = new List<string> { "Solution3", "Solution4" },
                BestSolutionPegCount = 6,
                MinimumPegsToStart = 4,
                Board = new char[,] { { 'O', 'X' }, { 'X', 'O' } }
            };

            Assert.NotEqual(board1.GetHashCode(), board2.GetHashCode());
        }
    }
}