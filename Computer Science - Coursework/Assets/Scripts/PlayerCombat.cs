 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CombatClass
{
    public int extraLives = 3;
    bool immunity = false;
    public float immunityLength = 2.5f;
    float immunityTime;
    bool dead;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space)) //detects if spacebar is pressed on
        {
            Attack(); //calling the attack function
        }
        //removing immunity if time is up
        if((Time.time - immunityTime) >= immunityLength)
        {
            immunity = false; //resseting immunity if time is up
        }
        Physics2D.IgnoreLayerCollision(3,7,immunity);//ignores the layercollisions if the player is immune
        if(immunity)//making the player transparent if theyre immune
        {
            sprite.color = new Color(1,1,1,0.7f);
        }else{
            sprite.color = new Color(1,1,1,1);
        }

        if(dead && (Time.time - deathTime) > animationDeathLength)
        {
            //reset entire level
        }
    }
    public void die()//overiding death
    {
        
        //testing if the player has extra lives
        if(extraLives >= 0)
        {
            extraLives -= 1;//removing the extra lives
            //resetting health
            health = maxHealth;
            immunity = true; //giving immunity to the player
            immunityTime = Time.time;
        }else{
            animator.SetBool("dead",true);//triggers death animation
            deathTime = Time.time;
            dead = true;
        }

    }
    
}
