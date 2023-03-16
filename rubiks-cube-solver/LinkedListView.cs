using System.Collections;

namespace RubiksCubeSolver;

sealed class LinkedListView<T> : IEnumerable<T>
{
    private readonly LinkedListView<T>? Head = null;
    private readonly T? Last;

    public LinkedListView() { }

    private LinkedListView(LinkedListView<T> head, T last)
    {
        Head = head;
        Last = last;
    }

    public LinkedListView<T> Append(T item) => new(this, item);
    
    public LinkedListView<T> Append(IEnumerable<T> items)
    {
        LinkedListView<T> result = this;
        foreach (T item in items)
            result = result.Append(item);
        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (Head is null)
            yield break;

        foreach (T item in Head)
            yield return item;
        yield return Last!;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
