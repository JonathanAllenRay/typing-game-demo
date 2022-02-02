using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{

    public EnemyManager enemyManager;
    public Player player;
    public MainWordQueue mainWordQueue;
    private Dictionary<string, string> openWith = new Dictionary<string, string>();
    private Dictionary<string, int> abilities = new Dictionary<string, int>();

    private const int inventorySize = 3;
    private List<string> inventory = new List<string>(inventorySize);

    // Start is called before the first frame update
    void Start()
    {
        if (enemyManager == null)
        {
            enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        }

        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        if (mainWordQueue == null)
        {
            mainWordQueue = GameObject.Find("MainWordQueueManager").GetComponent<MainWordQueue>();
        }
        InitializeDemoInventory();
        InitializeAbilities();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        TypingManager.delegateInstance += ProcessWord;
    }

    private void OnDisable()
    {
        TypingManager.delegateInstance -= ProcessWord;
    }

    private void ProcessWord(string word)
    {
        if (mainWordQueue.PopWord(word))
        {
            player.ChargeUp(10);
        }
        else if (player.GetCharge() >= 10 && enemyManager.BasicAttack(player.GetDamage(), word, player.GetEffect()))
        {
            player.DrainCharge(10);
        }
        else if (abilities.ContainsKey(word) && player.GetCharge() >= abilities[word])
        {
            UseAbility(word);
        }
        else if (inventory.Contains(word)) 
        {
            UseItem(word);
        }
        else
        {
            player.DrainCharge(5);
        }
    }

    private void InitializeAbilities()
    {
        abilities.Add("deathray", 50);
        abilities.Add("repair", 25);
        abilities.Add("shield", 20);
        abilities.Add("daze", 30);
        abilities.Add("sparks", 20);
        abilities.Add("overcharge", 20);
    }

    private void InitializeDemoInventory()
    {
        inventory.Add("battery");
        inventory.Add("gas");
        inventory.Add("scrap");
    }

    private void UseAbility(string word)
    {
        if (abilities.ContainsKey(word))
        {
            player.DrainCharge(abilities[word]);
        }

        if (word == "deathray")
        {
            enemyManager.DeathRay();
        }
        else if (word == "repair")
        {
            player.Attacked(-25);
        }
        else if (word == "shield")
        {
            player.ActivateShield();
        }
        else if (word == "daze")
        {
            player.PrimeDaze();
        }
        else if (word == "sparks")
        {
            player.ActivateSparks();
        }
        else if (word == "overcharge")
        {
            player.ActivateOvercharge();
        }
    }

    private void UseItem(string item)
    {
        if (inventory.Contains(item))
        {
            switch (item)
            {
                case "gas":
                    enemyManager.GasBomb();
                    break;
                case "battery":
                    player.UseBattery();
                    break;
                case "scrap":
                    player.UseScrap();
                    break;
            }
            inventory.Remove(item);
        }
    }
}
