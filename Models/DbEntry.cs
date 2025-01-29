namespace TBDel.Models;

// The DB entry - A list of files and folders
public class DbEntry
{
    public List<FileEntry> Files { get; set; } = new();
    public List<FolderEntry> Folders { get; set; } = new();
}