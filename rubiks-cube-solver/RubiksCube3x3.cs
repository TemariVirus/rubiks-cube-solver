using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

internal readonly struct RubiksCube3x3 : IRubiksCube<RubiksCube3x3>, IEquatable<RubiksCube3x3>
{
    #region // Transformations
    static PermutationMatrix None =>
        new(
            new(
                Piece.WBR.Id,
                Piece.WRG.Id,
                Piece.WGO.Id,
                Piece.WOB.Id,
                Piece.YBO.Id,
                Piece.YRB.Id,
                Piece.YGR.Id,
                Piece.YOG.Id,
                Piece.WR.Id,
                Piece.WG.Id,
                Piece.WO.Id,
                Piece.WB.Id,
                Piece.RB.Id,
                Piece.GR.Id,
                Piece.OG.Id,
                Piece.BO.Id,
                Piece.YB.Id,
                Piece.YR.Id,
                Piece.YG.Id,
                Piece.YO.Id
            ),
            new(
                Piece.WBR,
                Piece.WRG,
                Piece.WGO,
                Piece.WOB,
                Piece.YBO,
                Piece.YRB,
                Piece.YGR,
                Piece.YOG,
                Piece.WR,
                Piece.WG,
                Piece.WO,
                Piece.WB,
                Piece.RB,
                Piece.GR,
                Piece.OG,
                Piece.BO,
                Piece.YB,
                Piece.YR,
                Piece.YG,
                Piece.YO
            )
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.None => None;
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.R =>
        new(
            new(
                Piece.WOB.Id,
                Piece.WRG.Id,
                Piece.WGO.Id,
                Piece.YBO.Id,
                Piece.YRB.Id,
                Piece.WBR.Id,
                Piece.YGR.Id,
                Piece.YOG.Id,
                Piece.WR.Id,
                Piece.WG.Id,
                Piece.WO.Id,
                Piece.BO.Id,
                Piece.WB.Id,
                Piece.GR.Id,
                Piece.OG.Id,
                Piece.YB.Id,
                Piece.RB.Id,
                Piece.YR.Id,
                Piece.YG.Id,
                Piece.YO.Id
            ),
            new(
                Piece.WBR.Rotate(2),
                Piece.WRG,
                Piece.WGO,
                Piece.WOB.Rotate(1),
                Piece.YBO.Rotate(2),
                Piece.YRB.Rotate(1),
                Piece.YGR,
                Piece.YOG,
                Piece.WR,
                Piece.WG,
                Piece.WO,
                Piece.WB.Rotate(1),
                Piece.RB.Rotate(0),
                Piece.GR,
                Piece.OG,
                Piece.BO.Rotate(1),
                Piece.YB.Rotate(0),
                Piece.YR,
                Piece.YG,
                Piece.YO
            )
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.L =>
        new(
            new(
                Piece.WBR.Id,
                Piece.YGR.Id,
                Piece.WRG.Id,
                Piece.WOB.Id,
                Piece.YBO.Id,
                Piece.YRB.Id,
                Piece.YOG.Id,
                Piece.WGO.Id,
                Piece.WR.Id,
                Piece.GR.Id,
                Piece.WO.Id,
                Piece.WB.Id,
                Piece.RB.Id,
                Piece.YG.Id,
                Piece.WG.Id,
                Piece.BO.Id,
                Piece.YB.Id,
                Piece.YR.Id,
                Piece.OG.Id,
                Piece.YO.Id
            ),
            new(
                Piece.WBR,
                Piece.WRG.Rotate(1),
                Piece.WGO.Rotate(2),
                Piece.WOB,
                Piece.YBO,
                Piece.YRB,
                Piece.YGR.Rotate(2),
                Piece.YOG.Rotate(1),
                Piece.WR,
                Piece.WG.Rotate(1),
                Piece.WO,
                Piece.WB,
                Piece.RB,
                Piece.GR.Rotate(1),
                Piece.OG.Rotate(0),
                Piece.BO,
                Piece.YB,
                Piece.YR,
                Piece.YG.Rotate(0),
                Piece.YO
            )
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.U =>
        new(
            new(
                Piece.WRG.Id,
                Piece.WGO.Id,
                Piece.WOB.Id,
                Piece.WBR.Id,
                Piece.YBO.Id,
                Piece.YRB.Id,
                Piece.YGR.Id,
                Piece.YOG.Id,
                Piece.WG.Id,
                Piece.WO.Id,
                Piece.WB.Id,
                Piece.WR.Id,
                Piece.RB.Id,
                Piece.GR.Id,
                Piece.OG.Id,
                Piece.BO.Id,
                Piece.YB.Id,
                Piece.YR.Id,
                Piece.YG.Id,
                Piece.YO.Id
            ),
            new(
                Piece.WBR.Rotate(0),
                Piece.WRG.Rotate(0),
                Piece.WGO.Rotate(0),
                Piece.WOB.Rotate(0),
                Piece.YBO,
                Piece.YRB,
                Piece.YGR,
                Piece.YOG,
                Piece.WR.Rotate(0),
                Piece.WG.Rotate(0),
                Piece.WO.Rotate(0),
                Piece.WB.Rotate(0),
                Piece.RB,
                Piece.GR,
                Piece.OG,
                Piece.BO,
                Piece.YB,
                Piece.YR,
                Piece.YG,
                Piece.YO
            )
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.D =>
        new(
            new(
                Piece.WBR.Id,
                Piece.WRG.Id,
                Piece.WGO.Id,
                Piece.WOB.Id,
                Piece.YOG.Id,
                Piece.YBO.Id,
                Piece.YRB.Id,
                Piece.YGR.Id,
                Piece.WR.Id,
                Piece.WG.Id,
                Piece.WO.Id,
                Piece.WB.Id,
                Piece.RB.Id,
                Piece.GR.Id,
                Piece.OG.Id,
                Piece.BO.Id,
                Piece.YO.Id,
                Piece.YB.Id,
                Piece.YR.Id,
                Piece.YG.Id
            ),
            new(
                Piece.WBR,
                Piece.WRG,
                Piece.WGO,
                Piece.WOB,
                Piece.YBO,
                Piece.YRB,
                Piece.YGR,
                Piece.YOG,
                Piece.WR,
                Piece.WG,
                Piece.WO,
                Piece.WB,
                Piece.RB,
                Piece.GR,
                Piece.OG,
                Piece.BO,
                Piece.YB.Rotate(0),
                Piece.YR.Rotate(0),
                Piece.YG.Rotate(0),
                Piece.YO.Rotate(0)
            )
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.F =>
        new(
            new(
                Piece.YRB.Id,
                Piece.WBR.Id,
                Piece.WGO.Id,
                Piece.WOB.Id,
                Piece.YBO.Id,
                Piece.YGR.Id,
                Piece.WRG.Id,
                Piece.YOG.Id,
                Piece.RB.Id,
                Piece.WG.Id,
                Piece.WO.Id,
                Piece.WB.Id,
                Piece.YR.Id,
                Piece.WR.Id,
                Piece.OG.Id,
                Piece.BO.Id,
                Piece.YB.Id,
                Piece.GR.Id,
                Piece.YG.Id,
                Piece.YO.Id
            ),
            new(
                Piece.WBR.Rotate(1),
                Piece.WRG.Rotate(2),
                Piece.WGO,
                Piece.WOB,
                Piece.YBO,
                Piece.YRB.Rotate(2),
                Piece.YGR.Rotate(1),
                Piece.YOG,
                Piece.WR.Rotate(1),
                Piece.WG,
                Piece.WO,
                Piece.WB,
                Piece.RB.Rotate(1),
                Piece.GR.Rotate(0),
                Piece.OG,
                Piece.BO,
                Piece.YB,
                Piece.YR.Rotate(0),
                Piece.YG,
                Piece.YO
            )
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.B =>
        new(
            new(
                Piece.WBR.Id,
                Piece.WRG.Id,
                Piece.YOG.Id,
                Piece.WGO.Id,
                Piece.WOB.Id,
                Piece.YRB.Id,
                Piece.YGR.Id,
                Piece.YBO.Id,
                Piece.WR.Id,
                Piece.WG.Id,
                Piece.OG.Id,
                Piece.WB.Id,
                Piece.RB.Id,
                Piece.GR.Id,
                Piece.YO.Id,
                Piece.WO.Id,
                Piece.YB.Id,
                Piece.YR.Id,
                Piece.YG.Id,
                Piece.BO.Id
            ),
            new(
                Piece.WBR,
                Piece.WRG,
                Piece.WGO.Rotate(1),
                Piece.WOB.Rotate(2),
                Piece.YBO.Rotate(1),
                Piece.YRB,
                Piece.YGR,
                Piece.YOG.Rotate(2),
                Piece.WR,
                Piece.WG,
                Piece.WO.Rotate(1),
                Piece.WB,
                Piece.RB,
                Piece.GR,
                Piece.OG.Rotate(1),
                Piece.BO.Rotate(0),
                Piece.YB,
                Piece.YR,
                Piece.YG,
                Piece.YO.Rotate(0)
            )
        );
    #endregion

    public static RubiksCube3x3 Solved => new() { Matrix = None };

    private static readonly PatternDistancesTable CornerDistances = new("3x3CornerDistances");
    private static readonly PatternDistancesTable FirstSixEdgeDistances =
        new("3x3FirstSixEdgeDistances");
    private static readonly PatternDistancesTable LastSixEdgeDistances =
        new("3x3LastSixEdgeDistances");

    private static readonly Dictionary<Int128, FaceRotation[]> NearSolutions =
        Solved.GenerateNearSolutions(1);

    public PermutationMatrix Matrix { get; init; }

    public RubiksCube3x3 ApplyTransformation(PermutationMatrix other) =>
        new() { Matrix = Matrix * other };

    public void DrawCube(int xOffset, int yOffset)
    {
        // Green face (left)
        ConsoleHelper.WriteAt(
            "[]",
            0 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YOG.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            2 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.OG.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WGO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            0 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.YG.Id)[1]
        );
        ConsoleHelper.WriteAt("[]", 2 + xOffset, 4 + yOffset, ConsoleColor.Green);
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.WG.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            0 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YGR.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            2 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.GR.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.WRG.Id)[2]
        );
        // White face (middle left)
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WGO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WOB.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.WG.Id)[0]
        );
        ConsoleHelper.WriteAt("[]", 8 + xOffset, 4 + yOffset, ConsoleColor.White);
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.WB.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.WRG.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.WR.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.WBR.Id)[0]
        );
        // Orange face (top)
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            0 + yOffset,
            Matrix.GetColumnValue(Piece.YOG.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            0 + yOffset,
            Matrix.GetColumnValue(Piece.YO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            0 + yOffset,
            Matrix.GetColumnValue(Piece.YBO.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            1 + yOffset,
            Matrix.GetColumnValue(Piece.OG.Id)[0]
        );
        ConsoleHelper.WriteAt("[]", 8 + xOffset, 1 + yOffset, ConsoleColor.DarkYellow);
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            1 + yOffset,
            Matrix.GetColumnValue(Piece.BO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WGO.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WOB.Id)[1]
        );
        // Red face (bottom)
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            6 + yOffset,
            Matrix.GetColumnValue(Piece.WRG.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            6 + yOffset,
            Matrix.GetColumnValue(Piece.WR.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            6 + yOffset,
            Matrix.GetColumnValue(Piece.WBR.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            7 + yOffset,
            Matrix.GetColumnValue(Piece.GR.Id)[1]
        );
        ConsoleHelper.WriteAt("[]", 8 + xOffset, 7 + yOffset, ConsoleColor.Red);
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            7 + yOffset,
            Matrix.GetColumnValue(Piece.RB.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            8 + yOffset,
            Matrix.GetColumnValue(Piece.YGR.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            8 + yOffset,
            Matrix.GetColumnValue(Piece.YR.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            8 + yOffset,
            Matrix.GetColumnValue(Piece.YRB.Id)[1]
        );
        // Blue face (middle right)
        ConsoleHelper.WriteAt(
            "[]",
            12 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WOB.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            14 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.BO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            16 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YBO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            12 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.WB.Id)[1]
        );
        ConsoleHelper.WriteAt("[]", 14 + xOffset, 4 + yOffset, ConsoleColor.Blue);
        ConsoleHelper.WriteAt(
            "[]",
            16 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.YB.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            12 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.WBR.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            14 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.RB.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            16 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YRB.Id)[2]
        );
        // Yellow face (right)
        ConsoleHelper.WriteAt(
            "[]",
            18 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YBO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            20 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            22 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YOG.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            18 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.YB.Id)[0]
        );
        ConsoleHelper.WriteAt("[]", 20 + xOffset, 4 + yOffset, ConsoleColor.Yellow);
        ConsoleHelper.WriteAt(
            "[]",
            22 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.YG.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            18 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YRB.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            20 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YR.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            22 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YGR.Id)[0]
        );
    }

    public FaceRotation[] Solve(out int count)
    {
        count = 0;
        if (NearSolutions.TryGetValue((Int128)this, out FaceRotation[]? solution))
            return solution;

        solution = new FaceRotation[20];
        Stack<(RubiksCube3x3, FaceRotation, int)> cubes = new();
        PriorityQueue<(RubiksCube3x3, FaceRotation, int), int> newCubes = new();
        // Iterative deepening A*
        int maxdepth = GetSolveLengthLowerBound();
        for (; true; )
        {
            int nextMaxDepth = 20; // Cannot exceed God's number
            // Start search with all cubes 1 move away
            count += IterateAllMoves(this, (FaceRotation)7, 0, ref nextMaxDepth);

            // Depth-first heuristic-based search (A*)
            while (cubes.Count > 0)
            {
                var (cube, rotation, depth) = cubes.Pop();
                solution[depth - 1] = rotation;
                if (NearSolutions.TryGetValue((Int128)cube, out FaceRotation[]? solutionEnd))
                    return solution[..depth].Concat(solutionEnd).ToArray();

                count += IterateAllMoves(cube, rotation, depth, ref nextMaxDepth);
            }
            maxdepth = nextMaxDepth;

            ConsoleHelper.WriteAt($"Searched: {count}", 1, 2);
        }

        int IterateAllMoves(
            RubiksCube3x3 cube,
            FaceRotation rotation,
            int depth,
            ref int nextMaxDepth
        )
        {
            // Try all possible moves on cube
            depth++;
            foreach (var fr in FaceRotationExtensions.Rotations)
            {
                // Skip if rotation is in same direction as previous move
                if (((int)fr & 0x7) == ((int)rotation & 0x7))
                    continue;

                var rotated = cube.MakeRotation(fr);

                // Skip if lower bound is too high
                int solveLengthLowerBound = depth + rotated.GetSolveLengthLowerBound();
                if (solveLengthLowerBound > maxdepth)
                {
                    nextMaxDepth = Math.Min(solveLengthLowerBound, nextMaxDepth);
                    continue;
                }

                // Negate lower bound so that lowest lower bounds are dequeued last
                newCubes.Enqueue((rotated, fr, depth), -solveLengthLowerBound + depth);
            }

            // Push new states to stack, with lowest lower bounds last
            int count = newCubes.Count;
            while (newCubes.Count > 0)
                cubes.Push(newCubes.Dequeue());

            return count;
        }
    }

    private Dictionary<Int128, FaceRotation[]> GenerateNearSolutions(int depth)
    {
        Dictionary<Int128, FaceRotation[]> nearSolutions =
            new() { { (Int128)this, Array.Empty<FaceRotation>() } };

        List<RubiksCube3x3> cubes = new() { this };
        for (int i = 0; i < depth; i++)
        {
            List<RubiksCube3x3> nextCubes = new();
            foreach (var cube in cubes)
            {
                foreach (var fr in FaceRotationExtensions.Rotations)
                {
                    var rotated = cube.MakeRotation(fr);

                    if (
                        !nearSolutions.TryAdd(
                            (Int128)rotated,
                            nearSolutions[(Int128)cube].Prepend(fr.ReverseRotation()).ToArray()
                        )
                    )
                        continue;

                    nextCubes.Add(rotated);
                }
            }
            cubes = nextCubes;
        }

        return nearSolutions;
    }

    private int GetCornerPermIndex()
    {
        const int ORIENTATION_RANKS_COUNT = 2187; // 2187 = 3^7

        int permutationRank = Matrix.RowPositions[7];
        int orientationRank = Matrix.RowValues[7].Parity;
        Span<bool> usedIndices = stackalloc bool[8];
        usedIndices[permutationRank] = true;
        permutationRank *= 7;
        for (int i = 6; i > 0; i--)
        {
            int pos = Matrix.RowPositions[i];
            permutationRank += pos;
            for (int j = 0; j < pos; j++)
                if (usedIndices[j])
                    permutationRank--;
            permutationRank *= i;
            usedIndices[pos] = true;

            orientationRank *= 3;
            orientationRank += Matrix.RowValues[i].Parity;
        }

        return permutationRank * ORIENTATION_RANKS_COUNT + orientationRank;
    }

    private int GetFirstSixEdgesPermIndex()
    {
        const int OFFSET = 8;

        int permutationRank = 0;
        int orientationRank = 0;
        Span<bool> usedIndices = stackalloc bool[12];
        for (int i = 0; i < 6; i++)
        {
            int index = i + OFFSET;
            int pos = Matrix.RowPositions[index] - 8;
            permutationRank *= 12 - i;
            permutationRank += pos;
            for (int j = 0; j < pos; j++)
                if (usedIndices[j])
                    permutationRank--;
            usedIndices[pos] = true;

            orientationRank <<= 1;
            orientationRank |= Matrix.RowValues[index].Parity;
        }

        return (permutationRank << 6) | orientationRank;
    }

    private int GetLastSixEdgesPermIndex()
    {
        const int OFFSET = 8 + 6;

        int permutationRank = 0;
        int orientationRank = 0;
        Span<bool> usedIndices = stackalloc bool[12];
        for (int i = 0; i < 6; i++)
        {
            int index = i + OFFSET;
            int pos = Matrix.RowPositions[index] - 8;
            permutationRank *= 12 - i;
            permutationRank += pos;
            for (int j = 0; j < pos; j++)
                if (usedIndices[j])
                    permutationRank--;
            usedIndices[pos] = true;

            orientationRank <<= 1;
            orientationRank |= Matrix.RowValues[index].Parity;
        }

        return (permutationRank << 6) | orientationRank;
    }

    public int GetPieceOrientationPermIndex()
    {
        int orientationRank = Matrix.RowValues[0].Parity;
        for (int i = 1; i < 7; i++)
        {
            orientationRank *= 3;
            orientationRank += Matrix.RowValues[i].Parity;
        }
        for (int i = 8; i < 19; i++)
        {
            orientationRank <<= 1;
            orientationRank |= Matrix.RowValues[i].Parity;
        }

        return orientationRank;
    }

    private int GetSolveLengthLowerBound()
    {
        int dist1 = CornerDistances.GetDistance(GetCornerPermIndex());
        int dist2 = FirstSixEdgeDistances.GetDistance(GetFirstSixEdgesPermIndex());
        int dist3 = LastSixEdgeDistances.GetDistance(GetLastSixEdgesPermIndex());
        if (dist1 >= dist2 && dist1 >= dist3)
            return dist1;
        if (dist2 >= dist3)
            return dist2;
        return dist3;
    }

    public override int GetHashCode() => ((Int128)this).GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is RubiksCube3x3 cube && this == cube;

    public bool Equals(RubiksCube3x3 other) => this == other;

    public static bool operator ==(RubiksCube3x3 left, RubiksCube3x3 right) =>
        left.Matrix == right.Matrix;

    public static bool operator !=(RubiksCube3x3 left, RubiksCube3x3 right) =>
        left.Matrix != right.Matrix;

    public static explicit operator Int128(RubiksCube3x3 cube)
    {
        Int128 ans = 0;
        for (int i = 0; i < 8; i++)
            ans |=
                (Int128)(cube.Matrix.RowValues[i].Parity << 3 | cube.Matrix.RowPositions[i])
                << (i * 6);
        for (int i = 8; i < 20; i++)
            ans |=
                (Int128)(cube.Matrix.RowValues[i].Parity << 5 | cube.Matrix.RowPositions[i])
                << (i * 6);
        return ans;
    }

    public static explicit operator RubiksCube3x3(Int128 value)
    {
        SixBitArray<ConvertibleInt32> rowPositions = new(20);
        SixBitArray<Piece> rowValues = new(20);
        for (int i = 0; i < 8; i++)
        {
            rowPositions[i] = (int)value & 0x7;
            rowValues[i] = new() { Value = (byte)((i << 2) | ((byte)value >> 3 & 0x3)) };
            value >>= 6;
        }
        for (int i = 8; i < 20; i++)
        {
            rowPositions[i] = (int)value & 0x1f;
            rowValues[i] = new() { Value = (byte)((i << 2) | ((byte)value >> 5 & 0x1)) };
            value >>= 6;
        }
        return new() { Matrix = new(rowPositions, rowValues) };
    }
}
