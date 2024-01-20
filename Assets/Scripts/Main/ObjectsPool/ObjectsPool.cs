using System.Collections.Generic;
using System.Linq;

public class ObjectsPool<T>
{
    private readonly int _capacity;
    private readonly List<T> _pool;
    public ObjectsPool(int capacity)
    {
        _capacity = capacity;
        _pool = new List<T>();
    }
    public bool IsFull => _pool.Count == _capacity;
    public bool HasElements => _pool.Count > 0;
    public void Push(T item) =>
        _pool.Add(item);
    public T Pop()
    {
        T obj = _pool.First();
        _pool.Remove(obj);
        return obj;
    }
}
