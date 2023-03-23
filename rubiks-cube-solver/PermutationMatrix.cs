using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

internal readonly record struct ConvertibleInt32 : IUInt32conversions<ConvertibleInt32>
{
    public int Value { get; private init; }

    public static ConvertibleInt32 FromUInt32(uint value) => new() { Value = (int)value };

    public static uint ToUInt32(ConvertibleInt32 value) => (uint)value.Value;

    public static implicit operator int(ConvertibleInt32 value) => value.Value;

    public static implicit operator ConvertibleInt32(int value) => new() { Value = value };
}

internal readonly struct PermutationMatrix
{
    public int Size => RowPositions.Length;
    public SixBitArray<ConvertibleInt32> RowPositions { get; private init; }
    public SixBitArray<Piece> RowValues { get; private init; }

    public PermutationMatrix(
        SixBitArray<ConvertibleInt32> rowPositions,
        SixBitArray<Piece> rowValues
    )
    {
#if DEBUG
        if (rowPositions.Length != rowValues.Length)
            throw new ArgumentException(
                $"{nameof(rowPositions)} and {nameof(rowValues)} must be of the same length."
            );
        if (rowPositions.Distinct().Count() != rowPositions.Length)
            throw new ArgumentException(
                $"All elements in {nameof(rowPositions)} must be distinct."
            );
#endif
        RowPositions = rowPositions;
        RowValues = rowValues;
    }

    public Piece GetRowValue(int rowIndex) => RowValues[rowIndex];

    public Piece GetColumnValue(int columnIndex)
    {
        for (int i = 0; true; i++)
            if (RowPositions[i] == columnIndex)
                return RowValues[i];
    }

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is PermutationMatrix matrix && this == matrix;

    public bool Equals(PermutationMatrix other) => this == other;

    public override int GetHashCode() => RowPositions.GetHashCode() ^ RowValues.GetHashCode();

    public PermutationMatrix Clone() =>
        new() { RowPositions = RowPositions, RowValues = RowValues, };

    public static PermutationMatrix Identity(int size) =>
        new()
        {
            RowPositions = new(Enumerable.Range(0, size).Select(i => (ConvertibleInt32)i)),
            RowValues = new(
                Enumerable.Range(0, size).Select(i => new Piece() { Value = (byte)(i << 2) })
            ),
        };

    public PermutationMatrix Inverse()
    {
        SixBitArray<ConvertibleInt32> rowPositions = new(Size);
        SixBitArray<Piece> rowValues = new(Size);
        for (byte i = 0; i < Size; i++)
        {
            int j = RowPositions[i];
            rowPositions[j] = i;
            rowValues[j] = RowValues[i].Inverse();
        }
        return new() { RowPositions = rowPositions, RowValues = rowValues };
    }

    public PermutationMatrix Pow(int power)
    {
        if (power == 0)
            return Identity(Size);
        if (power < 0)
            return Inverse().Pow(-power);

        var ans = (this * this).Pow(power >> 1);
        return (power & 0x1) == 0 ? ans : ans * this;
    }

    public static bool operator ==(PermutationMatrix left, PermutationMatrix right) =>
        left.RowValues == right.RowValues && left.RowPositions == right.RowPositions;

    public static bool operator !=(PermutationMatrix left, PermutationMatrix right) =>
        left.RowValues != right.RowValues || left.RowPositions != right.RowPositions;

    public static PermutationMatrix operator *(PermutationMatrix left, PermutationMatrix right)
    {
        if (left.Size != right.Size)
            throw new ArgumentException("Matrices must be of same size.");

        SixBitArray<ConvertibleInt32> rowPositions = new(left.Size);
        SixBitArray<Piece> rowValues = new(right.Size);
        for (int i = 0; i < left.Size; i++)
        {
            int j = left.RowPositions[i];
            rowValues[i] = left.RowValues[i] * right.RowValues[j];
            rowPositions[i] = right.RowPositions[j];
        }

        return new PermutationMatrix() { RowPositions = rowPositions, RowValues = rowValues };
    }
}
