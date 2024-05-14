using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPooling : MonoBehaviour
{
    
    [Header("Collectable")]
    [SerializeField] private GameObject[] objectsToPool; 
    [SerializeField] private int amountToPool; 
    [SerializeField] private Vector3[] initialPositions; 

    private List<GameObject> pooledObjects; 

    void Awake()
    {
        pooledObjects = new List<GameObject>();

        
        for (int i = 0; i < amountToPool; i++)
        {
            
            int prefabIndex = i % objectsToPool.Length;
            GameObject obj = Instantiate(objectsToPool[prefabIndex]);

           
            Vector3 initialPosition = initialPositions[i % initialPositions.Length];
            obj.transform.position = initialPosition;

            
            obj.SetActive(true);

            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject newObj = Instantiate(objectsToPool[0]);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }
    public void SpawnObject(Vector3 position)
    {
        GameObject item = GetPooledObject();
        if (item != null)
        {
            item.transform.position = position;
            item.SetActive(true);
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    } 
}
