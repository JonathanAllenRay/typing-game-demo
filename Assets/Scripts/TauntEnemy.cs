using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntEnemy : Enemy
{
    public new delegate void enemyAttack(int attackDamage);
    public new static event enemyAttack attack;

    public delegate void enemyTaunt();
    public static event enemyTaunt updateTaunt;

    private void Start()
    {
        BaseSetup();
        maxHealth = 65;
        health = maxHealth;
        action = 0;
        actionChargePerSecond = 15;
        attackDamage = 7;
    }

    private void Update()
    {
        if (action >= actionMax)
        {
            if (nextAction == "attack" && attack != null)
            {
                AttackPlayer();
                if (Random.Range(0, 2) >= 0 && !taunting)
                {
                    nextAction = "taunt";
                }
            }
            else
            {
                taunting = true;
                updateTaunt();
                enemyWord.text = "<i>" + enemyWord.text + "</i>";
                nextAction = "attack";
            }
            action = 0;
        }
        // technically this is partially redundant but idc this is just for a demo half this 
        // code including this is getting omegarefactored later
        if (health <= 0)
        {
            taunting = false;
            updateTaunt();
            Destroy(this.gameObject);
        }
        BaseUpdate();
    }

}
