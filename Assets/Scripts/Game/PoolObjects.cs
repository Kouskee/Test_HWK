using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolObjects<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _origin;

    private readonly List<T> _pool;

    public PoolObjects(T prefab, Transform origin, int capacity = 5)
    {
        _prefab = prefab;
        _origin = origin;
        _pool = new(capacity);
    }

    public void InitPool()
    {
        for (int i = 0; i < _pool.Capacity; i++)
        {
            var component = Create();
            component.gameObject.SetActive(false);
        }
    }

    private T Create()
    {
        var instance = Object.Instantiate(_prefab, _origin.position, Quaternion.identity, _origin);
        _pool.Add(instance);
        return instance;
    }

    public T Get()
    {
        T poolObject = _pool.FirstOrDefault(obj => !obj.gameObject.activeSelf);

        poolObject ??= Create();
        poolObject.gameObject.SetActive(true);

        return poolObject;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.position = _origin.position;
    }
}