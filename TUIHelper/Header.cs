namespace TBDel.TUIHelper;

public class Header
{
    public static void Show(String _headerText)
    {
        // Get third of the terminal width
        int viewWidth = Console.WindowWidth/3;
        
        // Show the header seperator
        Console.Write("#");
        for (int i = 0; i <= viewWidth-2; i++)
        {
            Console.Write("=");
        } 
        
        // Move to the next line
        Console.WriteLine("#");

        Console.Write("#");
        
        for (int i = 0; i <= viewWidth - 2;i++)
        {
            Console.Write(" ");
        }
        
        Console.Write("#");
        Console.Write("\n#");
        
        int textReletiveStartPosition = ((viewWidth - 2)- _headerText.Length)/2;
        
        for (int i = 0; i < textReletiveStartPosition; i++)
        {
            Console.Write(" ");
        }
        
        Console.Write(_headerText);
        
        for (int i = 0; i <= textReletiveStartPosition; i++)
        {
            Console.Write(" ");
        }
        Console.WriteLine("#");
        Console.Write("#");
        
        
        for (int i = 0; i <= viewWidth - 2;i++)
        {
            Console.Write(" ");
        }
        
        Console.Write("#");
        Console.Write("\n#");
        
        for (int i = 0; i <= viewWidth-2; i++)
        {
            Console.Write("=");
        } 
        Console.WriteLine("#");
    }
}