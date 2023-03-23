using RubiksCubeSolver;
using System.Diagnostics;

internal sealed class Program
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
        //using var f = File.Open("3x3LastSevenEdgeDistances", FileMode.Create);
        //byte[] distances = new byte[255_467_520];

        //int maxDepth = 0;
        //Queue<(Int128, int)> cubes = new();
        //cubes.Enqueue(((Int128)RubiksCube3x3.Solved, 0));
        //while (cubes.Count > 0)
        //{
        //    var (c, depth) = cubes.Dequeue();
        //    var cube = (RubiksCube3x3)c;
        //    depth++;
        //    foreach (FaceRotation fr in FaceRotationExtensions.Rotations)
        //    {
        //        var rotated = cube.MakeRotation(fr);
        //        int index = rotated.GetLastSevenEdgesPermIndex();
        //        bool indexIsEven = (index % 2) == 0;
        //        int indexOverTwo = index / 2;
        //        int val = indexIsEven
        //            ? distances[indexOverTwo] & 0xf
        //            : distances[indexOverTwo] >> 4;

        //        if (val != 0)
        //        {
        //            continue;
        //        }

        //        maxDepth = Math.Max(maxDepth, depth);
        //        distances[indexOverTwo] |= (byte)(indexIsEven ? depth & 0xf : depth << 4);
        //        cubes.Enqueue(((Int128)rotated, depth));
        //    }
        //}
        //ConsoleHelper.WriteAt($"Depth: {maxDepth}", 1, 0);
        //f.Write(distances);
        #endregion

        ConsoleHelper.Resize(32, 15);
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
            ConsoleHelper.WriteAt($"Searched: {nodeCount}", 1, 2);
            ConsoleHelper.WriteAt($"Time Taken: {sw.Elapsed}s", 1, 3);
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
