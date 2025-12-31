using System;
using TBDel.Models;

namespace TBDel.Services
{
    public static class TuiHelper
    {
        // Color constants for consistent styling
        public static readonly ConsoleColor HeaderColor = ConsoleColor.Cyan;
        public static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
        public static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
        public static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
        public static readonly ConsoleColor InfoColor = ConsoleColor.White;
        public static readonly ConsoleColor BorderColor = ConsoleColor.DarkGray;

        // Display a styled header
        public static void DisplayHeader(string title)
        {
            var borderLength = Math.Max(title.Length + 4, 30); // Ensure minimum length
            var border = new string('═', borderLength);
            Console.ForegroundColor = HeaderColor;
            Console.WriteLine($"\n╔{border}╗");
            // Center the title within the border
            var padding = (borderLength - title.Length) / 2;
            var leftPadding = padding;
            var rightPadding = borderLength - title.Length - leftPadding;
            var paddedTitle = new string(' ', leftPadding) + title + new string(' ', rightPadding);
            Console.WriteLine($"║{paddedTitle}║");
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
        }

        // Display a styled section separator
        public static void DisplaySectionSeparator(int length = 80)
        {
            Console.ForegroundColor = BorderColor;
            Console.WriteLine(new string('─', length));
            Console.ResetColor();
        }

        // Display a styled table header
        public static void DisplayTableHeader(string idLabel, string pathLabel, string dateLabel, int idWidth = 10, int pathWidth = 70, int dateWidth = 20)
        {
            Console.ForegroundColor = HeaderColor;
            Console.WriteLine("{0,-" + idWidth + "} │ {1,-" + pathWidth + "} │ {2,-" + dateWidth + "}", idLabel, pathLabel, dateLabel);
            DisplayTableSeparator(idWidth, pathWidth, dateWidth);
            Console.ResetColor();
        }

        // Display a table separator line
        public static void DisplayTableSeparator(int idWidth = 10, int pathWidth = 60, int dateWidth = 20)
        {
            Console.ForegroundColor = BorderColor;
            // Calculate total width: idWidth + " │ " + pathWidth + " │ " + dateWidth
            var totalWidth = idWidth + 3 + pathWidth + 3 + dateWidth; // 3 chars for " │ " separator
            Console.WriteLine(new string('─', totalWidth));
            Console.ResetColor();
        }

        // Display a table row
        public static void DisplayTableRow(uint id, string path, DateTime dateAdded, int idWidth = 10, int pathWidth = 70, int dateWidth = 20)
        {
            var formattedDate = dateAdded.ToString("yyyy-MM-dd HH:mm");
            var truncatedPath = path.Length > pathWidth ? path.Substring(0, pathWidth - 3) + "..." : path;
            Console.WriteLine("{0,-" + idWidth + "} │ {1,-" + pathWidth + "} │ {2,-" + dateWidth + "}", id, truncatedPath, formattedDate);
        }

        // Display a confirmation prompt with styled formatting
        public static bool GetConfirmation(string message)
        {
            Console.ForegroundColor = WarningColor;
            Console.Write($"⚠ {message} (y/N): ");
            Console.ResetColor();
            var input = Console.ReadLine();
            return input?.ToLowerInvariant() == "y";
        }

        // Display a success message
        public static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = SuccessColor;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }

        // Display an error message
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ErrorColor;
            Console.WriteLine($"✗ {message}");
            Console.ResetColor();
        }

        // Display an info message
        public static void DisplayInfo(string message)
        {
            Console.ForegroundColor = InfoColor;
            Console.WriteLine($"ℹ {message}");
            Console.ResetColor();
        }

        // Display a warning message
        public static void DisplayWarning(string message)
        {
            Console.ForegroundColor = WarningColor;
            Console.WriteLine($"⚠ {message}");
            Console.ResetColor();
        }

        // Display a styled summary box
        public static void DisplaySummary(string title, params (string label, string value)[] items)
        {
            // Calculate the max width needed for the content
            int contentWidth = Math.Max(title.Length + 2, 28); // +2 for the ─ ─ spacing around title, minimum 30 total
            foreach (var (label, value) in items)
            {
                int itemLength = (label + ": " + value).Length;
                if (itemLength > contentWidth) contentWidth = itemLength;
            }

            Console.ForegroundColor = HeaderColor;
            var topBorder = $"┌─ {title} ─{'─'.Repeat(Math.Max(0, contentWidth - title.Length - 2))}┐";
            Console.WriteLine($"\n{topBorder}");
            Console.ResetColor();

            foreach (var (label, value) in items)
            {
                var content = $"│ {label}: {value}";
                var padding = contentWidth - (label.Length + value.Length + 2); // 2 for ": "
                var paddedContent = content + new string(' ', padding) + "│";
                Console.WriteLine(paddedContent);
            }

            Console.ForegroundColor = HeaderColor;
            var bottomBorder = $"└{'─'.Repeat(contentWidth + 2)}┘"; // +2 for the │ │ borders
            Console.WriteLine(bottomBorder);
            Console.ResetColor();
        }
    }

    // Extension method to repeat a character
    public static class StringExtensions
    {
        public static string Repeat(this char c, int count)
        {
            return new string(c, count);
        }
    }
}