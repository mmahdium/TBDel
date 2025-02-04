using TBDel.Services;

namespace TBDel.Commands
{
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
            Console.Write($"Are you sure you want to permanently delete {allFiles.Count} file(s) and {allFolders.Count} folder(s)? (y/N) ");
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
                Console.WriteLine($"Deleting file: {file.Path}");
                File.Delete(file.Path);
                await dbService.RemoveFileEntryAsync(file.Id);
            }
            foreach (var folder in allFolders)
            {
                Console.WriteLine($"Deleting directory: {folder.Path}");
                Directory.Delete(folder.Path, true);
                await dbService.RemoveFolderEntryAsync(folder.Id);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("All files and folders deleted successfully.");
            Console.ResetColor();
        }
    }
}