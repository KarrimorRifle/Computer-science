using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    Slider slide;
    void Start()
    {  
        slide = gameObject.GetComponent<Slider>();
        slide.maxValue = Player.GetComponent<PlayerCombat>().maxHealth;
        
    }
    void Update()
    {
        slide.value = Player.GetComponent<PlayerCombat>().health;
    }
}
