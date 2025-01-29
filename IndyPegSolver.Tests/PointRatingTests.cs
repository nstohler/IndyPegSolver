using Shouldly;

namespace IndyPegSolver.Tests
{
    public class PointRatingTests
    {
        [Fact]
        public void PointRating_ShouldInitializeCorrectly()
        {
            // Arrange
            var holePosition = new Point(2, 2);

            // Act
            var pointRating = new PointRating(holePosition);

            // Assert
            Assert.Equal(holePosition, pointRating.HolePosition);
            Assert.NotNull(pointRating.Fillers);
            Assert.Empty(pointRating.Fillers);
        }

        [Fact]
        public void PointRating_AddFiller_ShouldAddPegPlacementToFillers()
        {
            // Arrange
            var holePosition = new Point(2, 2);
            var pointRating = new PointRating(holePosition);
            var filler = new PegPlacement(new Point(1, 1), SlotState.Left);

            // Act
            pointRating.AddFiller(filler);

            // Assert
            Assert.Contains(filler, pointRating.Fillers);
        }

        [Fact]
        public void Board_GeneratePointRatings_ShouldGenerateCorrectRatings()
        {
            // Arrange
            var initialState = new char[,]
            {
            { '-', '-', '-', '-', '-' },
            { '-', 'O', '-', 'O', '-' },
            { '-', '-', 'O', '-', '-' },
            { '-', 'O', '-', 'O', '-' },
            { '-', '-', '-', '-', '-' }
            };
            var board = new Board(initialState);

            // Act
            var pointRatings = board.GeneratePointRatings();

            // Assert
            Assert.Equal(5, pointRatings.Count);
            foreach (var pointRating in pointRatings)
            {
                Assert.NotEmpty(pointRating.Fillers);
            }
        }

        [Fact]
        public void Board_GeneratePointRatings_ShouldGenerateCorrectRatingsExt()
        {
            // Arrange
            var initialState = new char[,]
            {
            { 'O', 'O', 'O', 'O', 'O' },
            { '-', 'O', '-', '-', 'O' },
            { '-', '-', '-', '-', 'O' },
            { '-', 'O', '-', '-', 'O' },
            { 'O', '-', '-', '-', 'O' }
            };
            var board = new Board(initialState);

            // Act
            var pointRatings = board.GeneratePointRatings();

            // Assert
            Assert.Equal(12, pointRatings.Count);
            foreach (var pointRating in pointRatings)
            {
                Assert.NotEmpty(pointRating.Fillers); // can be empty if not connected to the board (not present in valid game boards!)
            }

            var pointRating0_0 = pointRatings.Single(pr => pr.HolePosition.Equals(new Point(0, 0)));
            pointRating0_0.Fillers.Count().ShouldBe(6);

            var pointRating4_4 = pointRatings.Single(pr => pr.HolePosition.Equals(new Point(4, 4)));
            pointRating4_4.Fillers.Count().ShouldBe(5);

            var pointRating0_4 = pointRatings.Single(pr => pr.HolePosition.Equals(new Point(0, 4)));
            pointRating0_4.Fillers.Count().ShouldBe(10);

            var pointRating3_1 = pointRatings.Single(pr => pr.HolePosition.Equals(new Point(3, 1)));
            pointRating3_1.Fillers.Count().ShouldBe(1);

            var pointRating4_0 = pointRatings.Single(pr => pr.HolePosition.Equals(new Point(4, 0)));
            pointRating4_0.Fillers.Count().ShouldBe(1);
        }
    }
}
