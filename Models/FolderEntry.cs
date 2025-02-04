using System.Text.Json.Serialization;

namespace TBDel.Models;

// The same as FileEntry
public class FolderEntry : FileEntry
{
    public FolderEntry() : base()
    {
    }

    [JsonConstructor]
    public FolderEntry(uint id, string path, DateTime dateAdded) : base(id, path, dateAdded)
    {
    }
}