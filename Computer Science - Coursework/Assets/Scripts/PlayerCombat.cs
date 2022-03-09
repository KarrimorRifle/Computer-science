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
    void Awake()
    {
        FindObjectOfType<GameSaveManager>().LoadPlayerState(this.gameObject);
    }
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
            if((Time.time - powStartTime > powLength) || immunity)
            {
                Physics2D.IgnoreLayerCollision(3,7,immunity);//ignores the layercollisions if the player is immune
                if(immunity)//making the player transparent if theyre immune
                {
                    sprite.color = new Color(1,1,1,0.5f);
                }else{
                    sprite.color = new Color(1,1,1,1);
                }
            }
            //death check
            if(dead && (Time.time - deathTime) > animationDeathLength)
            {
                //reset entire level
            }
        }
        if(Input.GetKeyDown("f"))//tests if the button f has been ran
            powFunc(pow);
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
            Destroy(gameObject,animationDeathLength);
            dead = true;
        }

    }
    public void takeDamage(float damage, Vector2 damageSource, float knockbackForce)
    {
        Debug.Log("combat: OW!");
        if(!immunity || !invincible)//only applies dmg if player isnt immune
        {
            animator.SetTrigger("hurt");//sets trigger for getting hurt in animations
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
        }
        //damage animation
        
        // death
        if( health <= 0)
        {
            Debug.Log("Combat: health is below 0");
            die();
        }
        //knockback
            //calculating x & y travel
            Vector2 travel = (Vector2)transform.position- damageSource;
            //calculate hypotenuse
            float h = Mathf.Sqrt(Mathf.Pow(travel.x,2) + Mathf.Pow(travel.y, 2));
            //calculating force
            Vector2 force = (Vector2)((travel*knockbackForce)/h);
            //adding force
            rb.AddForce(force);
    }

    //power ups
    public string pow = "";
    public float powStartTime; //setting variables up for UI
    public float powLength;
    void powFunc(string power)//switch statement manages all the powers
    {
        if(Time.time - powStartTime > powLength)//stops the function from being ran again
        {//if the previous power up isnt over
            pow = ""; //resetting pow since string is already stored in power
            powStartTime = Time.time;
            switch(power)//switch calls the necessary function & manages the powUp usage
            {
                case "smg":
                    powLength = smgDuration;
                    StartCoroutine(powSMG());
                    break;
                case "ghost":
                    powLength = ghostDuration;
                    StartCoroutine(powGhost());
                    break;
                case "invincible":
                    powLength = invDuration;
                    StartCoroutine(powInvincible());
                    break;
            }
        }
    }
    //smg
    public float smgDuration = 3f;//duration of power up
    public float smgMultiplier = 1f;//how much faster fire rate should be
    IEnumerator powSMG()
    {
        float bps = gun.bps;//saving original speed
        gun.bps = bps * smgMultiplier;// multiplier for fire rate
        gun.smg = true;//making sure bullets arent consumed
        yield return new WaitForSeconds(smgDuration);
        gun.bps = bps; //returning original bps value
        gun.smg = false; //making sure bullets are consumed
    } 
    // ghosting
    public PlayerMovement movement;//referencing the movement script of player
    public float ghostMultiplier = 2.5f; //speed multiplier for ghost
    public float ghostDuration = 3f;
    IEnumerator powGhost()
    {//saving two original speeds
        
        float sp = movement.NormalSpeed;
        float ssp = movement.SprintSpeed;
        //makes speeds faster
        movement.Speed = sp * ghostMultiplier ;
        movement.NormalSpeed = sp * ghostMultiplier;
        movement.SprintSpeed = ssp * ghostMultiplier;
        Debug.Log("combat : OOOOOOooo");
        Physics2D.IgnoreLayerCollision(3,7,true);//ignores enemies
        sprite.color = new Color(.6f,.6f,1,0.5f);//making the player blue to be a ghost
        yield return new WaitForSeconds(ghostDuration);//waits
        //resets movement speed,collision & colour
        sprite.color = new Color(1,1,1,1);
        movement.NormalSpeed = sp; 
        movement.Speed = sp;
        movement.SprintSpeed = ssp;
        Physics2D.IgnoreLayerCollision(3,7,false);
    }

    public float invDuration = 5f;//invincibility duration
    bool invincible = false;
    IEnumerator powInvincible()
    {
        invincible = true;//making the player immune
        sprite.color = new Color(.6f,1,.6f,0.5f); //makes player green
        yield return new WaitForSeconds(invDuration);//waits
        invincible = false; //resets immunity
        sprite.color = new Color(1,1,1,1); //changes colour back
    }

}

