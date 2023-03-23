using RubiksCubeSolver;
using System.Diagnostics;

sealed class Program
{
    const int X_OFFSET = 4;
    const int Y_OFFSET = 5;
    static RubiksCube3x3 cube = RubiksCube3x3.Solved;
    static readonly Dictionary<ConsoleKey, FaceRotation> keyMap =
        new()
        {
            { ConsoleKey.R, (FaceRotation)0 },
            { ConsoleKey.L, (FaceRotation)1 },
            { ConsoleKey.U, (FaceRotation)2 },
            { ConsoleKey.D, (FaceRotation)3 },
            { ConsoleKey.F, (FaceRotation)4 },
            { ConsoleKey.B, (FaceRotation)5 },
        };

    public static void Main()
    {
        #region // Generate pattern distance tables
        //using var f = File.Open("3x3LastSixEdgeDistances", FileMode.Create);
        //byte[] distances = new byte[21_288_960];

        //List<RubiksCube3x3> cubes = new() { RubiksCube3x3.Solved };
        //HashSet<int> cornerPermutations = new(cubes.Select(c => c.GetLastSixEdgesPermIndex()));
        //var rotations = (FaceRotation[])Enum.GetValues(typeof(FaceRotation));
        //for (int moves = 1; cubes.Count > 0; moves++)
        //{
        //    List<RubiksCube3x3> newCubes = new();
        //    foreach (var c in cubes)
        //    {
        //        foreach (FaceRotation fr in rotations)
        //        {
        //            var rotated = c.MakeRotation(fr);
        //            int index = rotated.GetLastSixEdgesPermIndex();
        //            if (cornerPermutations.Add(index))
        //            {
        //                distances[index / 2] |= (byte)((index % 2 == 0) ? moves : moves << 4);
        //                newCubes.Add(rotated);
        //            }
        //        }
        //    }
        //    cubes = newCubes;
        //    ConsoleHelper.WriteAt($"Depth: {moves}", 1, 0);
        //    ConsoleHelper.WriteAt($"Positions: {cornerPermutations.Count}", 1, 1);
        //}
        //f.Write(distances);
        #endregion

        ConsoleHelper.Resize(32, 14);
        cube.DrawCube(X_OFFSET, Y_OFFSET);

        // Main loop
        while (true)
        {
            Console.CursorVisible = false;
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

            Stopwatch sw = Stopwatch.StartNew();
            var solutionMoves = cube.Solve(out int nodeCount);
            sw.Stop();

            var solution = solutionMoves.Select(x => x.ToString().Replace("Prime", "`"));

            ConsoleHelper.WriteAt("".PadLeft(Console.BufferWidth), 0, 0);
            ConsoleHelper.WriteAt(solution.Any() ? string.Join(' ', solution) : "Solved", 1, 0);
            Console.CursorLeft = 1;
            Console.CursorTop++;
            Console.WriteLine($"Searched: {nodeCount}");
            Console.CursorLeft = 1;
            Console.WriteLine($"Time Taken: {sw.Elapsed.TotalSeconds:f3}s");
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
        cube = cube.MakeRotation((FaceRotation)modifier | rotation);

        cube.DrawCube(X_OFFSET, Y_OFFSET);
    }
}
