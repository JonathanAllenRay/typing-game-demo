using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    List<Enemy> enemies = new List<Enemy>();
    void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }
        Debug.Log(enemies.Count);
    }
    private void OnEnable()
    {
        Enemy.spawned += EnemySpawned;
        Player.sparks += ApplySparks;
        SupportEnemy.healAllies += HealAllEnemies;
    }

    private void OnDisable()
    {
        Enemy.spawned -= EnemySpawned;
        Player.sparks -= ApplySparks;
        SupportEnemy.healAllies -= HealAllEnemies;
    }

    private void EnemySpawned(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    private void ApplySparks(int damage)
    {
        int index = Random.Range(0, enemies.Count - 1);
        enemies[index].TakeDamage(damage);
    }

    public bool BasicAttack(int damage, string word, string effect)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].BasicAttack(damage, word, effect))
            {
                return true;
            }
        }
        return false;
    }

    public void DeathRay()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].TakeDamage(50);
        }
    }

    public void GasBomb()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ApplyEffect("poison");
        }
    }

    private void HealAllEnemies(int amount)
    {
        Debug.Log(enemies.Count);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].TakeDamage(-amount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
