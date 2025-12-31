
using TBDel.Services;

namespace TBDel.Commands;

public static class HelpCommand
{
    // Show the help message
    /// <summary>
    /// Shows the help message.
    /// </summary>
    /// <param name="showOnWrongCommand">If true, displays an error message indicating that an invalid command was entered.</param>
    public static void Show(
        Boolean showOnWrongCommand = false)
    {
        if (showOnWrongCommand)
        {
            TuiHelper.DisplayError("Invalid command. Use 'tbdel help' for help.");
        }

        TuiHelper.DisplayHeader("TBDel - Help");

        Console.WriteLine("Usage: tbdel <command> [arguments]");
        Console.WriteLine();

        TuiHelper.DisplayInfo("Available commands:");
        Console.WriteLine();

        Console.WriteLine("  add <path to file or folder>     │ Add a file or folder to the list");
        Console.WriteLine("                                   │ Example: tbdel add ./myfile.txt");
        Console.WriteLine();

        Console.WriteLine("  delete <file or folder ID>       │ Delete a file or folder permanently");
        Console.WriteLine("                                   │ Example: tbdel delete 12345");
        Console.WriteLine();

        Console.WriteLine("  deleteall                        │ Delete all items in the list");
        Console.WriteLine("                                   │ Example: tbdel deleteall");
        Console.WriteLine();

        Console.WriteLine("  rmflist <entry ID>               │ Remove an entry ONLY from the list");
        Console.WriteLine("                                   │ Example: tbdel rmflist 12345");
        Console.WriteLine();

        Console.WriteLine("  list                             │ List all items in the list");
        Console.WriteLine("                                   │ Example: tbdel list");
        Console.WriteLine();

        Console.WriteLine("  list files                       │ List only files in the list");
        Console.WriteLine("                                   │ Example: tbdel list files");
        Console.WriteLine();

        Console.WriteLine("  list folders                     │ List only folders in the list");
        Console.WriteLine("                                   │ Example: tbdel list folders");
        Console.WriteLine();

        Console.WriteLine("  help                             │ Show this help message");
        Console.WriteLine("                                   │ Example: tbdel help");
        Console.WriteLine();
    }
}