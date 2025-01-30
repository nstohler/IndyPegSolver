using Xunit;
using Shouldly;

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
            pegPlacement.Position.ShouldBe(position);
            pegPlacement.State.ShouldBe(state);
        }

        [Fact]
        public void PegPlacement_ShouldBeEqual_WhenPropertiesAreSame()
        {
            // Arrange
            PegPlacement pegPlacement1 = new PegPlacement(new Point(5, 10), SlotState.Left);
            PegPlacement pegPlacement2 = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act & Assert
            pegPlacement1.ShouldBe(pegPlacement2);
            pegPlacement1.Equals(pegPlacement2).ShouldBeTrue();
        }

        [Fact]
        public void PegPlacement_ShouldNotBeEqual_WhenPropertiesAreDifferent()
        {
            // Arrange
            PegPlacement pegPlacement1 = new PegPlacement(new Point(5, 10), SlotState.Left);
            PegPlacement pegPlacement2 = new PegPlacement(new Point(10, 5), SlotState.Right);

            // Act & Assert
            pegPlacement1.ShouldNotBe(pegPlacement2);
            pegPlacement1.Equals(pegPlacement2).ShouldBeFalse();
        }

        [Fact]
        public void PegPlacement_ShouldGenerateCorrectHashCode()
        {
            // Arrange
            PegPlacement pegPlacement1 = new PegPlacement(new Point(5, 10), SlotState.Left);
            PegPlacement pegPlacement2 = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act & Assert
            pegPlacement1.GetHashCode().ShouldBe(pegPlacement2.GetHashCode());
        }

        [Fact]
        public void PegPlacement_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            PegPlacement pegPlacement = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act
            string result = pegPlacement.ToString();

            // Assert
            result.ShouldBe("5-10-L");
        }

        [Fact]
        public void PegPlacement_Copy_ShouldCreateCorrectCopy()
        {
            // Arrange
            PegPlacement original = new PegPlacement(new Point(5, 10), SlotState.Left);

            // Act
            PegPlacement copy = original.Copy();

            // Assert
            copy.ShouldBe(original);
            
            #pragma warning disable CA2013
            ReferenceEquals(original, copy).ShouldBeFalse(); // Ensure they are not the same reference
            #pragma warning restore CA2013
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
            pegPlacements.ShouldBe(expectedOrder);
        }
    }
}