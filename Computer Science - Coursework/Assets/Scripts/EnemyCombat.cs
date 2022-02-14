using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CombatClass
{
    public Transform target;
    void Update()
    {
        //will use the attack function when the player is in range
        if(Vector2.Distance(target.position,attackPoint.position) <= attackRange)
        {
            Attack();
        }
        
        
        if((deathTime != 0) && (Time.time - deathTime) >= animationDeathLength)
        {
            sprite.enabled = false;
            this.enabled = false;
        }
    }
}
