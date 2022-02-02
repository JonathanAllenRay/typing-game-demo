using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawnPoints;
    public bool[] spawnPointsOccupied;

    public GameObject[] enemies;
        
    void Start()
    {
        SpawnEnemy(enemies[0]);
        SpawnEnemy(enemies[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(GameObject enemy)
    {
        int spawnPoint = GetSpawn();
        if (spawnPoint >= 0)
        {
            spawnPointsOccupied[spawnPoint] = true;
            GameObject.Instantiate(enemy, spawnPoints[spawnPoint].transform);
        }
    }

    private int GetSpawn()
    {
        for (int i = 0; i < spawnPointsOccupied.Length; i++)
        {
            if (!spawnPointsOccupied[i])
            {
                return i;
            }
        }
        return -1;
    }
}
