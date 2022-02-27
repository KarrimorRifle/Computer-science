using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpGraphics : MonoBehaviour
{
    public float rotation = 10f;//speed at which the object rotates
    //all the sprites for each power up
    public Sprite ghost;
    public Sprite smg;
    public Sprite invincible;
    public Sprite ammo;
    SpriteRenderer sp;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();//gets the sprite renderer
    }
    void Update()
    {
        transform.Rotate(0,rotation * .1f,0);//rotating the icon
    }
    public void setsprite(string spriteCode)
    { //function is used to set the sprite
        if(!(sp.sprite == ghost || sp.sprite == smg || sp.sprite == invincible)) 
        //makes sure the power up doesnt already have a sprite
            switch(spriteCode)
            {//switch is used for this
                case "ghost"://if sprite code is any of these cases the sprite
                //is changed to the sprite variable (set in the unity API)
                    sp.sprite = ghost;
                    break;
                case "smg":
                    sp.sprite = smg;
                    break;
                case "invincible":
                    sp.sprite = invincible;
                    break;
                case "ammo":
                    sp.sprite = ammo;
                    break;

            }
    }
}
