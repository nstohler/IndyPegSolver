
// demo of how to use the BoardCollection class
public class BoardCollectionExampleUsage
{
    public static void Main(string[] args)
    {
        var boardCollection = new BoardCollection();

        // Add hardcoded boards
        boardCollection.AddBoard(HardcodedBoards.IndyGameBoard1_1);

        // Save to file
        boardCollection.SaveToFile("boards.json");

        // Load from file
        boardCollection.LoadFromFile("boards.json");

        // Print loaded boards
        foreach (var board in boardCollection.Boards)
        {
            Console.WriteLine($"Name: {board.Name}, Best Solution Peg Count: {board.BestSolutionPegCount}, Minimum Pegs To Start: {board.MinimumPegsToStart}");
            PrintBoard(board.Board);
        }
    }

    private static void PrintBoard(char[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
