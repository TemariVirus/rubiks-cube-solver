using System.Diagnostics.CodeAnalysis;

namespace RubiksCubeSolver;

readonly struct PermutationMatrix<T> : IEquatable<PermutationMatrix<T>>
    where T : IMultiplyOperators<T, T, T>, IEquatable<T>
{
    public int Size => RowPositions.Length;
    public int[] RowPositions { get; private init; }
    public T[] RowValues { get; private init; }

    public T this[int i, int j] => RowPositions[i] == j ? RowValues[i] : default!;

    public PermutationMatrix(int[] rowPositions, T[] rowValues)
    {
        if (rowPositions is null)
            throw new ArgumentNullException(nameof(rowPositions));
        if (rowValues is null)
            throw new ArgumentNullException(nameof(rowValues));
        if (rowPositions.Length != rowValues.Length)
            throw new ArgumentException(
                $"{nameof(rowPositions)} and {nameof(rowValues)} must be of the same length."
            );
        if (rowPositions.Distinct().Count() != rowPositions.Length)
            throw new ArgumentException(
                $"All elements in {nameof(rowPositions)} must be distinct."
            );

        RowPositions = rowPositions;
        RowValues = rowValues;
    }

    public bool Equals(PermutationMatrix<T> other) => this == other;

    public override bool Equals([NotNullWhen(true)] object? obj) =>
        obj is PermutationMatrix<T> matrix && this == matrix;

    public override int GetHashCode()
    {
        int hash = 0;
        for (int i = 0; i < Size; i++)
            hash ^= RowPositions[i].GetHashCode() ^ RowValues[i].GetHashCode();
        return hash;
    }

    public PermutationMatrix<T> Clone() =>
        new() { RowPositions = (int[])RowPositions.Clone(), RowValues = (T[])RowValues.Clone(), };

    public T GetRowValue(int rowIndex) => RowValues[rowIndex];

    public T GetColumnValue(int columnIndex)
    {
        for (int i = 0; true; i++)
            if (RowPositions[i] == columnIndex)
                return RowValues[i];
    }

    public PermutationMatrix<T> Pow(int power)
    {
        PermutationMatrix<T> ans = this;
        for (int i = 1; i < power; i++)
            ans *= this;
        return ans;
    }

    public static bool operator ==(PermutationMatrix<T> left, PermutationMatrix<T> right)
    {
        if (left.Size != right.Size)
            return false;

        for (int i = 0; i < left.Size; i++)
            if (
                left.RowPositions[i] != right.RowPositions[i]
                || left.RowValues[i].Equals(right.RowValues[i])
            )
                return false;

        return true;
    }

    public static bool operator !=(PermutationMatrix<T> left, PermutationMatrix<T> right) =>
        !(left == right);

    public static PermutationMatrix<T> operator *(
        PermutationMatrix<T> left,
        PermutationMatrix<T> right
    )
    {
        #if DEBUG
        if (left.Size != right.Size)
            throw new ArgumentException("Matrices must be of same size.");
        #endif

        int[] rowPositions = new int[left.Size];
        T[] rowValues = new T[right.Size];
        for (int i = 0; i < left.Size; i++)
        {
            int j = left.RowPositions[i];
            rowValues[i] = left.RowValues[i].Multiply(right.RowValues[j]);
            rowPositions[i] = right.RowPositions[j];
        }

        return new PermutationMatrix<T>() { RowValues = rowValues, RowPositions = rowPositions };
    }
}
