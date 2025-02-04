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
                Console.WriteLine($"Deleting entry with ID: {id}");
                bool fileDeleted = await dbService.RemoveFileEntryAsync(id);
                bool folderDeleted = await dbService.RemoveFolderEntryAsync(id);

                if (fileDeleted || folderDeleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Entry deleted successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong. Entry not found.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid or missing ID argument.");
                Console.ResetColor();
            }
        }
    }
}