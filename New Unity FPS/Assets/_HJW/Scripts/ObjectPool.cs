using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    GameObject poolingObjectPrefab;

    Queue<Bomb> POQ = new Queue<Bomb>();

    private void Awake()
    {
        instance = this;

        Initialize(1);
    }

    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            POQ.Enqueue(CreateNewObject());
        }
    }

    private Bomb CreateNewObject()
    {
        Bomb newObject = Instantiate(poolingObjectPrefab).GetComponent<Bomb>();
        newObject.gameObject.SetActive(false);
        newObject.transform.SetParent(transform);
        return newObject;
    }

    public static Bomb GetObject()
    {
        if (instance.POQ.Count > 0)
        {
            Bomb obj = instance.POQ.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Bomb newObject = instance.CreateNewObject();
            newObject.gameObject.SetActive(true);
            newObject.transform.SetParent(null);
            return newObject;
        }
    }

    public static void ReturnObject(Bomb obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.POQ.Enqueue(obj);
    }
}
