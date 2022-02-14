using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aIPath; //public variable to reference the AI pathing algorithm

    // Update is called once per frame
    void Update()
    {
        if(aIPath.desiredVelocity.x >= 0.01f)//checks the velocity wanted, if greater than 0.01 will chnage the graphics to face right
        {
            transform.localScale = new Vector3(1f,1f,1f);
        } else if (aIPath.desiredVelocity.x <= -0.01f)//checks the velocity wanted, if less than 0.01 will chnage the graphics to face left
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // Debug.Log(aIPath.desiredVelocity.x); //debug code
        
    }
}
