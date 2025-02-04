using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands
{
    public class AddCommand
    {
        public static async Task AddEntry(string[] args)
        {
            // TODO: Add duplicate path check
            // TODO: Add support for multiple paths

            if (args.Length > 1)
            {
                string workingDirectory = Directory.GetCurrentDirectory();
                string absolutePath = Path.Combine(workingDirectory, args[1]);
                var dbService = new DbService();

                if (File.Exists(absolutePath))
                {
                    Console.WriteLine($"Adding: {absolutePath}");
                    var entry = new FileEntry { Id = GenerateUniqueId(dbService), Path = absolutePath, DateAdded = DateTime.Now };
                    if (await dbService.AddFileEntryAsync(entry))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("File added successfully.");
                        Console.ResetColor();
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to add file.");
                        Console.ResetColor();
                        return;
                    }
                }
                else if (Directory.Exists(absolutePath))
                {
                    Console.WriteLine($"Adding: {absolutePath}");
                    var entry = new FolderEntry() { Id = GenerateUniqueId(dbService), Path = absolutePath, DateAdded = DateTime.Now };
                    if (await dbService.AddFolderEntryAsync(entry))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Directory added successfully.");
                        Console.ResetColor();
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to add directory.");
                        Console.ResetColor();
                        return;
                    }
                }
            }
        }

        private static uint GenerateUniqueId(DbService dbService)
        {
            // Hopefully not running away from me one day
            Random random = new Random();
            uint newId;
            do
            {
                newId = (uint)random.Next(10000, 99999);
            } while (IdExists(newId, dbService));
            return newId;
        }

        private static bool IdExists(uint id, DbService dbService)
        {
            // Not async? Not a problem (yet(?))
            var fileEntries = dbService.GetFileEntriesAsync().Result;
            var folderEntries = dbService.GetFolderEntriesAsync().Result;

            return fileEntries.Any(e => e.Id == id) || folderEntries.Any(e => e.Id == id);
        }
    }
}
