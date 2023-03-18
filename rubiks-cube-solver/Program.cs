using RubiksCubeSolver;

sealed class Program
{
    const int X_OFFSET = 4;
    const int Y_OFFSET = 4;
    static RubiksCube2x2 cube = RubiksCube2x2.Solved;
    static readonly Dictionary<ConsoleKey, FaceRotation> keyMap =
        new()
        {
            { ConsoleKey.R, FaceRotation.R },
            { ConsoleKey.L, FaceRotation.L },
            { ConsoleKey.U, FaceRotation.U },
            { ConsoleKey.D, FaceRotation.D },
            { ConsoleKey.F, FaceRotation.F },
            { ConsoleKey.B, FaceRotation.B },
        };

    public static void Main()
    {
        ConsoleHelper.Resize(32, 14);
        cube.DrawCube(X_OFFSET, Y_OFFSET);

        // Main loop
        while (true)
        {
            Console.CursorVisible = false;
            Thread.Sleep(40);

            // Check for key presses
            while (Console.KeyAvailable)
                HandleKeyPress(Console.ReadKey(true));
        }
    }

    static void HandleKeyPress(ConsoleKeyInfo keyInfo)
    {
        // If pressed spacebar, print solution
        if (keyInfo.Key == ConsoleKey.Spacebar)
        {
            Console.Clear();
            cube.DrawCube(X_OFFSET, Y_OFFSET);
            ConsoleHelper.WriteAt("Solving...", 1, 0);

            var solution = cube.Solve().Select(x => x.ToString().Replace("Prime", "`"));
            ConsoleHelper.WriteAt("".PadLeft(Console.BufferWidth), 0, 0);
            ConsoleHelper.WriteAt(solution.Any() ? string.Join(' ', solution) : "Solved", 1, 0);
            cube.DrawCube(X_OFFSET, Y_OFFSET);
        }

        if (!keyMap.ContainsKey(keyInfo.Key))
            return;

        // If pressed a rotation key, rotate cube
        FaceRotation rotation = keyMap[keyInfo.Key];
        FaceRotationModifier modifier =
            keyInfo.Modifiers == ConsoleModifiers.Shift
                ? FaceRotationModifier.AntiClockwise
                : keyInfo.Modifiers == ConsoleModifiers.Alt
                    ? FaceRotationModifier.Double
                    : FaceRotationModifier.Clockwise;
        cube = cube.MakeRotation((FaceRotation)((int)modifier + (int)rotation));

        cube.DrawCube(X_OFFSET, Y_OFFSET);
    }
}
