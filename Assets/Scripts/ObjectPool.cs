using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> objectPoolList = new List<GameObject>();

    void Start()
    {
        CreatePool();
    }

    /// <summary>
    /// Create a pool of objects based on the specified pool size and object prefab
    /// </summary>
    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    /// <summary>
    /// Instantiate new Object and add it to the pool list
    /// </summary>
    /// <returns></returns>
    private GameObject CreateNewObject()
    {
        // Instantiate a new object from the prefab
        GameObject newObject = Instantiate(objectPrefab);

        // Set the new object to inactive and add it to the pool list
        newObject.SetActive(false);

        // Add the new object to the pool list and return it
        objectPoolList.Add(newObject);
        return newObject;
    }

    /// <summary>
    ///  Get an object from the pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool()
    {
        GameObject availableObject = objectPoolList.Find(obj => !obj.activeInHierarchy);

        if (availableObject == null)
        {
            availableObject = CreateNewObject();
        }

        availableObject.SetActive(true);
        return availableObject;
    }
}