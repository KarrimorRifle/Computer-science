using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    public GameObject Player;//refered / filled in API
    Slider slide; //refers to the slider
    void Start()
    {  
        slide = gameObject.GetComponent<Slider>();//getting the slider component
        slide.maxValue = Player.GetComponent<PlayerMovement>().MaxStamina;
        //sets the max value for the slider
    }
    void Update()
    {
        //sets the value of the slider every frame
        slide.value = Player.GetComponent<PlayerMovement>().Stamina;
    }
}
