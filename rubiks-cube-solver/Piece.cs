namespace RubiksCubeSolver;

readonly record struct Piece : IEquatable<Piece>
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

    public static readonly Piece WBR = new() { Value = 0 << 2 };
    public static readonly Piece WRG = new() { Value = 1 << 2 };
    public static readonly Piece WGO = new() { Value = 2 << 2 };
    public static readonly Piece WOB = new() { Value = 3 << 2 };
    public static readonly Piece YBO = new() { Value = 4 << 2 };
    public static readonly Piece YRB = new() { Value = 5 << 2 };
    public static readonly Piece YGR = new() { Value = 6 << 2 };
    public static readonly Piece YOG = new() { Value = 7 << 2 };
    public static readonly Piece WR = new() { Value = 8 << 2 };
    public static readonly Piece WG = new() { Value = 9 << 2 };
    public static readonly Piece WO = new() { Value = 10 << 2 };
    public static readonly Piece WB = new() { Value = 11 << 2 };
    public static readonly Piece RB = new() { Value = 12 << 2 };
    public static readonly Piece GR = new() { Value = 13 << 2 };
    public static readonly Piece OG = new() { Value = 14 << 2 };
    public static readonly Piece BO = new() { Value = 15 << 2 };
    public static readonly Piece YB = new() { Value = 16 << 2 };
    public static readonly Piece YR = new() { Value = 17 << 2 };
    public static readonly Piece YG = new() { Value = 18 << 2 };
    public static readonly Piece YO = new() { Value = 19 << 2 };

    public byte Value { get; init; }
    public bool IsCorner => Id < 8;
    public int Id => Value >> 2;
    public int Parity => Value & 0x3;

    public ConsoleColor this[int i] => Colors[Id][(i + Parity) % (IsCorner ? 3 : 2)];

    public Piece Rotate(int rotation)
    {
        int newParity = (Parity + rotation) % (IsCorner ? 3 : 2);
        return new() { Value = (byte)((Value & ~0x3) | newParity) };
    }

    public Piece Inverse()
    {
        int newParity = IsCorner ? (3 - Parity) % 3 : Parity ^ 0x1;
        return new() { Value = (byte)((Value & ~0x3) | newParity) };
    }

    public bool Equals(Piece other) => Value == other.Value;

    public override int GetHashCode() => Value;

    public static Piece operator *(Piece left, Piece right) => left.Rotate(right.Parity);
}
