using TBDel.Services;

namespace TBDel.Commands;

public class RemoveFromListCommand
{
    public static async Task DeleteEntryFromList(String[] args)
    {
        var dbService = new DbService();
        if (args.Length > 1 && uint.TryParse(args[1], out uint id))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Are you sure you want to remove the entry with ID {id} ONLY from the list? (y/N) ");
            Console.ResetColor();
            var input = Console.ReadLine();
                            
            if (input != "y")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Operation cancelled.");
                Console.ResetColor();
                return;
            }

            if (await dbService.RemoveFileEntryAsync(id) || await dbService.RemoveFolderEntryAsync(id))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Entry removed from the list.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while removing the entry from list.");
                Console.ResetColor();
            }
        }

        
    }
}