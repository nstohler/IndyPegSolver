using Xunit;
using Shouldly;

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
            point.X.ShouldBe(x);
            point.Y.ShouldBe(y);
        }

        [Fact]
        public void Point_Clone_ShouldCreateCorrectClone()
        {
            // Arrange
            Point original = new Point(5, 10);

            // Act
            Point cloned = original.Clone();

            // Assert
            cloned.ShouldBe(original);
            #pragma warning disable CA2013 
            ReferenceEquals(original, cloned).ShouldBeFalse(); // Ensure they are not the same reference
            #pragma warning restore CA2013
        }

        [Fact]
        public void Point_ShouldBeEqual_WhenCoordinatesAreSame()
        {
            // Arrange
            Point point1 = new Point(5, 10);
            Point point2 = new Point(5, 10);

            // Act & Assert
            point1.ShouldBe(point2);
            point1.Equals(point2).ShouldBeTrue();
        }

        [Fact]
        public void Point_ShouldNotBeEqual_WhenCoordinatesAreDifferent()
        {
            // Arrange
            Point point1 = new Point(5, 10);
            Point point2 = new Point(10, 5);

            // Act & Assert
            point1.ShouldNotBe(point2);
            point1.Equals(point2).ShouldBeFalse();
        }

        [Fact]
        public void Point_ShouldGenerateCorrectHashCode()
        {
            // Arrange
            Point point1 = new Point(5, 10);
            Point point2 = new Point(5, 10);

            // Act & Assert
            point1.GetHashCode().ShouldBe(point2.GetHashCode());
        }

        [Fact]
        public void Point_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            Point point = new Point(5, 10);

            // Act
            string result = point.ToString();

            // Assert
            result.ShouldBe("(5, 10)");
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
            points.ShouldBe(expectedOrder);
        }
    }
}