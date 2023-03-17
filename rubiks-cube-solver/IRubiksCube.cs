using System.Collections.Concurrent;

namespace RubiksCubeSolver;

interface IRubiksCube<TSelf>
    where TSelf : IRubiksCube<TSelf>, IEquatable<TSelf>
{
    public static abstract TSelf Solved { get; }
    static abstract PermutationMatrix None { get; }
    static abstract PermutationMatrix R { get; }
    static abstract PermutationMatrix L { get; }
    static abstract PermutationMatrix U { get; }
    static abstract PermutationMatrix D { get; }
    static abstract PermutationMatrix F { get; }
    static abstract PermutationMatrix B { get; }
    static PermutationMatrix X => TSelf.R * TSelf.L.Pow(3);
    static PermutationMatrix Y => TSelf.U * TSelf.D.Pow(3);
    static PermutationMatrix Z => TSelf.F * TSelf.B.Pow(3);

    static readonly Dictionary<FaceRotation, PermutationMatrix> RotationMap = Enumerable
        .Repeat(
            new PermutationMatrix[] { TSelf.R, TSelf.L, TSelf.U, TSelf.D, TSelf.F, TSelf.B },
            3
        )
        .SelectMany(x => x)
        .Select((matrix, i) => ((FaceRotation)(i + 1), matrix.Pow((i / 6) + 1)))
        .ToDictionary(x => x.Item1, x => x.Item2);
    static readonly PermutationMatrix[] CubeOrientations = Enumerable
        .Repeat(new[] { TSelf.None, X, X.Pow(2), X.Pow(3), Z, Z.Pow(3) }, 4)
        .SelectMany(x => x)
        .Select((o, i) => o * Y.Pow(i / 6))
        .ToArray();

    public PermutationMatrix Matrix { get; init; }

    public TSelf ApplyTransformation(PermutationMatrix other);

    public static abstract bool operator ==(TSelf left, TSelf right);

    public static abstract bool operator !=(TSelf left, TSelf right);
}

static class RubiksCubeExtensions
{
    public static T MakeRotation<T>(this T cube, FaceRotation rotation)
        where T : IRubiksCube<T>, IEquatable<T> =>
        cube.ApplyTransformation(IRubiksCube<T>.RotationMap[rotation]);

    public static FaceRotation[] Solve<T>(this T scrambled)
        where T : IRubiksCube<T>, IEquatable<T>
    {
        if (scrambled == T.Solved)
            return Array.Empty<FaceRotation>();

        bool isFromScrambled = true;
        HashSet<T> prevMap = new();
        // Meet-in-the-middle approach to reduce time of search by a quadratic factor
        Dictionary<T, LinkedListView<FaceRotation>> map = new() { { scrambled, new() } };
        Dictionary<T, LinkedListView<FaceRotation>> map2 = new() { { T.Solved, new() } };

        FaceRotation[] solution = Array.Empty<FaceRotation>();
        long count = 0;

        while (true)
        {
            ConcurrentDictionary<T, LinkedListView<FaceRotation>> newMap = new();
            foreach (var (cube, moves) in map)
            {
                // Try every possible rotation for each cube state
                foreach (FaceRotation fr in Enum.GetValues(typeof(FaceRotation)))
                {
                    T rotated = cube.MakeRotation(fr);

                    if (prevMap.Contains(rotated) || newMap.ContainsKey(rotated))
                        continue;

                    if (map2.TryGetValue(rotated, out LinkedListView<FaceRotation>? moves2))
                    {
                        // For moves made from solved state, reverse their order and direction
                        var firstHalf = isFromScrambled ? moves.Append(fr) : moves2!;
                        var secondHalf = (isFromScrambled ? moves2 : moves.Append(fr))
                            .Reverse()
                            .Select(ReverseRotation);
                        foreach (var move in firstHalf)
                            scrambled = scrambled.MakeRotation(move);

                        foreach (var o in IRubiksCube<T>.CubeOrientations)
                        {
                            T test = scrambled.ApplyTransformation(o);
                            foreach (var move in secondHalf)
                                test = test.MakeRotation(move);
                            if (test == T.Solved)
                                return firstHalf
                                    .Concat(secondHalf
                                        .Select(m => TransformRotation(m, o))
                                     )
                                    .ToArray();
                        }
                    }

                    newMap.TryAdd(rotated, moves.Append(fr));
                    count++;
                }
                if (solution.Length > 0)
                    break;
            }
            ConsoleHelper.WriteAt($"Searched: {count}", 1, 1);
            if (solution.Length > 0)
                return solution;

            prevMap = map.Keys.ToHashSet();
            map = newMap.ToDictionary(x => x.Key, x => x.Value);
            isFromScrambled = !isFromScrambled;
            // Swap to search 1 level deeper from the other side
            (map, map2) = (map2, map);
        }

        static FaceRotation ReverseRotation(FaceRotation fr)
        {
            int modifier = (int)(fr - 1) / 6;
            modifier = 1 - modifier;
            return (FaceRotation)(12 * modifier + (int)fr);
        }

        static FaceRotation TransformRotation(FaceRotation rotation, PermutationMatrix orientation)
        {
            // Jankiest code I've ever written
            FaceRotation[][] rotationMaps =
            {
                new FaceRotation[] { FaceRotation.R, FaceRotation.F, FaceRotation.D },
                new FaceRotation[] { FaceRotation.L, FaceRotation.B, FaceRotation.U },
                new FaceRotation[] { FaceRotation.B, FaceRotation.U, FaceRotation.R },
                new FaceRotation[] { FaceRotation.F, FaceRotation.D, FaceRotation.L },
                new FaceRotation[] { FaceRotation.U, FaceRotation.L, FaceRotation.F },
                new FaceRotation[] { FaceRotation.D, FaceRotation.R, FaceRotation.B },
            };

            int[] facing = orientation
                .Inverse()
                .Decompose(new PermutationMatrix[] { IRubiksCube<T>.X, IRubiksCube<T>.Y, IRubiksCube<T>.Z })
                .Select(FactorToInt)
                .ToArray();
            int modifier = (int)(rotation - 1) / 6;
            modifier *= 6;
            rotation = (FaceRotation)((int)rotation - modifier);
            foreach (var f in facing)
                rotation = rotationMaps[(int)rotation - 1][f];
            rotation = (FaceRotation)((int)rotation + modifier);

            return rotation;

            static int FactorToInt(PermutationMatrix factor) =>
                factor.RowPositions[0] switch
                {
                    3 => 0,
                    1 => 1,
                    5 => 2,
                    _ => throw new Exception($"The code says 'F*ck you' ({nameof(PermutationMatrix)}.{nameof(PermutationMatrix.Decompose)} probably broke).")
                };
        }
    }
}
