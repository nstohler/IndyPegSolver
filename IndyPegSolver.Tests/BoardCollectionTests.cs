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

            // Clean up
            File.Delete(filePath);
        }
    
        [Fact]
        public void SaveAndLoadMultipleBoards_ShouldPersistAndRetrieveAllBoards()
        {
            // Arrange
            var filePath = "test_multiple_boards.json";
            var boardCollection = new BoardCollection();
            var boardMetadata1 = new BoardMetadata
            {
                Name = "Test Board 1",
                Description = "First test board",
                ExamplePegPlacementSolutions = new List<string> { "0-0-L|1-1-R" },
                BestSolutionPegCount = 2,
                MinimumPegsToStart = 2,
                Board = new char[,]
                {
                { 'O', 'O' },
                { 'O', '-' }
                }
            };
            var boardMetadata2 = new BoardMetadata
            {
                Name = "Test Board 2",
                Description = "Second test board",
                ExamplePegPlacementSolutions = new List<string> { "1-0-L|0-1-R" },
                BestSolutionPegCount = 3,
                MinimumPegsToStart = 3,
                Board = new char[,]
                {
                { 'O', 'O', 'O' },
                { 'O', '-', 'O' }
                }
            };
            var boardMetadata3 = new BoardMetadata
            {
                Name = "Test Board 3",
                Description = "Third test board",
                ExamplePegPlacementSolutions = new List<string> { "2-0-L|2-1-R" },
                BestSolutionPegCount = 4,
                MinimumPegsToStart = 4,
                Board = new char[,]
                {
                { 'O', 'O', 'O', 'O' },
                { 'O', '-', 'O', 'O' }
                }
            };

            var hardCodedBoards = new List<BoardMetadata> 
            { 
                HardcodedBoards.IndyGameBoard1_1,
                HardcodedBoards.IndyGameBoard1_2,
                HardcodedBoards.IndyGameBoard2,
                HardcodedBoards.IndyGameBoard3,
                HardcodedBoards.IndyGameBoard4,
            };

            boardCollection.AddBoard(boardMetadata1);
            boardCollection.AddBoard(boardMetadata2);
            boardCollection.AddBoard(boardMetadata3);
            hardCodedBoards.ForEach(boardCollection.AddBoard);

            // Act
            boardCollection.SaveToFile(filePath);
            var loadedBoardCollection = new BoardCollection();
            loadedBoardCollection.LoadFromFile(filePath);

            // Assert
            loadedBoardCollection.Boards.Count.ShouldBe(boardCollection.Boards.Count);
            loadedBoardCollection.Boards.ShouldContain(boardMetadata1);
            loadedBoardCollection.Boards.ShouldContain(boardMetadata2);
            loadedBoardCollection.Boards.ShouldContain(boardMetadata3);
            hardCodedBoards.ForEach(board => loadedBoardCollection.Boards.ShouldContain(board));

            // Clean up
            File.Delete(filePath);
        }
    }
}
