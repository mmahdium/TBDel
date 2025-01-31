using Microsoft.Extensions.Logging;
using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands;

public class AddCommand
{

    public static async Task<Boolean> AddEntry(string[] args)
    {
        // TODO: Add unique Id support
        // TODO: Add duplicate path check
        // TODO: Add support for multiple paths

        if (args.Length > 1)
        {
            string workingDirectory = Directory.GetCurrentDirectory();
            string absolutePath = Path.Combine(workingDirectory, args[1]);
            if (File.Exists(absolutePath))
            {
                Console.WriteLine($"Adding: {absolutePath}");
                var entry = new FileEntry { Path = absolutePath, DateAdded = DateTime.Now };
                var dbService = new DbService();
                if (await dbService.AddFileEntryAsync(entry))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("File added successfully.");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to add file.");
                    Console.ResetColor();
                    return false;
                }
            }
            else if (Directory.Exists(absolutePath))
            {
                Console.WriteLine($"Adding: {absolutePath}");
                var entry = new FolderEntry() { Path = absolutePath, DateAdded = DateTime.Now };
                var dbService = new DbService();
                if (await dbService.AddFolderEntryAsync(entry))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Directory added successfully.");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to add directory.");
                    Console.ResetColor();
                    return false;
                }
            }
        }
        return false;
    }
}