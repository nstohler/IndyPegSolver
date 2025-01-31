using Shouldly;

namespace IndyPegSolver.Tests
{
    public class PegPlacementRatingTests
    {
        [Fact]
        public void PegPlacementRating_ShouldInitializeCorrectly()
        {
            // Arrange
            var pegPlacement = new PegPlacement(new Point(2, 2), SlotState.Left);

            // Act
            var pegPlacementRating = new PegPlacementRating(pegPlacement);

            // Assert
            pegPlacementRating.PegPlacement.ShouldBe(pegPlacement);
            pegPlacementRating.FilledPoints.ShouldNotBeNull();
            pegPlacementRating.FilledPoints.ShouldBeEmpty();
            pegPlacementRating.Rating.ShouldBe(0);
        }

        [Fact]
        public void PegPlacementRating_AddFilledPoint_ShouldAddPointToFilledPoints()
        {
            // Arrange
            var pegPlacement = new PegPlacement(new Point(2, 2), SlotState.Left);
            var pegPlacementRating = new PegPlacementRating(pegPlacement);
            var filledPoint = new Point(1, 1);

            // Act
            pegPlacementRating.AddFilledPoint(filledPoint);

            // Assert
            pegPlacementRating.FilledPoints.ShouldContain(filledPoint);
            pegPlacementRating.Rating.ShouldBe(1);
        }

        [Fact]
        public void PegPlacementRating_ToString_ShouldReturnCorrectFormat()
        {
            // Arrange
            var pegPlacement = new PegPlacement(new Point(2, 2), SlotState.Left);
            var pegPlacementRating = new PegPlacementRating(pegPlacement);
            pegPlacementRating.AddFilledPoint(new Point(1, 1));
            pegPlacementRating.AddFilledPoint(new Point(3, 3));

            // Act
            string result = pegPlacementRating.ToString();

            // Assert
            result.ShouldBe("Rating: 2, PegPlacement: 2-2-L, FilledPoints: [(1, 1), (3, 3)]");
        }

        [Fact]
        public void GeneratePegPlacementRatings_ShouldGenerateCorrectRatings()
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
            var pegPlacementRatings = board.GeneratePegPlacementRatings();

            // Assert
            pegPlacementRatings.Count.ShouldBe(10); // 5 holes, each with 2 peg placements (Left and Right)
            foreach (var rating in pegPlacementRatings)
            {
                if(rating.PegPlacement.State == SlotState.Left)
                {
                    rating.FilledPoints.ShouldNotBeEmpty(); // all left peg placements should have filled points
                }
                else
                {
                    rating.FilledPoints.ShouldBeEmpty(); // all right peg placements should have no filled points in this (special) board                    
                }                
            }
        }

        [Fact]
        public void GeneratePegPlacementRatings_ShouldGenerateCorrectRatingsExt()
        {
            // Arrange
            var initialState = new char[,]
            {
            { 'O', 'O', 'O', 'O', 'O' },
            { '-', 'O', '-', 'O', 'O' },
            { '-', '-', '-', '-', 'O' },
            { '-', 'O', '-', '-', 'O' },
            { 'O', '-', '-', '-', 'O' }
            };
            var board = new Board(initialState);

            // Act
            var pegPlacementRatings = board.GeneratePegPlacementRatings();

            // Assert
            var pegPlacementRating0_0_L = pegPlacementRatings.Single(pr => pr.PegPlacement.Equals(new PegPlacement(new Point(0, 0), SlotState.Left)));
            pegPlacementRating0_0_L.FilledPoints.Count().ShouldBe(2);

            var pegPlacementRating0_0_R = pegPlacementRatings.Single(pr => pr.PegPlacement.Equals(new PegPlacement(new Point(0, 0), SlotState.Right)));
            pegPlacementRating0_0_R.FilledPoints.Count().ShouldBe(4);

            var pegPlacementRating0_4_L = pegPlacementRatings.Single(pr => pr.PegPlacement.Equals(new PegPlacement(new Point(0, 4), SlotState.Left)));
            pegPlacementRating0_4_L.FilledPoints.Count().ShouldBe(3);

            var pegPlacementRating0_4_R = pegPlacementRatings.Single(pr => pr.PegPlacement.Equals(new PegPlacement(new Point(0, 4), SlotState.Right)));
            pegPlacementRating0_4_R.FilledPoints.Count().ShouldBe(8);

            var pegPlacementRating4_0_L = pegPlacementRatings.Single(pr => pr.PegPlacement.Equals(new PegPlacement(new Point(4, 0), SlotState.Left)));
            pegPlacementRating4_0_L.FilledPoints.Count().ShouldBe(1);

            var pegPlacementRating4_0_R = pegPlacementRatings.Single(pr => pr.PegPlacement.Equals(new PegPlacement(new Point(4, 0), SlotState.Right)));
            pegPlacementRating4_0_R.FilledPoints.Count().ShouldBe(0);
        }
    }
}
