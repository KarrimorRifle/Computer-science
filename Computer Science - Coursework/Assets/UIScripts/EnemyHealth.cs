using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    Slider sl;
    EnemyCombat enemy;
    void Start(){
        sl = GetComponentInChildren<Slider>();
        enemy = GetComponentInParent<EnemyCombat>();
        sl.maxValue = enemy.maxHealth;
    }
    void Update()
    {
        sl.value = enemy.health;
    }

}
