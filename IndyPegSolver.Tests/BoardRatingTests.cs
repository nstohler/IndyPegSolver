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
            Assert.Equal(pegCount, rating.PegCount);
            Assert.Equal(unfilledHolesCount, rating.UnfilledHolesCount);
        }

        [Fact]
        public void BoardRating_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            BoardRating rating = new BoardRating(5, 10);

            // Act
            string result = rating.ToString();

            // Assert
            Assert.Equal("[5-10]", result);
        }

        [Fact]
        public void BoardRating_ShouldBeEqual_WhenPropertiesAreSame()
        {
            // Arrange
            BoardRating rating1 = new BoardRating(5, 10);
            BoardRating rating2 = new BoardRating(5, 10);

            // Act & Assert
            Assert.Equal(rating1, rating2);
            Assert.True(rating1.Equals(rating2));
        }

        [Fact]
        public void BoardRating_ShouldNotBeEqual_WhenPropertiesAreDifferent()
        {
            // Arrange
            BoardRating rating1 = new BoardRating(5, 10);
            BoardRating rating2 = new BoardRating(10, 5);

            // Act & Assert
            Assert.NotEqual(rating1, rating2);
            Assert.False(rating1.Equals(rating2));
        }

        [Fact]
        public void BoardRating_ShouldGenerateCorrectHashCode()
        {
            // Arrange
            BoardRating rating1 = new BoardRating(5, 10);
            BoardRating rating2 = new BoardRating(5, 10);

            // Act & Assert
            Assert.Equal(rating1.GetHashCode(), rating2.GetHashCode());
        }
    }
}
