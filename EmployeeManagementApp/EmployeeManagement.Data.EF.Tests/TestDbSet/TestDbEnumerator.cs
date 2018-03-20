using System.Collections;
using System.Collections.Generic;

internal class TestDbEnumerator<T> : IEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestDbEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public void Dispose()
    {
        _inner.Dispose();
    }

    public T Current => _inner.Current;

    public bool MoveNext()
    {
        throw new System.NotImplementedException();
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    object IEnumerator.Current => throw new System.NotImplementedException();
}