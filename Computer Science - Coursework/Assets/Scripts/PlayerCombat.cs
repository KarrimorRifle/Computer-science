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
    public GunController gun;
    void Update()
    {
        if(!gun.turret){
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
                sprite.color = new Color(1,1,1,0.5f);
            }else{
                sprite.color = new Color(1,1,1,1);
            }
            //death check
            if(dead && (Time.time - deathTime) > animationDeathLength)
            {
                //reset entire level
            }
        }
    }
    public void die()//overiding death
    {
        Debug.Log("Combat: Die() has been called");
        
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
    public void takeDamage(float damage, Vector2 damageSource, float knockbackForce)
    {
        Debug.Log("combat: OW!");
        if(!immunity)//only applies dmg if player isnt immune
            if( shield > 0 && shield > damage)//taking damage with shield
            {
                shield -= damage;
            }else if(0 < shield && shield < damage)
            {
                float carryDamage = damage - shield;
                shield = 0;
                health -= carryDamage;
            }else{
                health -= damage;
            }
        //damage animation
        animator.SetTrigger("hurt");//sets trigger for getting hurt in animations
        // death
        if( health <= 0)
        {
            Debug.Log("Combat: health is below 0");
            die();
        }
        //knockback
            //calculating x & y travel
            Vector2 travel = (Vector2)self.transform.position- damageSource;
            //calculate hypotenuse
            float h = Mathf.Sqrt(Mathf.Pow(travel.x,2) + Mathf.Pow(travel.y, 2));
            //calculating force
            Vector2 force = (Vector2)((travel*knockbackForce)/h);
            //adding force
            rb.AddForce(force);
    }


}

