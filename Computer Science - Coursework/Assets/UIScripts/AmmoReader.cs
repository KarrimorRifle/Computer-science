using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoReader : MonoBehaviour
{
    public GameObject player;
    TextMeshProUGUI ammo;
    GunController gun;
    Slider sl;
    Image img;//slider image
    void Start(){
        //gets the gun controller
        gun = player.GetComponentInChildren<GunController>();
        //gets the text handler
        ammo = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //gets image from the third chils'd children
        img = gameObject.transform.GetChild(2).GetComponentInChildren<Image>();
        //gets the slider component in children
        sl = gameObject.GetComponentInChildren<Slider>();
        sl.maxValue = gun.reloadLength;
    }
    void Update()
    {
        //changes ammo text
        ammo.text = (gun.magCount.ToString() + "|" + gun.ammoCount.ToString()).ToString();
        //turns on or off the bar
        img.enabled = gun.reloading;
        //only changing value if in the middle of a reload
        if(Time.time - gun.reloadStartTime < gun.reloadLength)
        //setting the value
            sl.value = Time.time - gun.reloadStartTime;
    }
}