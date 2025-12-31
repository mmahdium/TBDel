using System.Diagnostics.Contracts;
using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands
{
    public class AddCommand
    {
        public static async Task AddEntry(string[] args)
        {
            if (args.Length > 1)
            {
                string workingDirectory = Directory.GetCurrentDirectory();
                string absolutePath = Path.Combine(workingDirectory, args[1]);
                var dbService = new DbService();

                if (File.Exists(absolutePath))
                {
                    TuiHelper.DisplayInfo($"Adding file: {absolutePath}");
                    if (await dbService.EtryExists(absolutePath))
                    {
                        TuiHelper.DisplayWarning("File already exists in the list - No actions performed.");
                        return;
                    }
                    var entry = new FileEntry { Id = GenerateUniqueId(dbService), Path = absolutePath, DateAdded = DateTime.Now };
                    if (await dbService.AddFileEntryAsync(entry))
                    {
                        TuiHelper.DisplaySuccess("File added successfully.");
                        return;
                    }
                    else
                    {
                        TuiHelper.DisplayError("Failed to add file.");
                        return;
                    }
                }
                else if (Directory.Exists(absolutePath))
                {
                    TuiHelper.DisplayInfo($"Adding directory: {absolutePath}");
                    if (await dbService.EtryExists(absolutePath))
                    {
                        TuiHelper.DisplayWarning("Folder already exists in the list - No actions performed.");
                        return;
                    }
                    var entry = new FolderEntry() { Id = GenerateUniqueId(dbService), Path = absolutePath, DateAdded = DateTime.Now };
                    if (await dbService.AddFolderEntryAsync(entry))
                    {
                        TuiHelper.DisplaySuccess("Directory added successfully.");
                        return;
                    }
                    else
                    {
                        TuiHelper.DisplayError("Failed to add directory.");
                        return;
                    }
                }
                else
                {
                    TuiHelper.DisplayError($"Path does not exist: {absolutePath}");
                    return;
                }
            }
            else
            {
                TuiHelper.DisplayError("No path provided. Please specify a file or folder to add.");
                return;
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
