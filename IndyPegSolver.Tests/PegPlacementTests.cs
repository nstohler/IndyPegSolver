using Xunit;
using System.Collections.Generic;

namespace IndyPegSolver.Tests
{
    public class PegPlacementTests
    {
        [Fact]
        public void PegPlacement_ShouldInitializeCorrectly()
        {
            // Arrange
            Point position = new Point(5, 10);
            SlotState state = SlotState.Left;

            // Act
            PegPlacement pegPlacement = new PegPlacement(position, state);

            // Assert
            Assert.Equal(position, pegPlacement.Position);
            Assert.Equal(state, pegPlacement.State);
        }

        [Fact]
        public void PegPlacement_ShouldBeEqual_WhenPropertiesAreSame()
        {
            // Arrange
            PegPlacement pegPlacement1 = new PegPlacement(new Point(5, 10), SlotState.Left);
            PegPlacement pegPlacement2 = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act & Assert
            Assert.Equal(pegPlacement1, pegPlacement2);
            Assert.True(pegPlacement1.Equals(pegPlacement2));
        }

        [Fact]
        public void PegPlacement_ShouldNotBeEqual_WhenPropertiesAreDifferent()
        {
            // Arrange
            PegPlacement pegPlacement1 = new PegPlacement(new Point(5, 10), SlotState.Left);
            PegPlacement pegPlacement2 = new PegPlacement(new Point(10, 5), SlotState.Right);

            // Act & Assert
            Assert.NotEqual(pegPlacement1, pegPlacement2);
            Assert.False(pegPlacement1.Equals(pegPlacement2));
        }

        [Fact]
        public void PegPlacement_ShouldGenerateCorrectHashCode()
        {
            // Arrange
            PegPlacement pegPlacement1 = new PegPlacement(new Point(5, 10), SlotState.Left);
            PegPlacement pegPlacement2 = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act & Assert
            Assert.Equal(pegPlacement1.GetHashCode(), pegPlacement2.GetHashCode());
        }

        [Fact]
        public void PegPlacement_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            PegPlacement pegPlacement = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act
            string result = pegPlacement.ToString();

            // Assert
            Assert.Equal("PegPlacement(Position: (5, 10), State: Left)", result);
        }

        [Fact]
        public void PegPlacement_Copy_ShouldCreateCorrectCopy()
        {
            // Arrange
            PegPlacement original = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act
            PegPlacement copy = original.Copy();

            // Assert
            Assert.Equal(original, copy);
            Assert.False(ReferenceEquals(original, copy)); // Ensure they are not the same reference
        }

        [Fact]
        public void PegPlacement_Comparer_ShouldSortCorrectly()
        {
            // Arrange
            List<PegPlacement> pegPlacements = new List<PegPlacement>
        {
            new PegPlacement(new Point(5, 10), SlotState.Left),
            new PegPlacement(new Point(3, 4), SlotState.Right),
            new PegPlacement(new Point(5, 5), SlotState.Left),
            new PegPlacement(new Point(3, 2), SlotState.Right),
            new PegPlacement(new Point(1, 1), SlotState.Left)
        };

            List<PegPlacement> expectedOrder = new List<PegPlacement>
        {
            new PegPlacement(new Point(3, 2), SlotState.Right),
            new PegPlacement(new Point(3, 4), SlotState.Right),
            new PegPlacement(new Point(1, 1), SlotState.Left),
            new PegPlacement(new Point(5, 5), SlotState.Left),
            new PegPlacement(new Point(5, 10), SlotState.Left)
        };

            // Act
            pegPlacements.Sort();

            // Assert
            Assert.Equal(expectedOrder, pegPlacements);
        }
    }
}