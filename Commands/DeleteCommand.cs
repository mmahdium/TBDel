using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands;


public class DeleteCommand
{
    // Will be done by unique file Id
    public static async Task DeleteEntry(string[] args)
    {
        var dbService = new DbService();
        if (args.Length > 1)
        {
            if (File.Exists(args[1]))
            {
                Console.WriteLine($"Deleteing file: {args[1]}");
                if (await dbService.RemoveFileEntryAsync(args[1]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("File deleted successfully");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("something went wrong");
                    Console.ResetColor();
                }
            }
        }

        
    }
}