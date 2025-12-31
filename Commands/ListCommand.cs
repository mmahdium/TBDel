using TBDel.Models;
using TBDel.Services;

namespace TBDel.Commands
{
    public class ListCommand
    {
        public static async Task List(string[] args)
        {
            if (args.Length > 2)
            {
                TuiHelper.DisplayError("Too many arguments.");
                return;
            }

            if (args.Length == 1)
            {
                TuiHelper.DisplayHeader("TBDel - List All Items");
                TuiHelper.DisplayInfo("Listing both files and folders.");

                await ListFiles();
                TuiHelper.DisplaySectionSeparator();
                await ListFolders();
            }
            else if (args[1] == "files")
            {
                TuiHelper.DisplayHeader("TBDel - List Files");
                await ListFiles();
            }
            else if (args[1] == "folders")
            {
                TuiHelper.DisplayHeader("TBDel - List Folders");
                await ListFolders();
            }
        }

        private static async Task ListFiles()
        {
            var dbService = new DbService();
            var filesList = await dbService.GetFileEntriesAsync();

            if (filesList.Count == 0)
            {
                TuiHelper.DisplayInfo("No files found.");
                return;
            }

            TuiHelper.DisplayInfo($"Found {filesList.Count} file(s):");
            Console.WriteLine();
            TuiHelper.DisplayTableHeader("ID", "Path", "Date Added", 10, 60, 20);

            foreach (var file in filesList)
            {
                TuiHelper.DisplayTableRow(file.Id, file.Path, file.DateAdded, 10, 60, 20);
            }
        }

        private static async Task ListFolders()
        {
            var dbService = new DbService();
            var foldersList = await dbService.GetFolderEntriesAsync();

            if (foldersList.Count == 0)
            {
                TuiHelper.DisplayInfo("No folders found.");
                return;
            }

            TuiHelper.DisplayInfo($"Found {foldersList.Count} folder(s):");
            Console.WriteLine();
            TuiHelper.DisplayTableHeader("ID", "Path", "Date Added", 10, 60, 20);

            foreach (var folder in foldersList)
            {
                TuiHelper.DisplayTableRow(folder.Id, folder.Path, folder.DateAdded, 10, 60, 20);
            }
        }
    }
}
