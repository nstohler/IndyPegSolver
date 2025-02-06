using Shouldly;
using Xunit;

namespace IndyPegSolver.Tests
{
    public class PointRatingTests
    {
        [Fact]
        public void PointRating_ShouldInitializeCorrectly()
        {
            // Arrange
            var point = new Point(2, 3);

            // Act
            var pointRating = new PointRating(point);

            // Assert
            pointRating.HolePosition.ShouldBe(point);
            pointRating.Fillers.ShouldBeEmpty();
            pointRating.PegPlacementRatings.ShouldBeEmpty();
            pointRating.Rating.ShouldBe(0);
        }

        [Fact]
        public void PointRating_ShouldAddFillerCorrectly()
        {
            // Arrange
            var point = new Point(2, 3);
            var pointRating = new PointRating(point);
            var filler = new PegPlacement(new Point(1, 1), SlotState.Left);

            // Act
            pointRating.AddFiller(filler);

            // Assert
            pointRating.Fillers.ShouldContain(filler);
            pointRating.Rating.ShouldBe(1);
        }

        [Fact]
        public void PointRating_ShouldAddPegPlacementRatingsCorrectly()
        {
            // Arrange
            var point = new Point(2, 3);
            var pointRating = new PointRating(point);
            var filler = new PegPlacement(new Point(1, 1), SlotState.Left);
            pointRating.AddFiller(filler);

            var pegPlacementRatings = new List<PegPlacementRating>
            {
                new PegPlacementRating(filler),
                new PegPlacementRating(new PegPlacement(new Point(2, 3), SlotState.Right))
            };

            // Act
            pointRating.AddPegPlacementRatings(pegPlacementRatings);

            // Assert
            pointRating.PegPlacementRatings.Count.ShouldBe(2);
            pointRating.PegPlacementRatings.ShouldContain(r => r.PegPlacement.Equals(filler));
            pointRating.PegPlacementRatings.ShouldContain(r => r.PegPlacement.Point.Equals(point));
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
            pointRating.Fillers.ShouldContain(filler);
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
            pointRatings.Count.ShouldBe(5);
            foreach (var pointRating in pointRatings)
            {
                pointRating.Fillers.ShouldNotBeEmpty();
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
            pointRatings.Count.ShouldBe(12);
            foreach (var pointRating in pointRatings)
            {
                pointRating.Fillers.ShouldNotBeEmpty(); // can be empty if not connected to the board (not present in valid game boards!)
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

        [Fact]
        public void PointRating_ToString_ShouldReturnCorrectString()
        {
            // Arrange
            var point = new Point(2, 3);
            var pointRating = new PointRating(point);
            var filler = new PegPlacement(new Point(1, 1), SlotState.Left);
            pointRating.AddFiller(filler);

            // Act
            var result = pointRating.ToString();

            // Assert
            result.ShouldBe("Rating: 1, Hole: (2, 3), Fillers: [1-1-L]");
        }
    }
}
