using TBDel.Models;

namespace TBDel.Interface;

public interface IDbService
{
    Task<Boolean> AddFileEntryAsync(FileEntry entry);
    Task<Boolean> AddFolderEntryAsync(FolderEntry entry);
    Task<List<FileEntry>> GetFileEntriesAsync();
    Task<List<FolderEntry>> GetFolderEntriesAsync();
    Task<Boolean> RemoveFileEntryAsync(string path);
    Task<Boolean> RemoveFolderEntryAsync(string path);
}