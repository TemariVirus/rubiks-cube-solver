namespace RubiksCubeSolver;

static class ConsoleHelper
{
    public static void Resize(int width, int height)
    {
        width = Math.Clamp(width, 0, Console.LargestWindowWidth);
        height = Math.Clamp(height, 0, Console.LargestWindowHeight);
        Console.CursorLeft = 0;
        Console.CursorTop = 0;
        Console.SetWindowSize(width, height);
        Console.SetBufferSize(width, height);
    }

    public static void WriteAt(object text, int x, int y, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
    {
        Console.CursorLeft = x;
        Console.CursorTop = y;
        Console.ForegroundColor = foreground;
        Console.BackgroundColor = background;
        Console.Write(text);
    }
}
