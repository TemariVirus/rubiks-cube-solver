using System.Collections.Concurrent;

namespace RubiksCubeSolver;

interface IRubiksCube<TSelf>
    where TSelf : IRubiksCube<TSelf>, IEquatable<TSelf>
{
    public static abstract TSelf Solved { get; }
    static abstract PermutationMatrix<IPiece> R { get; }
    static abstract PermutationMatrix<IPiece> L { get; }
    static abstract PermutationMatrix<IPiece> U { get; }
    static abstract PermutationMatrix<IPiece> D { get; }
    static abstract PermutationMatrix<IPiece> F { get; }
    static abstract PermutationMatrix<IPiece> B { get; }

    static readonly Dictionary<FaceRotation, PermutationMatrix<IPiece>> RotationMap =
        Enumerable
            .Repeat(new PermutationMatrix<IPiece>[] { TSelf.R, TSelf.L, TSelf.U, TSelf.D, TSelf.F, TSelf.B }, 3)
            .SelectMany(x => x)
            .Select((matrix, i) => ((FaceRotation)(i + 1), matrix.Pow((i / 6) + 1)))
            .ToDictionary(x => x.Item1, x => x.Item2);
 
    public PermutationMatrix<IPiece> Matrix { get; init; }

    public TSelf ApplyTransformation(PermutationMatrix<IPiece> other);

    public FaceRotation RotationFromFixed(FaceRotation rotation);

    public void DrawCube(int xOffset, int yOffset);
    
    public static abstract bool operator ==(TSelf left, TSelf right);

    public static abstract bool operator !=(TSelf left, TSelf right);
}

static class RubiksCubeExtensions
{
    public static T MakeRotation<T>(this T cube, FaceRotation rotation)
        where T : IRubiksCube<T>, IEquatable<T>, new() =>
        cube.ApplyTransformation(IRubiksCube<T>.RotationMap[rotation]);

    public static FaceRotation[] Solve<T>(this T scrambled)
        where T : IRubiksCube<T>, IEquatable<T>, new()
    {
        if (scrambled == T.Solved)
            return Array.Empty<FaceRotation>();

        bool isFromScrambled = true;
        HashSet<T> prevMap = new();
        // Meet-in-the-middle approach to reduce time of search by a quadratic factor
        Dictionary<T, LinkedListView<FaceRotation>> map = new() { { scrambled, new() } };
        Dictionary<T, LinkedListView<FaceRotation>> map2 = new() { { T.Solved.MakeSameRotationOffsetAs(scrambled), new() } };

        FaceRotation[] solution = Array.Empty<FaceRotation>();
        long count = 0;
        Thread thread = new(async () =>
        {
            PeriodicTimer timer = new(TimeSpan.FromMilliseconds(200));
            while (await timer.WaitForNextTickAsync())
            {
                ConsoleHelper.WriteAt($"Searched: {count}", 1, 1);
                if (solution.Length > 0) break;
            }
        });
        thread.Start();
        while (true)
        {
            ConcurrentDictionary<T, LinkedListView<FaceRotation>> newMap = new();
            Parallel.ForEach(map, (x, loopState) =>
            {
                var (cube, moves) = x;
                // Try every possible rotation for each cube state
                foreach (FaceRotation fr in Enum.GetValues(typeof(FaceRotation)))
                {
                    T rotated = cube.MakeRotation(fr);

                    if (prevMap.Contains(rotated) || newMap.ContainsKey(rotated))
                        continue;

                    count++;
                    if (map2.TryGetValue(rotated, out LinkedListView<FaceRotation>? moves2))
                    {
                        // For moves made from solved state, reverse their order and direction
                        solution = isFromScrambled
                            ? moves.Append(fr).Append(moves2.Reverse().Select(ReverseRotation)).ToArray()
                            : moves2.Append(moves.Append(fr).Reverse().Select(ReverseRotation)).ToArray();
                        loopState.Stop();
                    }

                    newMap.TryAdd(rotated, moves.Append(fr));
                }
            });
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
            modifier = (2 - modifier) * 6;
            int rotation = ((int)(fr - 1) % 6) + 1;
            return (FaceRotation)(modifier + rotation);
        }
    }
}
