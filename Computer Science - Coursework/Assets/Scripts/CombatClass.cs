using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatClass : MonoBehaviour
{
    
    public GameObject self;
    public Animator animator;

    public float shield;
    public int maxHealth = 50;
    public float health;
    public Rigidbody2D rb;
    
    public LayerMask targetLayers;
    //attack aspects
        public float attackSpeed = 0.5f;
        public float attackKnockback = 500f;
        public int attackDamage = 1;
        public Transform attackPoint;
        public float attackRange;
    //death / things to disable upon death
        public SpriteRenderer sprite;
        float lastAttack = 0;
        public float deathTime = 0;
    //animation clip times
    private AnimationClip clip;
    public float animationAttackLength; //contains the amount of time to run the attack length
    public float animationDeathLength; //contains the amount of time to run the death animation

    
    void Start()
    {
        health = maxHealth;
        updateAnimClipTimes();
        animator.SetFloat("attackSpeed",animationAttackLength * 5);
    }
    
    public void takeDamage(float damage, Vector2 damageSource, float knockbackForce)
    {
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
    public void Attack() //a function to deal damage
    {
        
        if( (Time.time - lastAttack) > (1 / attackSpeed))
        {
            lastAttack = Time.time;//sets the time of the latest attack
            //attack animation
            animator.SetTrigger("attack");

            //detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, targetLayers); //uses the previous variables to construct 
            //a sphere around the point and collects all enemies within the circle
            bool playerHit = false;
            //apply dmg
            foreach(Collider2D enemy in hitEnemies) //as can be read, for every enemy in the "hitEnemies" array they will be printed in the debug
            {
                Debug.Log("Combat: " + enemy.name + " was hit"); //debug function to test
                if(enemy.name == "Player" && !playerHit)
                {
                    enemy.GetComponent<PlayerCombat>().takeDamage(attackDamage,self.transform.position, attackKnockback);
                    playerHit = true;
                }   
                else
                    enemy.GetComponent<CombatClass>().takeDamage(attackDamage,self.transform.position, attackKnockback);
            }
        }
    }
    public void die()
    {
        //death aimation
        animator.SetBool("dead",true);//triggers death animation

        //disable character
        //the character is visually disabled and this script will stop running so no damage can be done to the player
        deathTime = Time.time;
        GetComponent<Collider2D>().enabled = false; //disables collider
        //this.enabled = false;// disables the combat script

    }
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); //allows player to see where the attack sphere is
    }
    public void updateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            Debug.Log("ANIMATION: Name: " + clip.name +" Time: " + System.Convert.ToString(clip.length));
            switch(clip.name)
            {
                case "slime_attack":
                    animationAttackLength = clip.length;
                    break;
                case "slime_death":
                    animationDeathLength = clip.length;
                    break;
                case "attack_sword":
                animationAttackLength = clip.length;
                    break;
                case "player_death":
                    animationDeathLength = clip.length;
                    break;
            }
        }
    }

}
