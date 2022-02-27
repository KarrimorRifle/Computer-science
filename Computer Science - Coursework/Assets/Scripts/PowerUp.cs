using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string powerUp; //string to store power up name
    public int ammo;
    public powerUpGraphics powG;//set in API
    //acccesing the sibling's script directly
    void Start()
    {
        if((powerUp == null) || powerUp == "")
            Destroy(transform.parent.gameObject);
    }
    void Update()
    {
        powG.setsprite(powerUp);//sets the powerup
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with : " + collision.tag);
        if(collision.name == "Player" && collision.GetComponent<PlayerCombat>().pow == "" && powerUp != "ammo")
        {//makes sure the power up doesnt overide the current one
            collision.GetComponent<PlayerCombat>().pow = powerUp;//changes the player's power up
            Destroy(transform.parent.gameObject);//destroys parent
        }else if(powerUp == "ammo")
        {
            collision.GetComponentInChildren<GunController>().ammoCount += ammo;
            Destroy(transform.parent.gameObject);
        }
    }
}
