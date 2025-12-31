using TBDel.Services;

namespace TBDel.Commands;

public class RemoveFromListCommand
{
    public static async Task DeleteEntryFromList(String[] args)
    {
        var dbService = new DbService();
        if (args.Length > 1 && uint.TryParse(args[1], out uint id))
        {
            var filePath = await dbService.GetEntryPath(id);

            if (string.IsNullOrEmpty(filePath))
            {
                TuiHelper.DisplayError($"No entry found with ID {id}.");
                return;
            }

            TuiHelper.DisplayHeader("TBDel - Remove from List");
            Console.WriteLine($"Entry ID: {id}");
            Console.WriteLine($"Path: {filePath}");
            Console.WriteLine();

            if (!TuiHelper.GetConfirmation("Remove this entry ONLY from the list?"))
            {
                TuiHelper.DisplayInfo("Operation cancelled.");
                return;
            }

            if (await dbService.RemoveFileEntryAsync(id) || await dbService.RemoveFolderEntryAsync(id))
            {
                TuiHelper.DisplaySuccess("Entry removed from the list.");
            }
            else
            {
                TuiHelper.DisplayError("Something went wrong while removing the entry from list.");
            }
        }
        else
        {
            TuiHelper.DisplayError("Invalid ID provided. Please provide a valid numeric ID.");
        }
    }
}