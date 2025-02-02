using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands;

public class ListCommand
{
    public static async Task List(string[] args)
    {
        if (args.Length > 2)
        {
            Console.WriteLine("Too many arguments.");
            return;
        }
        if (args.Length == 1)
        {
            Console.WriteLine("Listing both files and folders.");
            await ListFiles();
            for (int i = 0; i < 110; i++)
            {
                Console.Write("-");
            }
            Console.Write("\n");
            await ListFolders();
        }
        else if (args[1] == "files")
        {
            await ListFiles();
        }
        else if (args[1] == "folders")
        {
            await ListFolders();
        }
        
    }

    private static async Task ListFiles()
    {
        var dbService = new DbService();
        var filesList = await dbService.GetFileEntriesAsync();

        if (filesList.Count == 0)
        {
            Console.WriteLine("No files found.");
            return;
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Found {0} file(s):\n", filesList.Count);
        Console.WriteLine("{0,-100} {1,10}\n", "Path", "Date added");
        Console.ResetColor();
        foreach (var file in filesList)
        {
            Console.WriteLine("{0,-100} {1,10}", file.Path, file.DateAdded);
        }
    }
    private static async Task ListFolders()
    {
        var dbService = new DbService();
        var foldersList = await dbService.GetFolderEntriesAsync();

        if (foldersList.Count == 0)
        {
            Console.WriteLine("No folders found.");
            return;
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Found {0} folder(s):\n", foldersList.Count);
        Console.WriteLine("{0,-100} {1,10}\n", "Path", "Date added");
        Console.ResetColor();
        foreach (var folder in foldersList)
        {
            Console.WriteLine("{0,-100} {1,10}", folder.Path, folder.DateAdded);
        }
    }
}