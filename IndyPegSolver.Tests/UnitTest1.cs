using Shouldly;
using Xunit;

namespace IndyPegSolver.Tests
{
    public class UnitTest1
    {
        private readonly Demo _demo;

        public UnitTest1()
        {
            _demo = new Demo();
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-1, -1, -2)]
        [InlineData(100, 200, 300)]
        public void Add_ShouldReturnCorrectSum(int a, int b, int expectedSum)
        {
            // Act
            var result = _demo.Add(a, b);

            // Assert
            result.ShouldBe(expectedSum);
        }

        [Fact]
        public void Add_ShouldThrowOverflowException_OnOverflow()
        {
            // Arrange
            int maxInt = int.MaxValue;

            // Act & Assert
            Should.Throw<OverflowException>(() => _demo.Add(maxInt, 1));
        }

        [Fact]
        public void Add_ShouldThrowOverflowException_OnUnderflow()
        {
            // Arrange
            int minInt = int.MinValue;

            // Act & Assert
            Should.Throw<OverflowException>(() => _demo.Add(minInt, -1));
        }
    }
}