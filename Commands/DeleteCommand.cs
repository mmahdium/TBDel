using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands
{
    public class DeleteCommand
    {
        public static async Task DeleteEntry(string[] args)
        {
            var dbService = new DbService();
            if (args.Length > 1 && uint.TryParse(args[1], out uint id))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Are you sure you want to permanently delete entry with ID {id}? (y/N)");
                Console.ResetColor();
                var input = Console.ReadLine();
                if (input != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Operation cancelled.");
                    Console.ResetColor();
                    return;
                }

                if (File.Exists(args[1]))
                {
                    File.Delete(args[1]);
                    if (await dbService.RemoveFileEntryAsync(id))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("File deleted successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Something went wrong while deleting the file.");
                        Console.ResetColor();
                    }
                }
                else if (Directory.Exists(args[1]))
                {
                    Directory.Delete(args[1]);
                    if (await dbService.RemoveFolderEntryAsync(id))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Directory deleted successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Something went wrong while deleting the directory.");
                        Console.ResetColor();
                    }
                }


            }
        }
    }
}