using TBDel.Services;

namespace TBDel.Commands;

public class DeleteCommand
{
    public static async Task DeleteAll()
    {
        var dbService = new DbService();
        var allFiles = await dbService.GetFileEntriesAsync();
        var allFolders = await dbService.GetFolderEntriesAsync();
     
        if (allFiles.Count == 0 && allFolders.Count == 0)
        {
            Console.WriteLine("No files or folders found.");
            return;
        }
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Are you sure you want to permanently delete {allFiles.Count} file(s) and {allFolders.Count} folder(s)? (y/N)");
        Console.ResetColor();
        var input = Console.ReadLine();
        if (input != "y")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Operation cancelled.");
            Console.ResetColor();
            return;
        }
        
        var toBeDeletedFiles = new List<string>();
        var toBeDeletedFolders = new List<string>();
        foreach (var file in allFiles)
        {
            toBeDeletedFiles.Add(file.Path);
        }
        foreach (var folder in allFolders)
        {
            toBeDeletedFolders.Add(folder.Path);
        }
        
        foreach (var file in toBeDeletedFiles)
        {
            Console.WriteLine($"Deleting file: {file}");
            File.Delete(file);
            await dbService.RemoveFileEntryAsync(file);
        }
        foreach (var folder in toBeDeletedFolders)
        {
            Console.WriteLine($"Deleting directory: {folder}");
            Directory.Delete(folder, true);
            await dbService.RemoveFolderEntryAsync(folder);
        }
    }
}