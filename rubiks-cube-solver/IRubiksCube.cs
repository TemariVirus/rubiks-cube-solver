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

    static readonly Dictionary<FaceRotation, PermutationMatrix> RotationMap = Enumerable
        .Repeat(new PermutationMatrix[] { TSelf.R, TSelf.L, TSelf.U, TSelf.D, TSelf.F, TSelf.B }, 3)
        .SelectMany(x => x)
        .Select((matrix, i) => ((FaceRotation)((i / 6 + 1) * 8 + (i % 6)), matrix.Pow(i / 6 + 1)))
        .ToDictionary(x => x.Item1, x => x.Item2);

    public TSelf ApplyTransformation(PermutationMatrix other);

    public FaceRotation[] Solve(out int count);
}

static class RubiksCubeExtensions
{
    public static T MakeRotation<T>(this T cube, FaceRotation rotation)
        where T : IRubiksCube<T>, IEquatable<T> =>
        cube.ApplyTransformation(IRubiksCube<T>.RotationMap[rotation]);
}
