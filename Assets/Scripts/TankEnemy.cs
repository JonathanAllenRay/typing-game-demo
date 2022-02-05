using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : Enemy
{

    public new delegate void enemyAttack(int attackDamage);
    public new static event enemyAttack attack;

    private void Start()
    {
        BaseSetup();
        maxHealth = 150;
        health = maxHealth;
        action = 0;
        actionChargePerSecond = 5;
        attackDamage = 30;
    }

    private void Update()
    {
        if (action >= actionMax)
        {
            if (nextAction == "attack" && attack != null)
            {
                AttackPlayer();
            }
            action = 0;
        }

        if (tookDamage && action > 90)
        {
            action -= 25;
        }

        BaseUpdate();
    }

}
