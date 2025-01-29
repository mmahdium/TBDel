using JsonFlatFileDataStore;
using TBDel.Interface;
using TBDel.Models;

namespace TBDel.Services;

public class DbService : IDbService
{
    private readonly DataStore _store;
    /*private readonly IDataCollection<FileEntry> _fileCollection;
    private readonly IDataCollection<FolderEntry> _folderCollection;*/

    public DbService(string filePath) // Constructor takes the file path
    {
        /*_store = new DataStore(filePath);
        _fileCollection = _store.GetCollection<FileEntry>();
        _folderCollection = _store.GetCollection<FolderEntry>();*/
    }

    public async Task<Boolean> AddFileEntryAsync(FileEntry entry)
    {
        //await _fileCollection.InsertOneAsync(entry);
        return false; 
    }

    public async Task<Boolean> AddFolderEntryAsync(FolderEntry entry)
    {
        //await _folderCollection.InsertOneAsync(entry);
        return false;
    }

    public async Task<List<FileEntry>> GetFileEntriesAsync()
    {
        //return _fileCollection.AsQueryable().ToList();
        return new List<FileEntry>();
    }

    public async Task<List<FolderEntry>> GetFolderEntriesAsync()
    {
        //return _folderCollection.AsQueryable().ToList();
        return new List<FolderEntry>();
    }


    public async Task<Boolean> RemoveFileEntryAsync(string path)
    {
        //var entryToRemove = _fileCollection.AsQueryable().FirstOrDefault(e => e.Path == path);
        /*if (entryToRemove != null)
        {
            await _fileCollection.DeleteOneAsync(e => e.Path == path); // or entryToRemove.Path
        }*/
        return false;
    }

    public async Task<Boolean> RemoveFolderEntryAsync(string path)
    {
        //var entryToRemove = _folderCollection.AsQueryable().FirstOrDefault(e => e.Path == path);
        /*if (entryToRemove != null)
        {
            await _folderCollection.DeleteOneAsync(e => e.Path == path); // or entryToRemove.Path
        }*/
        return false;
    }
}