using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool2 : MonoBehaviour
{
    public static ObjectPool2 instance;

    private List<GameObject> pooledObjects2 = new List<GameObject>();
    private int amountToPool = 1;

    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjects2.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        for ( int i = 0; i < pooledObjects2.Count; i++)
        {
            if (!pooledObjects2[i].activeInHierarchy)
            {
                return pooledObjects2[i];
            }
        }

        return null;
    }
}
