using TBDel.Commands;

namespace TBDel
{
    internal class Program
    {
        // TODO: Add a command to show the Db path
        // TODO: Add a command to remove a file or folder only from the list
        static async Task Main(string[] args)
        {
            // Show the help message if no arguments are provided
            if (args.Length == 0)
            {
                HelpCommand.Show();
                return;
            }

            string command = args[0].ToLower();

            switch (command)
            {
                case "add":
                    await AddCommand.AddEntry(args);
                    break;
                case "delete":
                    await DeleteCommand.DeleteEntry(args);
                    break;
                case "deleteall":
                    await DeleteAllCommand.DeleteAll();
                    break;
                case "list":
                    await ListCommand.List(args);
                    break;
                case "help":
                    HelpCommand.Show();
                    break;
                default:
                    HelpCommand.Show(true);
                    break;
            }
        }
        
    }
}