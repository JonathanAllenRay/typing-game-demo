using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int charge = 0;
    private const int maxCharge = 100;
    public const int maxHealth = 100;
    public int health = maxHealth;
    public Text healthText;
    public Text chargeText;
    private bool shieldActive;
    private int sparkDamage = 0;
    private int overchargeCounter = 0;
    private int attackDamage = 10;
    private int overchargeDamage = 15;
    private string effect;


    public delegate void playerDied();
    public static event playerDied died;
    public delegate void chargeSparks(int damage);
    public static event chargeSparks sparks;

    // Start is called before the first frame update
    void Start()
    {
        if (healthText == null)
        {
            healthText = GameObject.Find("Health").GetComponent<Text>();
        }

        if (chargeText == null)
        {
            chargeText = GameObject.Find("Charge").GetComponent<Text>();
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Game Over");
            died();
            Destroy(this.gameObject);
        }

        healthText.text = "HP: " + health;
        chargeText.text = "Charge: " + charge;
    }

    private void OnEnable()
    {
        MainWordQueue.hit += WordQueueHit;
        Enemy.attack += Attacked;
        SupportEnemy.attack += Attacked;
        TankEnemy.attack += Attacked;
        TauntEnemy.attack += Attacked;
    }

    private void OnDisable()
    {
        MainWordQueue.hit -= WordQueueHit;
        Enemy.attack -= Attacked;
        SupportEnemy.attack -= Attacked;
        TauntEnemy.attack -= Attacked;
    }

    private void WordQueueHit()
    {
        charge += 5;
        if (charge > maxCharge)
        {
            charge = maxCharge;
        }
    }

    public int GetDamage()
    {
        return attackDamage;
    }

    public void DrainCharge(int chargeAmount)
    {
        charge -= chargeAmount;
        if (charge < 0)
        {
            charge = 0;
        }
    }

    public void ChargeUp(int chargeAmount)
    {
        if (sparkDamage > 0)
        {
            sparks(sparkDamage);
            sparkDamage -= 5;
        }
        charge += chargeAmount;
        if (charge > maxCharge)
        {
            charge = maxCharge;
        }
    }

    public int GetCharge()
    {
        return charge;
    }

    public bool ActivateShield()
    {
        if (shieldActive == false)
        {
            shieldActive = true;
            Debug.Log("Shield activated");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActivateSparks()
    {
        sparkDamage = 15;
    }

    public void ActivateOvercharge()
    {
        CancelInvoke("CountdownOvercharge");
        attackDamage += overchargeDamage;
        overchargeCounter = 10;
        InvokeRepeating("CountdownOvercharge", 0f, 1.0f);
    }

    private void CountdownOvercharge()
    {
        overchargeCounter -= 1;
        if (overchargeCounter <= 0)
        {
            attackDamage -= overchargeDamage;
            CancelInvoke("CountdownOvercharge");
        }
    }

    public void PrimeDaze()
    {
        effect = "daze";
    }

    public void Attacked(int attackDamage)
    {
        if (shieldActive && attackDamage > 0)
        {
            attackDamage /= 2;
            shieldActive = false;
            Debug.Log("Shield down");
        }

        health -= attackDamage;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }



    // Get current boosted effect of attack
    public string GetEffect()
    {
        return effect;
    }

#region items

    public void UseScrap()
    {
        health = maxHealth;
    }    

    public void UseGasBomb()
    {

    }
    
    public void UseBattery()
    {
        charge = maxCharge;
    }

#endregion
}
