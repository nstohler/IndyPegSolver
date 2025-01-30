using Shouldly;
using Xunit;

namespace IndyPegSolver.Tests
{
    public class BoardRatingTests
    {
        [Fact]
        public void BoardRating_ShouldInitializeCorrectly()
        {
            // Arrange
            int pegCount = 5;
            int unfilledHolesCount = 10;

            // Act
            BoardRating rating = new BoardRating(pegCount, unfilledHolesCount);

            // Assert
            rating.PegCount.ShouldBe(pegCount);
            rating.UnfilledHolesCount.ShouldBe(unfilledHolesCount);
        }

        [Fact]
        public void BoardRating_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            BoardRating rating = new BoardRating(5, 10);

            // Act
            string result = rating.ToString();

            // Assert
            result.ShouldBe("[5-10]");
        }

        [Fact]
        public void BoardRating_ShouldBeEqual_WhenPropertiesAreSame()
        {
            // Arrange
            BoardRating rating1 = new BoardRating(5, 10);
            BoardRating rating2 = new BoardRating(5, 10);

            // Act & Assert
            rating1.ShouldBe(rating2);
            rating1.Equals(rating2).ShouldBeTrue();
        }

        [Fact]
        public void BoardRating_ShouldNotBeEqual_WhenPropertiesAreDifferent()
        {
            // Arrange
            BoardRating rating1 = new BoardRating(5, 10);
            BoardRating rating2 = new BoardRating(10, 5);

            // Act & Assert
            rating1.ShouldNotBe(rating2);
            rating1.Equals(rating2).ShouldBeFalse();
        }

        [Fact]
        public void BoardRating_ShouldGenerateCorrectHashCode()
        {
            // Arrange
            BoardRating rating1 = new BoardRating(5, 10);
            BoardRating rating2 = new BoardRating(5, 10);

            // Act & Assert
            rating1.GetHashCode().ShouldBe(rating2.GetHashCode());
        }
    }
}
