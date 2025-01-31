using TBDel.Commands;

namespace TBDel
{
    internal class Program
    {
        // TODO: Add a command to show the Db path
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
                    
                    break;
                case "deleteall":
                    
                    break;
                case "list":
                    
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