namespace RubiksCubeSolver;

interface IMultiplyOperators<TLeft, TRight, TResult>
    where TLeft : IMultiplyOperators<TLeft, TRight, TResult>
    where TRight : IMultiplyOperators<TLeft, TRight, TResult>
    where TResult : IMultiplyOperators<TLeft, TRight, TResult>
{
    abstract TResult Multiply(TRight other);
}

static class Piece
{
    public const int WBR = 0;
    public const int WRG = 1;
    public const int WGO = 2;
    public const int WOB = 3;
    public const int YBO = 4;
    public const int YRB = 5;
    public const int YGR = 6;
    public const int YOG = 7;
    public const int WR = 8;
    public const int WG = 9;
    public const int WO = 10;
    public const int WB = 11;
    public const int RB = 12;
    public const int GR = 13;
    public const int OG = 14;
    public const int BO = 15;
    public const int YB = 16;
    public const int YR = 17;
    public const int YG = 18;
    public const int YO = 19;
}

interface IPiece : IMultiplyOperators<IPiece, IPiece, IPiece>, IEquatable<IPiece>
{
    public int Value { get; }
    public int Id { get; }
    public int Parity { get; }

    public abstract ConsoleColor this[int i] { get; }

    public abstract IPiece Rotate(int rotation);
}

readonly record struct EdgePiece : IPiece
{
    static readonly ConsoleColor[][] Colors =
        {
            new ConsoleColor[] { ConsoleColor.White, ConsoleColor.Red },
            new ConsoleColor[] { ConsoleColor.White, ConsoleColor.Green },
            new ConsoleColor[] { ConsoleColor.White, ConsoleColor.DarkYellow },
            new ConsoleColor[] { ConsoleColor.White, ConsoleColor.Blue },
            new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue },
            new ConsoleColor[] { ConsoleColor.Green, ConsoleColor.Red },
            new ConsoleColor[] { ConsoleColor.DarkYellow, ConsoleColor.Green },
            new ConsoleColor[] { ConsoleColor.Blue, ConsoleColor.DarkYellow },
            new ConsoleColor[] { ConsoleColor.Yellow, ConsoleColor.Blue },
            new ConsoleColor[] { ConsoleColor.Yellow, ConsoleColor.Red },
            new ConsoleColor[] { ConsoleColor.Yellow, ConsoleColor.Green },
            new ConsoleColor[] { ConsoleColor.Yellow, ConsoleColor.DarkYellow },
        };

    public static readonly EdgePiece WR = new() { Value = Piece.WR << 2 };
    public static readonly EdgePiece WG = new() { Value = Piece.WG << 2 };
    public static readonly EdgePiece WO = new() { Value = Piece.WO << 2 };
    public static readonly EdgePiece WB = new() { Value = Piece.WB << 2 };
    public static readonly EdgePiece RB = new() { Value = Piece.RB << 2 };
    public static readonly EdgePiece GR = new() { Value = Piece.GR << 2 };
    public static readonly EdgePiece OG = new() { Value = Piece.OG << 2 };
    public static readonly EdgePiece BO = new() { Value = Piece.BO << 2 };
    public static readonly EdgePiece YB = new() { Value = Piece.YB << 2 };
    public static readonly EdgePiece YR = new() { Value = Piece.YR << 2 };
    public static readonly EdgePiece YG = new() { Value = Piece.YG << 2 };
    public static readonly EdgePiece YO = new() { Value = Piece.YO << 2 };

    private int Value { get; init; }
    int IPiece.Value => Value;
    public int Id => Value >> 2;
    public int Parity => Value & 0x1;

    public ConsoleColor this[int i] => Colors[Id - Piece.WR][(i + Parity) & 0x1];

    public IPiece Rotate(int rotation)
    {
        int newParity = (Parity + rotation) & 0x1;
        return new EdgePiece { Value = ((Value & ~0x1) | newParity) };
    }

    public IPiece Multiply(IPiece other) => Rotate(other.Parity);

    #nullable disable
    public bool Equals(IPiece other) => Value == other.Value;
    #nullable enable

    public override int GetHashCode() => Value;
}

readonly record struct CornerPiece : IPiece
{
    static readonly ConsoleColor[][] Colors =
        {
            new[] { ConsoleColor.White, ConsoleColor.Blue, ConsoleColor.Red },
            new[] { ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Green },
            new[] { ConsoleColor.White, ConsoleColor.Green, ConsoleColor.DarkYellow },
            new[] { ConsoleColor.White, ConsoleColor.DarkYellow, ConsoleColor.Blue },
            new[] { ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.DarkYellow },
            new[] { ConsoleColor.Yellow, ConsoleColor.Red, ConsoleColor.Blue },
            new[] { ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Red },
            new[] { ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.Green },
        };

    public static readonly CornerPiece WBR = new() { Value = Piece.WBR << 2 };
    public static readonly CornerPiece WRG = new() { Value = Piece.WRG << 2 };
    public static readonly CornerPiece WGO = new() { Value = Piece.WGO << 2 };
    public static readonly CornerPiece WOB = new() { Value = Piece.WOB << 2 };
    public static readonly CornerPiece YBO = new() { Value = Piece.YBO << 2 };
    public static readonly CornerPiece YRB = new() { Value = Piece.YRB << 2 };
    public static readonly CornerPiece YGR = new() { Value = Piece.YGR << 2 };
    public static readonly CornerPiece YOG = new() { Value = Piece.YOG << 2 };

    private int Value { get; init; }
    int IPiece.Value => Value;
    public int Id => Value >> 2;
    public int Parity => Value & 0x3;

    public ConsoleColor this[int i] => Colors[Id - Piece.WBR][(i + Parity) % 3];

    public IPiece Rotate(int rotation)
    {
        int newParity = Parity + rotation;
        newParity = newParity >= 3 ? newParity - 3 : newParity;
        return new CornerPiece() { Value = (Value & ~0x3) | newParity };
    }

    public IPiece Multiply(IPiece other) => Rotate(other.Parity);

    #nullable disable
    public bool Equals(IPiece other) => Value == other.Value;
    #nullable enable

    public override int GetHashCode() => Value;
}
