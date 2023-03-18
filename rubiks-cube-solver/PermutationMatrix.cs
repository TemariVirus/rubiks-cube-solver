using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

readonly struct PermutationMatrix : IEquatable<PermutationMatrix>
{
    public int Size => RowPositions.Length;
    public byte[] RowPositions { get; private init; }
    public Piece[] RowValues { get; private init; }

    public Piece this[int i, int j] => RowPositions[i] == j ? RowValues[i] : default!;

    public PermutationMatrix(byte[] rowPositions, Piece[] rowValues)
    {
        if (rowPositions is null)
            throw new ArgumentNullException(nameof(rowPositions));
        if (rowValues is null)
            throw new ArgumentNullException(nameof(rowValues));
        if (rowPositions.Length != rowValues.Length)
            throw new ArgumentException(
                $"{nameof(rowPositions)} and {nameof(rowValues)} must be of the same length."
            );
        if (rowPositions.Length > 256)
            throw new ArgumentException("Array was too large.");
        if (rowPositions.Distinct().Count() != rowPositions.Length)
            throw new ArgumentException(
                $"All elements in {nameof(rowPositions)} must be distinct."
            );

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

    public override int GetHashCode()
    {
        int hash = 0;
        for (int i = 0; i < Size; i++)
            hash ^= RowPositions[i].GetHashCode() ^ RowValues[i]!.GetHashCode();
        return hash;
    }

    public PermutationMatrix Clone() =>
        new() { RowPositions = (byte[])RowPositions.Clone(), RowValues = (Piece[])RowValues.Clone(), };

    public static PermutationMatrix Identity(int size) =>
        new()
        {
            RowPositions = Enumerable.Range(0, size).Select(i => (byte)i).ToArray(),
            RowValues = Enumerable.Range(0, size).Select(i => new Piece() { Value = (byte)(i << 2) }).ToArray(),
        };

    public bool IsIdentity()
    {
        for (int i = 0; i < Size; i++)
            if (RowPositions[i] != i)
                return false;
        return true;
    }

    public PermutationMatrix Inverse()
    {
        byte[] rowPositions = new byte[Size];
        Piece[] rowValues = new Piece[Size];
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

    public PermutationMatrix[] Decompose(PermutationMatrix[] factors)
    {
        if (factors.Length <= 0)
            throw new ArgumentException("Array of matrices to decompose into must not be empty.");

        if (factors.Contains(this))
            return new PermutationMatrix[] { this };

        if (IsIdentity())
            return Array.Empty<PermutationMatrix>();

        PermutationMatrix[] factorInverses = factors.Select(f => f.Inverse()).ToArray();
        // Meet-in-the-middle approach
        Dictionary<PermutationMatrix, LinkedListView<int>> ring = new() { { this, new() } };
        Dictionary<PermutationMatrix, LinkedListView<int>> otherRing = factors
            .Select((m, i) => (m, i))
            .ToDictionary(kvp => kvp.m, kvp => new LinkedListView<int>(new[] { kvp.i }));
        HashSet<PermutationMatrix> seen = ring.Concat(otherRing)
            .Select(x => x.Key)
            .ToHashSet();
        bool isReversed = false;
        while (true)
        {
            Dictionary<PermutationMatrix, LinkedListView<int>> newRing = new();
            foreach (var (matrix, list) in ring)
            {
                for (int i = 0; i < factors.Length; i++)
                {
                    var factor = isReversed ? factors[i] : factorInverses[i];
                    var composed = matrix * factor;

                    if (otherRing.TryGetValue(composed, out var otherList))
                        return (
                            isReversed
                                ? list.Append(i).Concat(otherList.Reverse())
                                : otherList.Append(i).Concat(list.Reverse())
                        )
                            .Select(i => factors[i])
                            .ToArray();

                    if (seen.Contains(composed))
                        continue;

                    newRing.Add(composed, list.Append(i));
                    seen.Add(composed);
                }
            }
            isReversed = !isReversed;
            ring = otherRing;
            otherRing = newRing;
        }
    }

    public static bool operator ==(PermutationMatrix left, PermutationMatrix right)
    {
        if (left.Size != right.Size)
            return false;

        for (int i = 0; i < left.Size; i++)
            if (left.RowPositions[i] != right.RowPositions[i] || left.RowValues[i] != right.RowValues[i])
                return false;

        return true;
    }

    public static bool operator !=(PermutationMatrix left, PermutationMatrix right) =>
        !(left == right);

    public static PermutationMatrix operator *(
        PermutationMatrix left,
        PermutationMatrix right
    )
    {
        if (left.Size != right.Size)
            throw new ArgumentException("Matrices must be of same size.");

        byte[] rowPositions = new byte[left.Size];
        Piece[] rowValues = new Piece[right.Size];
        for (int i = 0; i < left.Size; i++)
        {
            int j = left.RowPositions[i];
            rowValues[i] = left.RowValues[i] * right.RowValues[j];
            rowPositions[i] = right.RowPositions[j];
        }

        return new PermutationMatrix() { RowValues = rowValues, RowPositions = rowPositions };
    }
}
