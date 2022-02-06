using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawnPoints;
    public Player player;
    public GameObject[] spawnedEnemies;

    private float spawnTimerMax = 10f;
    private float spawnTimerCurrent = 0f;

    private int maxEnemies = 1;

    public GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
        if (spawnTimerCurrent <= 0f && EnemiesCount() < maxEnemies)
        {
            spawnTimerCurrent = spawnTimerMax;
            SpawnEnemy(enemies[Random.Range(0, player.GetLevel())]);
        }
        spawnTimerCurrent -= Time.deltaTime;
    }

    void SpawnEnemy(GameObject enemy)
    {
        int spawnPoint = GetSpawn();
        if (spawnPoint >= 0)
        {
            spawnedEnemies[spawnPoint] = GameObject.Instantiate(enemy, spawnPoints[spawnPoint].transform.position, spawnPoints[spawnPoint].transform.rotation);
        }
    }

    private int GetSpawn()
    {
        for (int i = 0; i < spawnedEnemies.Length; i++)
        {
            if (spawnedEnemies[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    private int EnemiesCount()
    {
        int count = 0;
        for (int i = 0; i < spawnedEnemies.Length; i++)
        {
            if (spawnedEnemies[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    private void UpdateMaxEnemies()
    {
        maxEnemies = player.GetLevel();
    }

    private void OnEnable()
    {
        Player.lvlUp += UpdateMaxEnemies;
    }

    private void OnDisable()
    {
        Player.lvlUp -= UpdateMaxEnemies;
    }

}
