using TBDel.Commands;

namespace TBDel
{
    internal class Program
    {
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
                case "rmflist":
                    await RemoveFromListCommand.DeleteEntryFromList(args);
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