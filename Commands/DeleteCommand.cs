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
                var filePath = await dbService.GetEntryPath(id);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Are you sure you want to permanently delete entry with ID {id}? (y/N) ");
                Console.ResetColor();
                var input = Console.ReadLine();
                
                if (input != "y")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Operation cancelled.");
                    Console.ResetColor();
                    return;
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
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
                else if (Directory.Exists(filePath))
                {
                    try
                    {
                        Directory.Delete(filePath, true);
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (e.Message.Contains("Directory not empty"))
                        {
                            Console.WriteLine("Directory is not empty. It must be empty before it can be deleted or deleted manually.");
                            
                        }
                        
                        Console.WriteLine("Something went wrong while deleting the directory.");
                        Console.ResetColor();
                        Console.WriteLine(e.Message);
                        return;
                    }
                    
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