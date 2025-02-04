using System.Text.Json;
using TBDel.Models;

namespace TBDel.Services
{
    public class DbService
    {
        private readonly string _dbPath;
        private List<FileEntry> _fileCollection;
        private List<FolderEntry> _folderCollection;

        public DbService()
        {
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TBDel_Db.json");
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_dbPath))
            {
                var json = File.ReadAllText(_dbPath);
                var dbContent = JsonSerializer.Deserialize(json, JsonContext.Default.DatabaseContent) ?? new DatabaseContent();
                _fileCollection = dbContent.FileEntries;
                _folderCollection = dbContent.FolderEntries;
            }
            else
            {
                _fileCollection = new List<FileEntry>();
                _folderCollection = new List<FolderEntry>();
            }
        }

        private void SaveData()
        {
            var dbContent = new DatabaseContent
            {
                FileEntries = _fileCollection,
                FolderEntries = _folderCollection
            };
            var json = JsonSerializer.Serialize(dbContent, JsonContext.Default.DatabaseContent);
            File.WriteAllText(_dbPath, json);
        }

        public async Task<bool> AddFileEntryAsync(FileEntry entry)
        {
            _fileCollection.Add(entry);
            SaveData();
            return await Task.FromResult(true);
        }

        public async Task<bool> AddFolderEntryAsync(FolderEntry entry)
        {
            _folderCollection.Add(entry);
            SaveData();
            return await Task.FromResult(true);
        }

        public async Task<List<FileEntry>> GetFileEntriesAsync()
        {
            return await Task.FromResult(_fileCollection.ToList());
        }

        public async Task<List<FolderEntry>> GetFolderEntriesAsync()
        {
            return await Task.FromResult(_folderCollection.ToList());
        }

        public async Task<bool> RemoveFileEntryAsync(string path)
        {
            var entryToRemove = _fileCollection.FirstOrDefault(e => e.Path == path);
            if (entryToRemove != null)
            {
                _fileCollection.Remove(entryToRemove);
                SaveData();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> RemoveFolderEntryAsync(string path)
        {
            var entryToRemove = _folderCollection.FirstOrDefault(e => e.Path == path);
            if (entryToRemove != null)
            {
                _folderCollection.Remove(entryToRemove);
                SaveData();
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }

    public class DatabaseContent
    {
        public DatabaseContent()
        {
            FileEntries = new List<FileEntry>();
            FolderEntries = new List<FolderEntry>();
        }

        public List<FileEntry> FileEntries { get; set; }
        public List<FolderEntry> FolderEntries { get; set; }
    }
}
