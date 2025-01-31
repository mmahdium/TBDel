namespace TBDel.Models;

public class FileEntry
{
    // Unique 5 digit number for each entry
    uint Id { get; set; }
    // Absolute path
    public string Path { get; set; } = string.Empty;
    // Date added
    public DateTime DateAdded { get; set; }
}