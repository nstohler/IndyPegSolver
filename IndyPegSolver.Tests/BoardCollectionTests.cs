using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Xunit;
using Shouldly;

namespace IndyPegSolver.Tests
{
    public class BoardCollectionTests
    {
        [Fact]
        public void AddBoard_ShouldAddBoardToCollection()
        {
            // Arrange
            var boardCollection = new BoardCollection();
            var boardMetadata = new BoardMetadata
            {
                Name = "Test Board",
                Description = "A test board",
                ExamplePegPlacementSolutions = new List<string> { "0-0-L|1-1-R" },
                BestSolutionPegCount = 2,
                MinimumPegsToStart = 2,
                Board = new char[,]
                {
                { 'O', 'O' },
                { 'O', '-' }
                }
            };

            // Act
            boardCollection.AddBoard(boardMetadata);

            // Assert
            boardCollection.Boards.ShouldContain(boardMetadata);
        }

        [Fact]
        public void LoadFromFile_ShouldLoadBoardsFromFile()
        {
            // Arrange
            var filePath = "test_boards.json";
            var boardMetadata = new BoardMetadata
            {
                Name = "Test Board",
                Description = "A test board",
                ExamplePegPlacementSolutions = new List<string> { "0-0-L|1-1-R" },
                BestSolutionPegCount = 2,
                MinimumPegsToStart = 2,
                Board = new char[,]
                {
                { 'O', 'O' },
                { 'O', '-' }
                }
            };
            var boards = new List<BoardMetadata> { boardMetadata };
            var json = JsonConvert.SerializeObject(boards, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);

            var boardCollection = new BoardCollection();

            // Act
            boardCollection.LoadFromFile(filePath);

            // Assert
            boardCollection.Boards.ShouldContain(boardMetadata);

            // Clean up
            File.Delete(filePath);
        }

        [Fact]
        public void SaveToFile_ShouldSaveBoardsToFile()
        {
            // Arrange
            var filePath = "test_boards.json";
            var boardCollection = new BoardCollection();
            var boardMetadata = new BoardMetadata
            {
                Name = "Test Board",
                Description = "A test board",
                ExamplePegPlacementSolutions = new List<string> { "0-0-L|1-1-R" },
                BestSolutionPegCount = 2,
                MinimumPegsToStart = 2,
                Board = new char[,]
                {
                { 'O', 'O' },
                { 'O', '-' }
                }
            };
            boardCollection.AddBoard(boardMetadata);

            // Act
            boardCollection.SaveToFile(filePath);

            // Assert
            var json = File.ReadAllText(filePath);
            var loadedBoards = JsonConvert.DeserializeObject<List<BoardMetadata>>(json);
                        
            loadedBoards.ShouldContain(boardMetadata);
            loadedBoards.ShouldContain(boardMetadata);

            // Clean up
            File.Delete(filePath);
        }
    }
}
