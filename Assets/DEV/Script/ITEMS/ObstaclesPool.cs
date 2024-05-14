using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private GameObject[] obstaclesToPool; // Pool'da kullanýlacak engellerin prefab'larý
    [SerializeField] private int amountOfObstaclesToPool; // Havuzda oluþturulacak maksimum engel sayýsý
    [SerializeField] private Vector3[] initialObstaclePositions; // Engellerin baþlangýç pozisyonlarý

    private List<GameObject> pooledObstacles; // Havuzdaki engelleri tutan liste

    void Awake()
    {
        pooledObstacles = new List<GameObject>();

        
        for (int i = 0; i < amountOfObstaclesToPool; i++)
        {
           
            int prefabIndex = i % obstaclesToPool.Length;
            GameObject obstacle = Instantiate(obstaclesToPool[prefabIndex]);

           
            Vector3 initialPosition = initialObstaclePositions[i % initialObstaclePositions.Length];
            obstacle.transform.position = initialPosition;

            
            obstacle.SetActive(true);

            pooledObstacles.Add(obstacle);
        }
    }

    public GameObject GetPooledObstacle()
    {
        foreach (GameObject obstacle in pooledObstacles)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }
        GameObject newObstacle = Instantiate(obstaclesToPool[0]);
        newObstacle.SetActive(false);
        pooledObstacles.Add(newObstacle);
        return newObstacle;
    }

    public void SpawnObstacle(Vector3 position)
    {
        GameObject obstacleItem = GetPooledObstacle();
        if (obstacleItem != null)
        {
            obstacleItem.transform.position = position;
            obstacleItem.SetActive(true);
        }
    }

    public void ReturnObstacleToPool(GameObject obstacle)
    {
        obstacle.SetActive(false);
    }
}
