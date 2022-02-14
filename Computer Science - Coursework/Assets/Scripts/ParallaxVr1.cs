using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxVr1 : MonoBehaviour
{   //variables used for code created here
    private float length,cameraOffset;
    public Camera cam;
    public Vector2 parallaxEffect;
    public Transform player;
    Vector2 cameraStart, startpos; //creates variable for camera position
    void Start()
    {
        startpos = transform.position; //was used in the tutorial but is unused now
        length = GetComponent<SpriteRenderer>().bounds.size.x; //finds the length of the sprite
        cameraOffset  = cam.transform.position.x - transform.position.x; //finds the difference between camera and sprite coordinates
        // Debug.Log("startpos is: " + startpos);
        // Debug.Log("length is: " + length);
        // Debug.Log("playerOffset is: " + playerOffset); //had to get some variables to know how to do things
        //the variables was used to find out how far i should place the backgrounds from each other
        cameraStart = cam.transform.position; // finds original camera position
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(startpos.x + (cam.transform.position.x - cameraStart.x) * parallaxEffect.x,startpos.y +(cam.transform.position.y - cameraStart.y) * parallaxEffect.y,0);// changes the position of the background
        //Debug.Log(player.position.x); //was used to find player position
        if (cam.transform.position.x > transform.position.x + cameraOffset + length){ //tests if camera is past a point on the right
            startpos.x += length;
        }
        else if (cam.transform.position.x < transform.position.x + cameraOffset - length){//tests if camera is past a point on the left
            startpos.x -= length;
        }
        
    }
}
