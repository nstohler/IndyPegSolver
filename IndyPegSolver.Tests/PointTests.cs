using Xunit;
namespace IndyPegSolver.Tests
{
    public class PointTests
    {
        [Fact]
        public void Point_ShouldInitializeCorrectly()
        {
            // Arrange
            int x = 5;
            int y = 10;

            // Act
            Point point = new Point(x, y);

            // Assert
            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
        }

        [Fact]
        public void Point_Copy_ShouldCreateCorrectCopy()
        {
            // Arrange
            Point original = new Point(5, 10);

            // Act
            Point copy = original.Copy();

            // Assert
            Assert.Equal(original, copy);
            #pragma warning disable CA2013 
            Assert.False(ReferenceEquals(original, copy)); // Ensure they are not the same reference
            #pragma warning restore CA2013
        }

        [Fact]
        public void Point_ShouldBeEqual_WhenCoordinatesAreSame()
        {
            // Arrange
            Point point1 = new Point(5, 10);
            Point point2 = new Point(5, 10);

            // Act & Assert
            Assert.Equal(point1, point2);
            Assert.True(point1.Equals(point2));
        }

        [Fact]
        public void Point_ShouldNotBeEqual_WhenCoordinatesAreDifferent()
        {
            // Arrange
            Point point1 = new Point(5, 10);
            Point point2 = new Point(10, 5);

            // Act & Assert
            Assert.NotEqual(point1, point2);
            Assert.False(point1.Equals(point2));
        }

        [Fact]
        public void Point_ShouldGenerateCorrectHashCode()
        {
            // Arrange
            Point point1 = new Point(5, 10);
            Point point2 = new Point(5, 10);

            // Act & Assert
            Assert.Equal(point1.GetHashCode(), point2.GetHashCode());
        }

        [Fact]
        public void Point_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            Point point = new Point(5, 10);

            // Act
            string result = point.ToString();

            // Assert
            Assert.Equal("(5, 10)", result);
        }

        [Fact]
        public void Point_Comparer_ShouldSortCorrectly()
        {
            // Arrange
            List<Point> points = new List<Point>
            {
                new Point(5, 10),
                new Point(3, 4),
                new Point(5, 5),
                new Point(3, 2),
                new Point(1, 1)
            };

            List<Point> expectedOrder = new List<Point>
            {
                new Point(1, 1),
                new Point(3, 2),
                new Point(3, 4),
                new Point(5, 5),
                new Point(5, 10)
            };

            // Act
            points.Sort();

            // Assert
            Assert.Equal(expectedOrder, points);
        }
    }
}