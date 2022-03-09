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
        slide.maxValue = Player.GetComponent<CombatClass>().maxHealth;
        
    }
    void Update()
    {
        slide.value = Player.GetComponent<CombatClass>().health;
    }
}
