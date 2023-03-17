using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

readonly struct RubiksCube3x3 : IRubiksCube<RubiksCube3x3>, IEquatable<RubiksCube3x3>
{
    #region // Transformations
    static PermutationMatrix<IPiece> None =>
        new(
            new int[] { Piece.WBR, Piece.WRG, Piece.WGO, Piece.WOB, Piece.YBO, Piece.YRB, Piece.YGR, Piece.YOG,
            Piece.WR, Piece.WG, Piece.WO, Piece.WB, Piece.RB, Piece.GR, Piece.OG, Piece.BO, Piece.YB, Piece.YR, Piece.YG, Piece.YO },
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
                EdgePiece.WR,
                EdgePiece.WG,
                EdgePiece.WO,
                EdgePiece.WB,
                EdgePiece.RB,
                EdgePiece.GR,
                EdgePiece.OG,
                EdgePiece.BO,
                EdgePiece.YB,
                EdgePiece.YR,
                EdgePiece.YG,
                EdgePiece.YO,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube3x3>.R =>
        new(
            new int[] { Piece.WOB, Piece.WRG, Piece.WGO, Piece.YBO, Piece.YRB, Piece.WBR, Piece.YGR, Piece.YOG,
            Piece.WR, Piece.WG, Piece.WO, Piece.BO, Piece.WB, Piece.GR, Piece.OG, Piece.YB, Piece.RB, Piece.YR, Piece.YG, Piece.YO },
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
                EdgePiece.WR,
                EdgePiece.WG,
                EdgePiece.WO,
                EdgePiece.WB.Rotate(1),
                EdgePiece.RB.Rotate(0),
                EdgePiece.GR,
                EdgePiece.OG,
                EdgePiece.BO.Rotate(1),
                EdgePiece.YB.Rotate(0),
                EdgePiece.YR,
                EdgePiece.YG,
                EdgePiece.YO,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube3x3>.L =>
        new(
            new int[] { Piece.WBR, Piece.YGR, Piece.WRG, Piece.WOB, Piece.YBO, Piece.YRB, Piece.YOG, Piece.WGO,
            Piece.WR, Piece.GR, Piece.WO, Piece.WB, Piece.RB, Piece.YG, Piece.WG, Piece.BO, Piece.YB, Piece.YR, Piece.OG, Piece.YO },
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
                EdgePiece.WR,
                EdgePiece.WG.Rotate(1),
                EdgePiece.WO,
                EdgePiece.WB,
                EdgePiece.RB,
                EdgePiece.GR.Rotate(1),
                EdgePiece.OG.Rotate(0),
                EdgePiece.BO,
                EdgePiece.YB,
                EdgePiece.YR,
                EdgePiece.YG.Rotate(0),
                EdgePiece.YO,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube3x3>.U =>
        new(
            new int[] { Piece.WRG, Piece.WGO, Piece.WOB, Piece.WBR, Piece.YBO, Piece.YRB, Piece.YGR, Piece.YOG,
            Piece.WG, Piece.WO, Piece.WB, Piece.WR, Piece.RB, Piece.GR, Piece.OG, Piece.BO, Piece.YB, Piece.YR, Piece.YG, Piece.YO },
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
                EdgePiece.WR.Rotate(0),
                EdgePiece.WG.Rotate(0),
                EdgePiece.WO.Rotate(0),
                EdgePiece.WB.Rotate(0),
                EdgePiece.RB,
                EdgePiece.GR,
                EdgePiece.OG,
                EdgePiece.BO,
                EdgePiece.YB,
                EdgePiece.YR,
                EdgePiece.YG,
                EdgePiece.YO,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube3x3>.D =>
        new(
            new int[] { Piece.WBR, Piece.WRG, Piece.WGO, Piece.WOB, Piece.YOG, Piece.YBO, Piece.YRB, Piece.YGR,
            Piece.WR, Piece.WG, Piece.WO, Piece.WB, Piece.RB, Piece.GR, Piece.OG, Piece.BO, Piece.YO, Piece.YB, Piece.YR, Piece.YG },
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
                EdgePiece.WR,
                EdgePiece.WG,
                EdgePiece.WO,
                EdgePiece.WB,
                EdgePiece.RB,
                EdgePiece.GR,
                EdgePiece.OG,
                EdgePiece.BO,
                EdgePiece.YB.Rotate(0),
                EdgePiece.YR.Rotate(0),
                EdgePiece.YG.Rotate(0),
                EdgePiece.YO.Rotate(0),
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube3x3>.F =>
        new(
            new int[] { Piece.YRB, Piece.WBR, Piece.WGO, Piece.WOB, Piece.YBO, Piece.YGR, Piece.WRG, Piece.YOG,
            Piece.RB, Piece.WG, Piece.WO, Piece.WB, Piece.YR, Piece.WR, Piece.OG, Piece.BO, Piece.YB, Piece.GR, Piece.YG, Piece.YO },
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
                EdgePiece.WR.Rotate(1),
                EdgePiece.WG,
                EdgePiece.WO,
                EdgePiece.WB,
                EdgePiece.RB.Rotate(1),
                EdgePiece.GR.Rotate(0),
                EdgePiece.OG,
                EdgePiece.BO,
                EdgePiece.YB,
                EdgePiece.YR.Rotate(0),
                EdgePiece.YG,
                EdgePiece.YO,
            }
        );
    static PermutationMatrix<IPiece> IRubiksCube<RubiksCube3x3>.B =>
        new(
            new int[] { Piece.WBR, Piece.WRG, Piece.YOG, Piece.WGO, Piece.WOB, Piece.YRB, Piece.YGR, Piece.YBO,
            Piece.WR, Piece.WG, Piece.OG, Piece.WB, Piece.RB, Piece.GR, Piece.YO, Piece.WO, Piece.YB, Piece.YR, Piece.YG, Piece.BO },
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
                EdgePiece.WR,
                EdgePiece.WG,
                EdgePiece.WO.Rotate(1),
                EdgePiece.WB,
                EdgePiece.RB,
                EdgePiece.GR,
                EdgePiece.OG.Rotate(1),
                EdgePiece.BO.Rotate(0),
                EdgePiece.YB,
                EdgePiece.YR,
                EdgePiece.YG,
                EdgePiece.YO.Rotate(0),
            }
        );
    #endregion

    public static RubiksCube3x3 Solved => new() { Matrix = None };

    public PermutationMatrix<IPiece> Matrix { get; init; }
    
    public RubiksCube3x3 ApplyTransformation(PermutationMatrix<IPiece> other) => new() { Matrix = Matrix * other };

    public FaceRotation RotationFromFixed(FaceRotation rotation) => rotation;

    public void DrawCube(int xOffset, int yOffset)
    {
        // Green face (left)
        ConsoleHelper.WriteAt("[]", 0  + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YOG)[2]);
        ConsoleHelper.WriteAt("[]", 2  + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.OG)[1]);
        ConsoleHelper.WriteAt("[]", 4  + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WGO)[1]);
        ConsoleHelper.WriteAt("[]", 0  + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.YG)[1]);
        ConsoleHelper.WriteAt("[]", 2  + xOffset, 4 + yOffset, ConsoleColor.Green);
        ConsoleHelper.WriteAt("[]", 4  + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.WG)[1]);
        ConsoleHelper.WriteAt("[]", 0  + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YGR)[1]);
        ConsoleHelper.WriteAt("[]", 2  + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.GR)[0]);
        ConsoleHelper.WriteAt("[]", 4  + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.WRG)[2]);
        // White face (middle left)
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WGO)[0]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WO)[0]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WOB)[0]);
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.WG)[0]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 4 + yOffset, ConsoleColor.White);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.WB)[0]);
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.WRG)[0]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.WR)[0]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.WBR)[0]);
        // Orange face (top)
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 0 + yOffset, Matrix.GetColumnValue(Piece.YOG)[1]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 0 + yOffset, Matrix.GetColumnValue(Piece.YO)[1]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 0 + yOffset, Matrix.GetColumnValue(Piece.YBO)[2]);
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 1 + yOffset, Matrix.GetColumnValue(Piece.OG)[0]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 1 + yOffset, ConsoleColor.DarkYellow);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 1 + yOffset, Matrix.GetColumnValue(Piece.BO)[1]);
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WGO)[2]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WO)[1]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 2 + yOffset, Matrix.GetColumnValue(Piece.WOB)[1]);
        // Red face (bottom)
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 6 + yOffset, Matrix.GetColumnValue(Piece.WRG)[1]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 6 + yOffset, Matrix.GetColumnValue(Piece.WR)[1]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 6 + yOffset, Matrix.GetColumnValue(Piece.WBR)[2]);
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 7 + yOffset, Matrix.GetColumnValue(Piece.GR)[1]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 7 + yOffset, ConsoleColor.Red);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 7 + yOffset, Matrix.GetColumnValue(Piece.RB)[0]);
        ConsoleHelper.WriteAt("[]", 6  + xOffset, 8 + yOffset, Matrix.GetColumnValue(Piece.YGR)[2]);
        ConsoleHelper.WriteAt("[]", 8  + xOffset, 8 + yOffset, Matrix.GetColumnValue(Piece.YR)[1]);
        ConsoleHelper.WriteAt("[]", 10 + xOffset, 8 + yOffset, Matrix.GetColumnValue(Piece.YRB)[1]);
        // Blue face (middle right)
        ConsoleHelper.WriteAt("[]", 12 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.WOB)[2]);
        ConsoleHelper.WriteAt("[]", 14 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.BO)[0]);
        ConsoleHelper.WriteAt("[]", 16 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YBO)[1]);
        ConsoleHelper.WriteAt("[]", 12 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.WB)[1]);
        ConsoleHelper.WriteAt("[]", 14 + xOffset, 4 + yOffset, ConsoleColor.Blue);
        ConsoleHelper.WriteAt("[]", 16 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.YB)[1]);
        ConsoleHelper.WriteAt("[]", 12 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.WBR)[1]);
        ConsoleHelper.WriteAt("[]", 14 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.RB)[1]);
        ConsoleHelper.WriteAt("[]", 16 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YRB)[2]);
        // Yellow face (right)
        ConsoleHelper.WriteAt("[]", 18 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YBO)[0]);
        ConsoleHelper.WriteAt("[]", 20 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YO)[0]);
        ConsoleHelper.WriteAt("[]", 22 + xOffset, 3 + yOffset, Matrix.GetColumnValue(Piece.YOG)[0]);
        ConsoleHelper.WriteAt("[]", 18 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.YB)[0]);
        ConsoleHelper.WriteAt("[]", 20 + xOffset, 4 + yOffset, ConsoleColor.Yellow);
        ConsoleHelper.WriteAt("[]", 22 + xOffset, 4 + yOffset, Matrix.GetColumnValue(Piece.YG)[0]);
        ConsoleHelper.WriteAt("[]", 18 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YRB)[0]);
        ConsoleHelper.WriteAt("[]", 20 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YR)[0]);
        ConsoleHelper.WriteAt("[]", 22 + xOffset, 5 + yOffset, Matrix.GetColumnValue(Piece.YGR)[0]);
    }

    public override int GetHashCode() => ((Int128)this).GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj) => obj is RubiksCube3x3 cube && this == cube;

    public bool Equals(RubiksCube3x3 other) => this == other;

    public static bool operator ==(RubiksCube3x3 left, RubiksCube3x3 right) => (Int128)left == (Int128)right;
    public static bool operator !=(RubiksCube3x3 left, RubiksCube3x3 right) => (Int128)left != (Int128)right;
    public static explicit operator Int128(RubiksCube3x3 cube)
    {
        Int128 ans = 0;
        for (int i = 0; i < 8; i++)
            ans |= (Int128)(cube.Matrix.GetRowValue(i).Parity << 3 | cube.Matrix.RowPositions[i]) << (i * 6);
        for (int i = 9; i < 20; i++)
            ans |= (Int128)(cube.Matrix.GetRowValue(i).Parity << 5 | cube.Matrix.RowPositions[i]) << (i * 6);
        return ans;
    }
}
