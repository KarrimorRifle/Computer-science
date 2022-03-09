using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CombatClass
{
    public Transform target;
    void Start()
    {
        target = GameObject.Find("Player").transform.Find("PlayerFollow");
        health = maxHealth;
        updateAnimClipTimes();
        animator.SetFloat("attackSpeed",animationAttackLength * 5);
    }
    void Update()
    {
        //will use the attack function when the player is in range
        if(Vector2.Distance(target.position,attackPoint.position) <= attackRange)
        {
            Attack();
        }
    }
}
