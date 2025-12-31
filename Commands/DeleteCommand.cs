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

                if (string.IsNullOrEmpty(filePath))
                {
                    TuiHelper.DisplayError($"No entry found with ID {id}.");
                    return;
                }

                // Display entry details before confirmation
                TuiHelper.DisplayHeader("TBDel - Delete Entry");
                Console.WriteLine($"Entry ID: {id}");
                Console.WriteLine($"Path: {filePath}");
                Console.WriteLine();

                if (!TuiHelper.GetConfirmation($"Permanently delete this entry?"))
                {
                    TuiHelper.DisplayInfo("Operation cancelled.");
                    return;
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    if (await dbService.RemoveFileEntryAsync(id))
                    {
                        TuiHelper.DisplaySuccess("File deleted successfully.");
                    }
                    else
                    {
                        TuiHelper.DisplayError("Something went wrong while deleting the file.");
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
                        if (e.Message.Contains("Directory not empty"))
                        {
                            TuiHelper.DisplayError("Directory is not empty. It must be empty before it can be deleted or deleted manually.");
                        }
                        else
                        {
                            TuiHelper.DisplayError("Something went wrong while deleting the directory.");
                            Console.WriteLine(e.Message);
                        }
                        return;
                    }

                    if (await dbService.RemoveFolderEntryAsync(id))
                    {
                        TuiHelper.DisplaySuccess("Directory deleted successfully.");
                    }
                    else
                    {
                        TuiHelper.DisplayError("Something went wrong while deleting the directory.");
                    }
                }
            }
            else
            {
                TuiHelper.DisplayError("Invalid ID provided. Please provide a valid numeric ID.");
            }
        }
    }
}