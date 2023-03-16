using RubiksCubeSolver;

sealed class Program
{
    public static void Main()
    {
        ConsoleHelper.Resize(32, 14);

        Dictionary<ConsoleKey, FaceRotation> keyMap =
            new()
            {
                { ConsoleKey.R, FaceRotation.R },
                { ConsoleKey.L, FaceRotation.L },
                { ConsoleKey.U, FaceRotation.U },
                { ConsoleKey.D, FaceRotation.D },
                { ConsoleKey.F, FaceRotation.F },
                { ConsoleKey.B, FaceRotation.B },
            };

        const int X_OFFSET = 4;
        const int Y_OFFSET = 4;
        RubiksCube3x3 cube = RubiksCube3x3.Solved;
        cube.DrawCube(X_OFFSET, Y_OFFSET);

        // Main loop
        while (true)
        {
            Console.CursorVisible = false;
            Thread.Sleep(20);
            // Check for key presses
            ConsoleKeyInfo keyPressed = Console.KeyAvailable
                ? Console.ReadKey(true)
                : new ConsoleKeyInfo();

            // If pressed spacebar, print solution
            if (keyPressed.Key == ConsoleKey.Spacebar)
            {
                Console.Clear();
                cube.DrawCube(X_OFFSET, Y_OFFSET);
                ConsoleHelper.WriteAt("Solving...", 1, 0);

                var solution = cube.Solve().Select(x => x.ToString().Replace("Prime", "`"));
                Console.Clear();
                ConsoleHelper.WriteAt(solution.Any() ? string.Join(' ', solution) : "Solved", 1, 0);
                cube.DrawCube(X_OFFSET, Y_OFFSET);
            }

            if (!keyMap.ContainsKey(keyPressed.Key))
                continue;
            
            // If pressed a rotation key, rotate cube
            FaceRotation rotation = keyMap[keyPressed.Key];
            FaceRotationModifier modifier =
                keyPressed.Modifiers == ConsoleModifiers.Shift
                    ? FaceRotationModifier.AntiClockwise
                    : keyPressed.Modifiers == ConsoleModifiers.Alt
                        ? FaceRotationModifier.Double
                        : FaceRotationModifier.Clockwise;
            cube = cube.MakeRotation((FaceRotation)((int)modifier + (int)rotation));

            cube.DrawCube(X_OFFSET, Y_OFFSET);
        }
    }
}
