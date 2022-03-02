using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpIcon : MonoBehaviour
{
    //basic variables set in Unity API
    public GameObject Player;
    public Sprite smg;
    public Sprite ghost;
    public Sprite invincible;
    Image img;
    void Start()
    {
        //gets the image component
        img = gameObject.GetComponent<Image>();
    }
    void Update()
    {
        switch(Player.GetComponent<PlayerCombat>().pow)
        {
            case "smg":
            //if pow is smg icon is changed to smg and icon
            //is turned on repeated with seperate icons repsectively
                img.enabled = true;
                img.sprite = smg;
                break;
            case "ghost":
                img.enabled = true;
                img.sprite = ghost;
                break;
            case "invincible":
                img.enabled = true;
                img.sprite = invincible;
                break;
            case "":
            //icon is turned off if there is no power up
                img.enabled = false;
                break;
            
        }
    }
}
