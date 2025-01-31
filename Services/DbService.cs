using JsonFlatFileDataStore;
using TBDel.Models;

namespace TBDel.Services;

public class DbService 
{
    private readonly DataStore _store;
    private readonly IDocumentCollection<FileEntry> _fileCollection;
    private readonly IDocumentCollection<FolderEntry> _folderCollection;

    public DbService() 
    {
        string dbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TBDel_Db.json");
        var _store = new DataStore(dbPath,minifyJson:true);
        _fileCollection = _store.GetCollection<FileEntry>();
        _folderCollection = _store.GetCollection<FolderEntry>();
    }


    public async Task<Boolean> AddFileEntryAsync(FileEntry entry)
    {
        return await _fileCollection.InsertOneAsync(entry);
    }

    public async Task<Boolean> AddFolderEntryAsync(FolderEntry entry)
    {
        return await _folderCollection.InsertOneAsync(entry);
        
    }

    public async Task<List<FileEntry>> GetFileEntriesAsync()
    {
        return _fileCollection.AsQueryable().ToList();
    }

    public async Task<List<FolderEntry>> GetFolderEntriesAsync()
    {
        return _folderCollection.AsQueryable().ToList();
    }


    public  async Task<Boolean> RemoveFileEntryAsync(string path)
    {
        var entryToRemove = _fileCollection.AsQueryable().FirstOrDefault(e => e.Path == path);
        if (entryToRemove != null)
        {
            return await _fileCollection.DeleteOneAsync(e => e.Path == path); // or entryToRemove.Path
        }

        return false;
    }

    public async Task<Boolean> RemoveFolderEntryAsync(string path)
    {
        var entryToRemove = _folderCollection.AsQueryable().FirstOrDefault(e => e.Path == path);
        if (entryToRemove != null)
        {
            return await _folderCollection.DeleteOneAsync(e => e.Path == path); // or entryToRemove.Path
        }
        return false;
    }
}