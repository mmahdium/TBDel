using TBDel.Services;

namespace TBDel.Commands;

public class DeleteAllCommand
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
        Console.Write(
            $"Are you sure you want to permanently delete {allFiles.Count} file(s) and {allFolders.Count} folder(s)? (y/N) ");
        Console.ResetColor();
        var input = Console.ReadLine();
        if (input != "y")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Operation cancelled.");
            Console.ResetColor();
            return;
        }

        foreach (var file in allFiles)
        {
            if (!File.Exists(file.Path))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"File {file.Path} does not exist, removing from list.");
                Console.ResetColor();
                await dbService.RemoveFileEntryAsync(file.Id);
                continue;
            }

            Console.WriteLine($"Deleting file: {file.Path}");
            try
            {
                File.Delete(file.Path);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while deleting the file.");
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return;
            }

            await dbService.RemoveFileEntryAsync(file.Id);
        }

        foreach (var folder in allFolders)
        {
            if (!Directory.Exists(folder.Path))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Directory {folder.Path} does not exist, removing from list.");
                Console.ResetColor();
                await dbService.RemoveFolderEntryAsync(folder.Id);
                continue;
            }

            Console.WriteLine($"Deleting directory: {folder.Path}");
            try
            {
                Directory.Delete(folder.Path, true);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while deleting the directory.");
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return;
            }

            await dbService.RemoveFolderEntryAsync(folder.Id);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("All files and folders deleted successfully.");
        Console.ResetColor();
    }
}