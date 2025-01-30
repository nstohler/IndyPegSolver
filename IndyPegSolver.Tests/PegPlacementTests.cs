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

        [Theory]
        [InlineData("2-2-L", 2, 2, SlotState.Left)]
        [InlineData("3-4-R", 3, 4, SlotState.Right)]
        [InlineData("0-0-L", 0, 0, SlotState.Left)]
        [InlineData("5-5-R", 5, 5, SlotState.Right)]
        [InlineData("12-2-L", 12, 2, SlotState.Left)]
        [InlineData("3-14-R", 3, 14, SlotState.Right)]
        public void FromString_ShouldCreateCorrectPegPlacement(string input, int expectedX, int expectedY, SlotState expectedState)
        {
            // Act
            PegPlacement result = PegPlacement.FromString(input);

            // Assert
            result.Position.X.ShouldBe(expectedX);
            result.Position.Y.ShouldBe(expectedY);
            result.State.ShouldBe(expectedState);
        }

        [Theory]
        [InlineData("1-1-X")]
        [InlineData("1-2-Y")]
        [InlineData("3-3-Z")]
        [InlineData("x-1-L")]
        [InlineData("1-a-R")]
        [InlineData("3-,-L")]
        [InlineData("1-2-L-test")]
        [InlineData("1-2-LR")]
        public void FromString_ShouldThrowArgumentException_ForInvalidPegState(string input)
        {
            // Act & Assert
            Should.Throw<ArgumentException>(() => PegPlacement.FromString(input));
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