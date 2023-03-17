using System.Collections;

namespace RubiksCubeSolver;

sealed class LinkedListView<T> : IEnumerable<T>
{
    private readonly LinkedListView<T>? Head = null;
    private readonly T? Last;

    public LinkedListView() { }

    public LinkedListView(IEnumerable<T> collection)
    {
        if (!collection.Any())
            return;

        Head = new LinkedListView<T>(collection.SkipLast(1));
        Last = collection.Last();
    }

    private LinkedListView(LinkedListView<T> head, T last)
    {
        Head = head;
        Last = last;
    }

    public LinkedListView<T> Append(T item) => new(this, item);

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
