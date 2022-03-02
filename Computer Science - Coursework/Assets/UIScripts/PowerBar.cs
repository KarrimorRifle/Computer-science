using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerBar : MonoBehaviour
{//basic variables most are private as they
//can be accessed directly
    public GameObject player;
    //set in unity's API
    Slider sl;
    PlayerCombat combat;
    Image filler;
    Image border;
    void Start()//called at beginning
    {//setting variables to be accessed quicker
        sl = gameObject.GetComponent<Slider>();
        combat = player.GetComponent<PlayerCombat>();
        for (int i = 0; i < transform.childCount; i++)
        { //getting the images for the power timer
            switch(transform.GetChild(i).name)
            {
                case "Filler":
                    filler = transform.GetChild(i).GetComponent<Image>();
                    break;
                case "Border":
                    border = transform.GetChild(i).GetComponent<Image>();
                    break;
            }
        }

    }
    void Update()
    {//called once per frame
        if(Time.time - combat.powStartTime < combat.powLength)
        {// if a power is active the bar is enabled
        //sets the border and fill colour to match the player's power colour
            filler.color = player.GetComponent<SpriteRenderer>().color;
            border.color = player.GetComponent<SpriteRenderer>().color;
            filler.enabled = true;
            border.enabled = true;
            sl.maxValue = combat.powLength;
            //this is done so the bar will go down instead of filling up
            sl.value = combat.powLength - (Time.time - combat.powStartTime);
        }else{//when no power is active bar is disabled
            filler.enabled = false;
            border.enabled = false;
        }

        

    }
}
