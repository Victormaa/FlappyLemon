using System;
using System.Runtime.CompilerServices;
using UnityEngine;


internal class CachedQueue<T>
{
    private T[] _data;
    private int _size;
    private int _head;
    private int _end;

    public CachedQueue(int size)
    {
        _data = new T[size + 1];
        _size = size + 1;
        _head = 0;
        _end = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Enqueue(T value)
    {
        if ((_end + 1) % _size == _head)
        {
#if DEBUG && !PERFORMANCE && !UI_DEVELOP_MODE
            Debug.LogError("CachedQueue IndexOutOfRangeException");
#endif
            return;
        }
        _data[_end] = value;
        _end = (_end + 1) % _size;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Dequeue()
    {
        T value = default(T);
        if (_head == _end)
        {
            return value;
        }
        value = _data[_head];
        _head = (_head + 1) % _size;
        return value;
    }

    public int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return (_end + _size - _head) % _size; }
    }

    public int Capacity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return _size - 1; }
    }

    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _data[(_head + index) % _size];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _data[(_head + index) % _size] = value;
        }
    }

    public T Last
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _data[(_end + _size - 1) % _size];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _data[(_end + _size - 1) % _size] = value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _head = 0;
        _end = 0;
    }
}

