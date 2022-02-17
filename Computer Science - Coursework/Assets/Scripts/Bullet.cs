using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bDamage;
    public float bKnockBack;
    public GameObject hitExplosion;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
            collision.gameObject.GetComponent<CombatClass>().takeDamage(bDamage,transform.position,bKnockBack);//calling the enemy's function
        GameObject explosion = Instantiate(hitExplosion, transform.position - new Vector3(0f,0.3f,0f), Quaternion.identity);
        Destroy(explosion,1f);//destroy explosion after 2.5 seconds
        Destroy(gameObject);//destroy itself
    }
}
