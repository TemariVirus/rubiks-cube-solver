using System.Collections;

namespace RubiksCubeSolver;

internal interface IUInt32conversions<T>
{
    public static abstract uint ToUInt32(T value);
    public static abstract T FromUInt32(uint value);
}

internal record SixBitArray<T> : IEnumerable<T>
    where T : IUInt32conversions<T>
{
    private const int MAX_LENGTH = 20;
    private const int ITEM_SIZE = 6;
    private const uint ITEM_MASK = 0x3F;

    private UInt128 Data { get; set; }
    public int Length
    {
        get => (int)(Data >> (MAX_LENGTH * ITEM_SIZE));
        private init
        {
#if DEBUG
            if (value < 0 || value > MAX_LENGTH)
                throw new ArgumentOutOfRangeException(nameof(value));
#endif
            Data &= new UInt128(ulong.MaxValue >> (128 - MAX_LENGTH * ITEM_SIZE), ulong.MaxValue);
            Data |= (UInt128)value << (MAX_LENGTH * ITEM_SIZE);
        }
    }

    public T this[int i]
    {
        get
        {
#if DEBUG
            if (i >= Length)
                throw new IndexOutOfRangeException();
#endif
            return T.FromUInt32((uint)(Data >> (i * ITEM_SIZE)) & ITEM_MASK);
        }
        set
        {
#if DEBUG
            if (i >= Length)
                throw new IndexOutOfRangeException();
            if (T.ToUInt32(value) > ITEM_MASK)
                throw new ArgumentOutOfRangeException(nameof(value));
#endif
            Data &= ~((UInt128)ITEM_MASK << (i * ITEM_SIZE));
            Data |= (UInt128)T.ToUInt32(value) << (i * ITEM_SIZE);
        }
    }

    public SixBitArray(int length)
    {
        Length = length;
    }

    public SixBitArray(IEnumerable<T> data)
    {
        Length = data.Count();
        int i = 0;
        foreach (T item in data)
        {
            this[i] = item;
            i++;
        }
    }

    public SixBitArray(params T[] data)
        : this(data.AsEnumerable()) { }

    public SixBitArray<T> Add(T item)
    {
        SixBitArray<T> newArray = new() { Data = Data, Length = Length + 1 };
        newArray[Length] = item;
        return newArray;
    }

    public void CopyValue(int srcIndex, SixBitArray<T> dst, int dstIndex)
    {
#if DEBUG
        if (srcIndex < 0
            || srcIndex > Length
            || dstIndex < 0
            || dstIndex > dst.Length)
            throw new IndexOutOfRangeException();
#endif
        UInt128 value = (Data >> (srcIndex * ITEM_SIZE)) & ITEM_MASK;
        dst.Data &= ~((UInt128)ITEM_MASK << (dstIndex * ITEM_SIZE));
        dst.Data |= value << (dstIndex * ITEM_SIZE);
    }

    public IEnumerator<T> GetEnumerator()
    {
        UInt128 data = Data;
        for (int i = 0; i < Length; i++)
        {
            yield return T.FromUInt32((uint)data & ITEM_MASK);
            data >>= ITEM_SIZE;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
