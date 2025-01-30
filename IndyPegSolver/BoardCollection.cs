using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

public class BoardCollection
{
    public List<BoardMetadata> Boards { get; set; } = new List<BoardMetadata>();

    public void AddBoard(BoardMetadata boardMetadata)
    {
        Boards.Add(boardMetadata);
    }

    public void LoadFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var boards = JsonConvert.DeserializeObject<List<BoardMetadata>>(json);
        if (boards != null)
        {
            Boards = boards;
        }
    }

    public void SaveToFile(string filePath)
    {
        var json = JsonConvert.SerializeObject(Boards, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}