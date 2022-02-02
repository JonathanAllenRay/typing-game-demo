using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemy : Enemy
{

    public new delegate void enemyAttack(int attackDamage);
    public new static event enemyAttack attack;

    public delegate void enemyHealAllies(int amount);
    public static event enemyHealAllies healAllies;

    private void Start()
    {
        BaseSetup();
        maxHealth = 70;
        health = maxHealth;
        action = 0;
        actionChargePerSecond = 20;
        attackDamage = 5;
    }

    private void Update()
    {
        if (action >= actionMax)
        {
            if (nextAction == "attack" && attack != null)
            {
                AttackPlayer();
                nextAction = "heal";
            }
            else if (nextAction == "heal")
            {
                HealTeam();
                nextAction = "attack";
            }
            action = 0;
        }
        BaseUpdate();
    }

    private void HealTeam()
    {
        healAllies(25);
    }

}
