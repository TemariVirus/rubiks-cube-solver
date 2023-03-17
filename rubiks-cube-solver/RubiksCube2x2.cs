using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

readonly struct RubiksCube2x2 : IRubiksCube<RubiksCube2x2>, IEquatable<RubiksCube2x2>
{
    #region // Transformations
    static PermutationMatrix<IPiece> None =>
        new(
            new int[] { Piece.WBR, Piece.WRG, Piece.WGO, Piece.WOB, Piece.YBO, Piece.YRB, Piece.YGR, Piece.YOG },
            new IPiece[]
            {
                CornerPiece.WBR,
                CornerPiece.WRG,
                CornerPiece.WGO,
                CornerPiece.WOB,
                CornerPiece.YBO,
                CornerPiece.YRB,
                CornerPiece.YGR,
                CornerPiece.YOG,
            }
        );
    static PermutationMatrix<IPiece> R =>
        new(
            new int[] { Piece.WOB, Piece.WRG, Piece.WGO, Piece.YBO, Piece.YRB, Piece.WBR, Piece.YGR, Piece.YOG },
            new IPiece[]
            {
                CornerPiece.WBR.Rotate(2),
                CornerPiece.WRG,
                CornerPiece.WGO,
                CornerPiece.WOB.Rotate(1),
                CornerPiece.YBO.Rotate(2),
                CornerPiece.YRB.Rotate(1),
                CornerPiece.YGR,
                CornerPiece.YOG,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube2x2>.R => R;
    static PermutationMatrix<IPiece> L =>
        new(
            new int[] { Piece.WBR, Piece.YGR, Piece.WRG, Piece.WOB, Piece.YBO, Piece.YRB, Piece.YOG, Piece.WGO },
            new IPiece[]
            {
                CornerPiece.WBR,
                CornerPiece.WRG.Rotate(1),
                CornerPiece.WGO.Rotate(2),
                CornerPiece.WOB,
                CornerPiece.YBO,
                CornerPiece.YRB,
                CornerPiece.YGR.Rotate(2),
                CornerPiece.YOG.Rotate(1),
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube2x2>.L => L;
    static PermutationMatrix<IPiece> U =>
        new(
            new int[] { Piece.WRG, Piece.WGO, Piece.WOB, Piece.WBR, Piece.YBO, Piece.YRB, Piece.YGR, Piece.YOG },
            new IPiece[]
            {
                CornerPiece.WBR.Rotate(0),
                CornerPiece.WRG.Rotate(0),
                CornerPiece.WGO.Rotate(0),
                CornerPiece.WOB.Rotate(0),
                CornerPiece.YBO,
                CornerPiece.YRB,
                CornerPiece.YGR,
                CornerPiece.YOG,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube2x2>.U => U;
    static PermutationMatrix<IPiece> D =>
        new(
            new int[] { Piece.WBR, Piece.WRG, Piece.WGO, Piece.WOB, Piece.YOG, Piece.YBO, Piece.YRB, Piece.YGR },
            new IPiece[]
            {
                CornerPiece.WBR,
                CornerPiece.WRG,
                CornerPiece.WGO,
                CornerPiece.WOB,
                CornerPiece.YBO,
                CornerPiece.YRB,
                CornerPiece.YGR,
                CornerPiece.YOG,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube2x2>.D => D;
    static PermutationMatrix<IPiece> F =>
        new(
            new int[] { Piece.YRB, Piece.WBR, Piece.WGO, Piece.WOB, Piece.YBO, Piece.YGR, Piece.WRG, Piece.YOG },
            new IPiece[]
            {
                CornerPiece.WBR.Rotate(1),
                CornerPiece.WRG.Rotate(2),
                CornerPiece.WGO,
                CornerPiece.WOB,
                CornerPiece.YBO,
                CornerPiece.YRB.Rotate(2),
                CornerPiece.YGR.Rotate(1),
                CornerPiece.YOG,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube2x2>.F => F;
    static PermutationMatrix<IPiece> B =>
        new(
            new int[] { Piece.WBR, Piece.WRG, Piece.YOG, Piece.WGO, Piece.WOB, Piece.YRB, Piece.YGR, Piece.YBO },
            new IPiece[]
            {
                CornerPiece.WBR,
                CornerPiece.WRG,
                CornerPiece.WGO.Rotate(1),
                CornerPiece.WOB.Rotate(2),
                CornerPiece.YBO.Rotate(1),
                CornerPiece.YRB,
                CornerPiece.YGR,
                CornerPiece.YOG.Rotate(2),
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube2x2>.B => B;
    public static readonly PermutationMatrix<IPiece> X = R * L.Pow(3);
    public static readonly PermutationMatrix<IPiece> Y = U * D.Pow(3);
    public static readonly PermutationMatrix<IPiece> Z = F * B.Pow(3);
    public static readonly PermutationMatrix<IPiece> TopRightFixedClockwise = X * Y;
    public static readonly PermutationMatrix<IPiece> TopRightFixedAntiClockwise = Z.Pow(3) * Y.Pow(3);
    #endregion

    public static readonly PermutationMatrix<IPiece>[] UnrotationsMap =
    {
        None,
        TopRightFixedClockwise,
        TopRightFixedAntiClockwise,
        Y.Pow(3),
        Y.Pow(3) * TopRightFixedClockwise,
        Y.Pow(3) * TopRightFixedAntiClockwise,
        Y.Pow(2),
        Y.Pow(2) * TopRightFixedClockwise,
        Y.Pow(2) * TopRightFixedAntiClockwise,
        Y,
        Y * TopRightFixedClockwise,
        Y * TopRightFixedAntiClockwise,
        X.Pow(2),
        X.Pow(2) * TopRightFixedClockwise,
        X.Pow(2) * TopRightFixedAntiClockwise,
        X.Pow(2) * Y,
        X.Pow(2) * Y * TopRightFixedClockwise,
        X.Pow(2) * Y * TopRightFixedAntiClockwise,
        X.Pow(2) * Y.Pow(2),
        X.Pow(2) * Y.Pow(2) * TopRightFixedClockwise,
        X.Pow(2) * Y.Pow(2) * TopRightFixedAntiClockwise,
        X.Pow(2) * Y.Pow(3),
        X.Pow(2) * Y.Pow(3) * TopRightFixedClockwise,
        X.Pow(2) * Y.Pow(3) * TopRightFixedAntiClockwise,
    };
    public static readonly Dictionary<FaceRotation, FaceRotation>[] RotationsMap =
    {
        // None
        new Dictionary<FaceRotation, FaceRotation>()
        {
            { FaceRotation.R, FaceRotation.R },
            { FaceRotation.L, FaceRotation.L },
            { FaceRotation.U, FaceRotation.U },
            { FaceRotation.D, FaceRotation.D },
            { FaceRotation.F, FaceRotation.F },
            { FaceRotation.B, FaceRotation.B },
        },
        // TopRightFixedAntiClockwise
        new Dictionary<FaceRotation, FaceRotation>()
        {
            { FaceRotation.R, FaceRotation.R },
            { FaceRotation.L, FaceRotation.L },
            { FaceRotation.U, FaceRotation.U },
            { FaceRotation.D, FaceRotation.D },
            { FaceRotation.F, FaceRotation.F },
            { FaceRotation.B, FaceRotation.B },
        },
        TopRightFixedClockwise,
        Y,
        Y * TopRightFixedAntiClockwise,
        Y * TopRightFixedClockwise,
        Y.Pow(2),
        Y.Pow(2) * TopRightFixedAntiClockwise,
        Y.Pow(2) * TopRightFixedClockwise,
        Y,
        Y.Pow(3) * TopRightFixedAntiClockwise,
        Y.Pow(3) * TopRightFixedClockwise,
        X.Pow(2),
        X.Pow(2) * TopRightFixedAntiClockwise,
        X.Pow(2) * TopRightFixedClockwise,
        X.Pow(2) * Y,
        X.Pow(2) * Y * TopRightFixedAntiClockwise,
        X.Pow(2) * Y * TopRightFixedClockwise,
        X.Pow(2) * Y.Pow(2),
        X.Pow(2) * Y.Pow(2) * TopRightFixedAntiClockwise,
        X.Pow(2) * Y.Pow(2) * TopRightFixedClockwise,
        X.Pow(2) * Y.Pow(3),
        X.Pow(2) * Y.Pow(3) * TopRightFixedAntiClockwise,
        X.Pow(2) * Y.Pow(3) * TopRightFixedClockwise,
    };
    public static RubiksCube2x2 Solved => new() { Matrix = None };

    public PermutationMatrix<IPiece> Matrix { get; init; }

    public RubiksCube2x2 ApplyTransformation(PermutationMatrix<IPiece> other) => new() { Matrix = Matrix * other };

    public RubiksCube2x2 RotateToFixed() => ApplyTransformation(UnrotationsMap[Matrix.RowPositions[0] * 3 + Matrix.RowValues[0].Parity]);

    public FaceRotation RotationFromFixed(FaceRotation rotation)
    {

    }

    public void DrawCube(int xOffset, int yOffset)
    {
        // Green face (left)
        ConsoleHelper.WriteAt("[]", 0 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.YOG)[2]);
        ConsoleHelper.WriteAt("[]", 2 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WGO)[1]);
        ConsoleHelper.WriteAt("[]", 0 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YGR)[1]);
        ConsoleHelper.WriteAt("[]", 2 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WRG)[2]);
        // White face (middle left)
        ConsoleHelper.WriteAt("[]", 4 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WGO)[0]);
        ConsoleHelper.WriteAt("[]", 6 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WOB)[0]);
        ConsoleHelper.WriteAt("[]", 4 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WRG)[0]);
        ConsoleHelper.WriteAt("[]", 6 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WBR)[0]);
        // Orange face (top)
        ConsoleHelper.WriteAt("[]", 4 + xOffset, 0 + yOffset, Matrix.GetColumnValue(Piece.YOG)[1]);
        ConsoleHelper.WriteAt("[]", 6 + xOffset, 0 + yOffset, Matrix.GetColumnValue(Piece.YBO)[2]);
        ConsoleHelper.WriteAt("[]", 4 + xOffset, 1 + yOffset, Matrix.GetColumnValue(Piece.WGO)[2]);
        ConsoleHelper.WriteAt("[]", 6 + xOffset, 1 + yOffset, Matrix.GetColumnValue(Piece.WOB)[1]);
        // Red face (bottom)
        ConsoleHelper.WriteAt("[]", 4 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.WRG)[1]);
        ConsoleHelper.WriteAt("[]", 6 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.WBR)[2]);
        ConsoleHelper.WriteAt("[]", 4 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YGR)[2]);
        ConsoleHelper.WriteAt("[]", 6 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YRB)[1]);
        // Blue face (middle right)
        ConsoleHelper.WriteAt("[]", 8 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WOB)[2]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.YBO)[1]);
        ConsoleHelper.WriteAt("[]", 8 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WBR)[1]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YRB)[2]);
        // Yellow face (right)
        ConsoleHelper.WriteAt("[]", 12 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.YBO)[0]);
        ConsoleHelper.WriteAt("[]", 14 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.YOG)[0]);
        ConsoleHelper.WriteAt("[]", 12 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YRB)[0]);
        ConsoleHelper.WriteAt("[]", 14 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YGR)[0]);
    }

    public override int GetHashCode() => ((long)this).GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is RubiksCube2x2 cube && this == cube;

    public bool Equals(RubiksCube2x2 other) => this == other;

    public static bool operator ==(RubiksCube2x2 left, RubiksCube2x2 right) => (long)left == (long)right;
    public static bool operator !=(RubiksCube2x2 left, RubiksCube2x2 right) => (long)left != (long)right;
    public static explicit operator long(RubiksCube2x2 cube)
    {
        PermutationMatrix<IPiece> matrix = cube.RotateToFixed().Matrix;
        //PermutationMatrix<IPiece> matrix = cube.Matrix;
        long ans = 0;
        for (int i = 0; i < matrix.Size; i++)
            ans |=
                (long)(matrix.GetRowValue(i).Parity << 3 | matrix.RowPositions[i]) << (i * 5);
        return ans;
    }
}
