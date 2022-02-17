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
    Vector2 mousePos;
    //shooting
    public float bps = 3f; //bullets per second
    public float lastFired = 0;
    void Start()
    {
        magCount = magMax;
        Physics2D.IgnoreLayerCollision(3,8,true);
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
        if(turret && Input.GetMouseButton(0))
        {

            shoot();
        }
        if(turret && Input.GetButtonDown("r")){
            StartCoroutine(reload());
            Debug.Log("Combat : reloading!");
        }
    }
    public GameObject bulletPrefab;
    void shoot()
    {
        if((magCount > 0) && (1/bps < (Time.time - lastFired)))
        {
            magCount --;
            lastFired = Time.time;
            GameObject bullet = Instantiate(bulletPrefab,shootLocation.position,shootLocation.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); //referencing the rigidbody
            Transform bTrans = bullet.GetComponent<Transform>();
            bTrans.Rotate(0,0,-90);
            bullet.GetComponent<Bullet>().bKnockBack = gunKnockback;
            bullet.GetComponent<Bullet>().bDamage = gunDamage;
            rb.AddForce(((mousePos - (Vector2)shootLocation.position).normalized) * bulletVelocity, ForceMode2D.Impulse); //adding force to the rigidbody
        }else if(magCount == 0)
            Debug.Log("Combat: No ammo");
    }
    public IEnumerator reload()
    {
        //Debug.Log("Combat : reloading 0");
        if(magCount < magMax)
        {
            //Debug.Log("Combat : reloading 1");
            if(ammoCount >= magMax) //checks if there is enough ammo
            {
                //Debug.Log("Combat : reloading 2");
                reloading = true;
                yield return new WaitForSeconds(reloadLength);
                magCount = magMax;
                ammoCount -= magMax;
                reloading = false;
            }else if(ammoCount > 0){//in case there isnt enough ammo
                //Debug.Log("Combat : reloading 3");
                reloading = true;
                yield return new WaitForSeconds(reloadLength);
                magCount = ammoCount;
                ammoCount = 0;
                reloading = false;
            }
        }
   }
}
