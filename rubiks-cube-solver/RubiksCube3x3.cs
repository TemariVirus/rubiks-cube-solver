using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

readonly struct RubiksCube3x3 : IRubiksCube<RubiksCube3x3>, IEquatable<RubiksCube3x3>
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
                (byte)Piece.YOG.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.BO.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.YG.Id,
                (byte)Piece.YO.Id
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
                Piece.YO,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.None => None;
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.R =>
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
                (byte)Piece.YOG.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.BO.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.YG.Id,
                (byte)Piece.YO.Id
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
                Piece.YO,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.L =>
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
                (byte)Piece.WGO.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.YG.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.BO.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.YO.Id
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
                Piece.YO,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.U =>
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
                (byte)Piece.YOG.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.BO.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.YG.Id,
                (byte)Piece.YO.Id
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
                Piece.YO,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.D =>
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
                (byte)Piece.YGR.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.BO.Id,
                (byte)Piece.YO.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.YG.Id
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
                Piece.YO.Rotate(0),
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.F =>
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
                (byte)Piece.YOG.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.BO.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.YG.Id,
                (byte)Piece.YO.Id
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
                Piece.YO,
            }
        );
    static PermutationMatrix IRubiksCube<RubiksCube3x3>.B =>
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
                (byte)Piece.YBO.Id,
                (byte)Piece.WR.Id,
                (byte)Piece.WG.Id,
                (byte)Piece.OG.Id,
                (byte)Piece.WB.Id,
                (byte)Piece.RB.Id,
                (byte)Piece.GR.Id,
                (byte)Piece.YO.Id,
                (byte)Piece.WO.Id,
                (byte)Piece.YB.Id,
                (byte)Piece.YR.Id,
                (byte)Piece.YG.Id,
                (byte)Piece.BO.Id
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
                Piece.YO.Rotate(0),
            }
        );
    #endregion
    public static RubiksCube3x3 Solved => new() { Matrix = None };
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

    public override int GetHashCode() => ((Int128)this).GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is RubiksCube3x3 cube && this == cube;

    public bool Equals(RubiksCube3x3 other) => this == other;

    public static bool operator ==(RubiksCube3x3 left, RubiksCube3x3 right) =>
        (Int128)left == (Int128)right;

    public static bool operator !=(RubiksCube3x3 left, RubiksCube3x3 right) =>
        (Int128)left != (Int128)right;

    public static explicit operator Int128(RubiksCube3x3 cube)
    {
        Int128 ans = 0;
        for (int i = 0; i < 8; i++)
            ans |=
                (Int128)(cube.Matrix.GetRowValue(i).Parity << 3 | cube.Matrix.RowPositions[i])
                << (i * 6);
        for (int i = 9; i < 20; i++)
            ans |=
                (Int128)(cube.Matrix.GetRowValue(i).Parity << 5 | cube.Matrix.RowPositions[i])
                << (i * 6);
        return ans;
    }
}
