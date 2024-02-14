using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private List<T> objPool;
    private Transform _container;

    public ObjectPool(T prefab, float preloadObjects, string parent)
    {
        _container = new GameObject(parent).transform;
        this.prefab = prefab;
        objPool = new List<T>();

        for (int i = 0; i < preloadObjects; i++)
        {
            Create();
        }
    }

    public T Get()
    {
        T obj = objPool.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
        {
            obj = Create();
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Realease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private T Create()
    {
        T newObj = GameObject.Instantiate(prefab, _container);
        objPool.Add(newObj);
        newObj.gameObject.SetActive(false);

        return newObj;
    }
}