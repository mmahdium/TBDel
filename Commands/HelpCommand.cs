
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Invalid command. Use 'tbdel help' for help.");
            Console.ResetColor();
        }
        Console.WriteLine("Usage: tbdel <command> [arguments]");
        Console.WriteLine("Available commands:");
        Console.WriteLine("  add <path to file or folder>       Add a file or folder to the list");
        Console.WriteLine("  delete <path to file or folder OR file ID>         Deletes a file or folder from the list");
        Console.WriteLine("  deleteall        Deletes all items in the list");
        Console.WriteLine("  list             Lists all items in the list");
        Console.WriteLine("  help             Shows this help message");
    }
}