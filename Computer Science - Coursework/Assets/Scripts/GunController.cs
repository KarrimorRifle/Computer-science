using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public Transform shootLocation;
    public Transform gun;
    public float gunDamage = 3;
    public float gunKnockback = 100;
    public float bulletVelocity = 5;
    int magCount;
    public int ammoCount = 15;
    public int magMax = 10;
    //reloading
    public bool reloading = false;
    public bool turret = false;
    public float reloadLength = 2.5f;
    float reloadTime;
    Vector2 mousePos;
    void Start()
    {
        magCount = magMax;
    }
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //finds the screen location of the mouse
        Vector2 lookDir = mousePos - (Vector2)gun.position; //finds the direction vector from the pivot point to the mouse
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; //calculates the angle of the vector
        if((angle > 90) || (angle < -90)) //if the mouse is on the left side different set of rotation is used
            transform.rotation = Quaternion.Euler(0, 180, 180 - angle); //arm is on left side, math is used to fix rotation
        else
            transform.rotation = Quaternion.Euler(0, 0,angle); //arm is rotated for right side
    }
    void Update()
    {

        if(Input.GetMouseButton(1))
            turret = true;
        else
            turret = false;
        anim.SetBool("showing",turret);
    }
    public IEnumerator reload()
    {
        if(magCount < magMax)
        {
            if(ammoCount >= magMax) //checks if there is enough ammo
            {
                reloading = true;
                yield return new WaitForSeconds(reloadLength);
                magCount = magMax;
                ammoCount -= magMax;
            }else if(ammoCount > 0){//in case there isnt enough ammo
                reloading = true;
                yield return new WaitForSeconds(reloadLength);
                magCount = ammoCount;
                ammoCount = 0;
            }
        }
   }
}
