using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI enemyWord;
    public TextMeshProUGUI stats;
    public int health = 100;
    public int maxHealth = 100;
    public Player player;
    public WordManager wordManager;
    public int actionMax = 100;
    public int action = 0;
    public int actionChargePerSecond = 10;
    public int attackDamage = 10;
    public string nextAction = "attack";
    public delegate void enemyAttack(int attackDamage);
    public static event enemyAttack attack;
    private List<string> statuses = new List<string>();
    private int dazeTimer = 0;
    private int poisonTimer = 0;

    public delegate void enemySpawned(Enemy enemy);
    public static event enemySpawned spawned;

    void Start()
    {
        BaseSetup();
    }

    protected void BaseSetup()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (wordManager == null)
        {
            wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        }
        enemyWord.text = wordManager.GetWord();
        spawned(this);
        InvokeRepeating("Charge", 0f, 1.0f);
    }

    void Update()
    {
        if (action >= actionMax && attack != null)
        {
            AttackPlayer();
            action = 0;
        }
        BaseUpdate();
    }

    protected void BaseUpdate()
    {

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        stats.text = "HP: " + health + " - Action: " + action;
    }

    public bool BasicAttack(int damage, string word, string effect)
    {
        if (word == enemyWord.text)
        {
            ApplyEffect(effect);
            enemyWord.text = wordManager.GetWord();
            health -= damage;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ApplyEffect(string effect)
    {
        if (effect == "daze")
        {
            statuses.Add(effect);
            dazeTimer = 10;
        }
        else if (effect == "poison")
        {
            statuses.Add(effect);
            poisonTimer = 10;
            InvokeRepeating("TakePoisonDamage", 0f, 1.0f);
        }
    }

    private void TakePoisonDamage()
    {
        if (poisonTimer <= 0)
        {
            CancelInvoke("TakePoisonDamage");
        } else
        {
            poisonTimer -= 1;
            TakeDamage(5);
        }
    }

    private void Charge()
    {
        if (statuses.Contains("daze"))
        {
            dazeTimer -= 1;
            if (dazeTimer <= 0)
            {
                statuses.Remove("daze");
            }
            action += (actionChargePerSecond / 3);
        }
        else
        {
            action += actionChargePerSecond;
        }

        if (action > 100)
        {
            action = 100;
        }
    }

    protected void AttackPlayer()
    {
        attack(attackDamage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
