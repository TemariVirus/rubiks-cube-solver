using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

readonly struct RubiksCube2x2 : IRubiksCube<RubiksCube2x2>, IEquatable<RubiksCube2x2>
{
    #region // Transformations
    static PermutationMatrix None =>
        new(
            new byte[]
            {
                (byte)Piece.WBR.Id,
                (byte)Piece.WRG.Id,
                (byte)Piece.WGO.Id,
                (byte)Piece.WOB.Id,
                (byte)Piece.YBO.Id,
                (byte)Piece.YRB.Id,
                (byte)Piece.YGR.Id,
                (byte)Piece.YOG.Id
            },
            new Piece[]
            {
                Piece.WBR,
                Piece.WRG,
                Piece.WGO,
                Piece.WOB,
                Piece.YBO,
                Piece.YRB,
                Piece.YGR,
                Piece.YOG,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.None => None;
    static PermutationMatrix R =>
        new(
            new byte[]
            {
                (byte)Piece.WOB.Id,
                (byte)Piece.WRG.Id,
                (byte)Piece.WGO.Id,
                (byte)Piece.YBO.Id,
                (byte)Piece.YRB.Id,
                (byte)Piece.WBR.Id,
                (byte)Piece.YGR.Id,
                (byte)Piece.YOG.Id
            },
            new Piece[]
            {
                Piece.WBR.Rotate(2),
                Piece.WRG,
                Piece.WGO,
                Piece.WOB.Rotate(1),
                Piece.YBO.Rotate(2),
                Piece.YRB.Rotate(1),
                Piece.YGR,
                Piece.YOG,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.R => R;
    static PermutationMatrix L =>
        new(
            new byte[]
            {
                (byte)Piece.WBR.Id,
                (byte)Piece.YGR.Id,
                (byte)Piece.WRG.Id,
                (byte)Piece.WOB.Id,
                (byte)Piece.YBO.Id,
                (byte)Piece.YRB.Id,
                (byte)Piece.YOG.Id,
                (byte)Piece.WGO.Id
            },
            new Piece[]
            {
                Piece.WBR,
                Piece.WRG.Rotate(1),
                Piece.WGO.Rotate(2),
                Piece.WOB,
                Piece.YBO,
                Piece.YRB,
                Piece.YGR.Rotate(2),
                Piece.YOG.Rotate(1),
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.L => L;
    static PermutationMatrix U =>
        new(
            new byte[]
            {
                (byte)Piece.WRG.Id,
                (byte)Piece.WGO.Id,
                (byte)Piece.WOB.Id,
                (byte)Piece.WBR.Id,
                (byte)Piece.YBO.Id,
                (byte)Piece.YRB.Id,
                (byte)Piece.YGR.Id,
                (byte)Piece.YOG.Id
            },
            new Piece[]
            {
                Piece.WBR.Rotate(0),
                Piece.WRG.Rotate(0),
                Piece.WGO.Rotate(0),
                Piece.WOB.Rotate(0),
                Piece.YBO,
                Piece.YRB,
                Piece.YGR,
                Piece.YOG,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.U => U;
    static PermutationMatrix D =>
        new(
            new byte[]
            {
                (byte)Piece.WBR.Id,
                (byte)Piece.WRG.Id,
                (byte)Piece.WGO.Id,
                (byte)Piece.WOB.Id,
                (byte)Piece.YOG.Id,
                (byte)Piece.YBO.Id,
                (byte)Piece.YRB.Id,
                (byte)Piece.YGR.Id
            },
            new Piece[]
            {
                Piece.WBR,
                Piece.WRG,
                Piece.WGO,
                Piece.WOB,
                Piece.YBO,
                Piece.YRB,
                Piece.YGR,
                Piece.YOG,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.D => D;
    static PermutationMatrix F =>
        new(
            new byte[]
            {
                (byte)Piece.YRB.Id,
                (byte)Piece.WBR.Id,
                (byte)Piece.WGO.Id,
                (byte)Piece.WOB.Id,
                (byte)Piece.YBO.Id,
                (byte)Piece.YGR.Id,
                (byte)Piece.WRG.Id,
                (byte)Piece.YOG.Id
            },
            new Piece[]
            {
                Piece.WBR.Rotate(1),
                Piece.WRG.Rotate(2),
                Piece.WGO,
                Piece.WOB,
                Piece.YBO,
                Piece.YRB.Rotate(2),
                Piece.YGR.Rotate(1),
                Piece.YOG,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.F => F;
    static PermutationMatrix B =>
        new(
            new byte[]
            {
                (byte)Piece.WBR.Id,
                (byte)Piece.WRG.Id,
                (byte)Piece.YOG.Id,
                (byte)Piece.WGO.Id,
                (byte)Piece.WOB.Id,
                (byte)Piece.YRB.Id,
                (byte)Piece.YGR.Id,
                (byte)Piece.YBO.Id
            },
            new Piece[]
            {
                Piece.WBR,
                Piece.WRG,
                Piece.WGO.Rotate(1),
                Piece.WOB.Rotate(2),
                Piece.YBO.Rotate(1),
                Piece.YRB,
                Piece.YGR,
                Piece.YOG.Rotate(2),
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube2x2>.B => B;
    public static readonly PermutationMatrix X = R * L.Pow(3);
    public static readonly PermutationMatrix Y = U * D.Pow(3);
    public static readonly PermutationMatrix Z = F * B.Pow(3);
    public static readonly PermutationMatrix TopRightFixedClockwise = X * Y;
    public static readonly PermutationMatrix TopRightFixedAntiClockwise = Z.Pow(3) * Y.Pow(3);
    #endregion
    public static readonly PermutationMatrix[] UnrotationsMap =
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
    public static RubiksCube2x2 Solved => new() { Matrix = None };
    public PermutationMatrix Matrix { get; init; }

    public RubiksCube2x2 ApplyTransformation(PermutationMatrix other) =>
        new() { Matrix = Matrix * other };

    public RubiksCube2x2 RotateToFixed() =>
        ApplyTransformation(
            UnrotationsMap[Matrix.RowPositions[0] * 3 + Matrix.RowValues[0].Parity]
        );

    public void DrawCube(int xOffset, int yOffset)
    {
        // Green face (left)
        ConsoleHelper.WriteAt(
            "[]",
            0 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.YOG.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            2 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WGO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            0 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YGR.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            2 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WRG.Id)[2]
        );
        // White face (middle left)
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WGO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WOB.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WRG.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WBR.Id)[0]
        );
        // Orange face (top)
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            0 + yOffset,
            Matrix.GetColumnValue(Piece.YOG.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            0 + yOffset,
            Matrix.GetColumnValue(Piece.YBO.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            1 + yOffset,
            Matrix.GetColumnValue(Piece.WGO.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            1 + yOffset,
            Matrix.GetColumnValue(Piece.WOB.Id)[1]
        );
        // Red face (bottom)
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.WRG.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            4 + yOffset,
            Matrix.GetColumnValue(Piece.WBR.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            4 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YGR.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            6 + xOffset,
            5 + yOffset,
            Matrix.GetColumnValue(Piece.YRB.Id)[1]
        );
        // Blue face (middle right)
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.WOB.Id)[2]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.YBO.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            8 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.WBR.Id)[1]
        );
        ConsoleHelper.WriteAt(
            "[]",
            10 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YRB.Id)[2]
        );
        // Yellow face (right)
        ConsoleHelper.WriteAt(
            "[]",
            12 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.YBO.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            14 + xOffset,
            2 + yOffset,
            Matrix.GetColumnValue(Piece.YOG.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            12 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YRB.Id)[0]
        );
        ConsoleHelper.WriteAt(
            "[]",
            14 + xOffset,
            3 + yOffset,
            Matrix.GetColumnValue(Piece.YGR.Id)[0]
        );
    }

    public override int GetHashCode() => ((long)this).GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is RubiksCube2x2 cube && this == cube;

    public bool Equals(RubiksCube2x2 other) => this == other;

    public static bool operator ==(RubiksCube2x2 left, RubiksCube2x2 right) =>
        (long)left == (long)right;

    public static bool operator !=(RubiksCube2x2 left, RubiksCube2x2 right) =>
        (long)left != (long)right;

    public static explicit operator long(RubiksCube2x2 cube)
    {
        PermutationMatrix matrix = cube.RotateToFixed().Matrix;
        long ans = 0;
        for (int i = 0; i < matrix.Size; i++)
            ans |= (long)(matrix.GetRowValue(i).Parity << 3 | matrix.RowPositions[i]) << (i * 5);
        return ans;
    }
}
