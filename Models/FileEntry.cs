using System.Text.Json.Serialization;

namespace TBDel.Models;

public class FileEntry
{
    // Unique 5 digit number for each entry
    public uint Id { get; set; }
    // Absolute path
    public string Path { get; set; } = string.Empty;
    // Date added
    public DateTime DateAdded { get; set; }

    public FileEntry()
    {
    }

    // To support trimmed binary
    [JsonConstructor]
    public FileEntry(uint id, string path, DateTime dateAdded)
    {
        Id = id;
        Path = path;
        DateAdded = dateAdded;
    }
}