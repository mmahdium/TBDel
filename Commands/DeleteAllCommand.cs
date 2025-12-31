using TBDel.Services;

namespace TBDel.Commands;

public class DeleteAllCommand
{
    public static async Task DeleteAll()
    {
        var dbService = new DbService();
        var allFiles = await dbService.GetFileEntriesAsync();
        var allFolders = await dbService.GetFolderEntriesAsync();

        if (allFiles.Count == 0 && allFolders.Count == 0)
        {
            TuiHelper.DisplayInfo("No files or folders found.");
            return;
        }

        TuiHelper.DisplayHeader("TBDel - Delete All Entries");

        // Display summary before confirmation
        TuiHelper.DisplaySummary("Deletion Summary",
            ("Files to delete", allFiles.Count.ToString()),
            ("Folders to delete", allFolders.Count.ToString()),
            ("Total items", (allFiles.Count + allFolders.Count).ToString())
        );

        if (!TuiHelper.GetConfirmation("Permanently delete ALL entries?"))
        {
            TuiHelper.DisplayInfo("Operation cancelled.");
            return;
        }

        int deletedFiles = 0;
        int deletedFolders = 0;
        int failedDeletions = 0;

        foreach (var file in allFiles)
        {
            if (!File.Exists(file.Path))
            {
                TuiHelper.DisplayWarning($"File {file.Path} does not exist, removing from list.");
                await dbService.RemoveFileEntryAsync(file.Id);
                continue;
            }

            TuiHelper.DisplayInfo($"Deleting file: {file.Path}");
            try
            {
                File.Delete(file.Path);
                await dbService.RemoveFileEntryAsync(file.Id);
                deletedFiles++;
            }
            catch (Exception e)
            {
                TuiHelper.DisplayError("Something went wrong while deleting the file.");
                Console.WriteLine(e.Message);
                failedDeletions++;
            }
        }

        foreach (var folder in allFolders)
        {
            if (!Directory.Exists(folder.Path))
            {
                TuiHelper.DisplayWarning($"Directory {folder.Path} does not exist, removing from list.");
                await dbService.RemoveFolderEntryAsync(folder.Id);
                continue;
            }

            TuiHelper.DisplayInfo($"Deleting directory: {folder.Path}");
            try
            {
                Directory.Delete(folder.Path, true);
                await dbService.RemoveFolderEntryAsync(folder.Id);
                deletedFolders++;
            }
            catch (Exception e)
            {
                TuiHelper.DisplayError("Something went wrong while deleting the directory.");
                Console.WriteLine(e.Message);
                failedDeletions++;
            }
        }

        // Display final results
        TuiHelper.DisplayHeader("Deletion Results");
        TuiHelper.DisplaySummary("Summary",
            ("Files deleted", deletedFiles.ToString()),
            ("Folders deleted", deletedFolders.ToString()),
            ("Failed deletions", failedDeletions.ToString())
        );

        if (failedDeletions == 0)
        {
            TuiHelper.DisplaySuccess("All files and folders deleted successfully.");
        }
        else
        {
            TuiHelper.DisplayWarning("Some items could not be deleted.");
        }
    }
}