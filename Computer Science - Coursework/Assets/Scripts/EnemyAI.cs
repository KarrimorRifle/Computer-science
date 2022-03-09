using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; //a library that is used for enemy path finding

public class EnemyAI : MonoBehaviour
{
    //variables
    public Transform target; //target location (will be who the AI follows) public to allow us to select he player
    public float speed = 200f; //the travel speed the AI can take
    public float nextWaypointDistance = 3f; //sets the distance at what each waypoint should be, is public so it can be changed in editer
    public float StopRange = 1f;
    public float MaxChaseRange = 10f;
    Path path; //a variable to store the path that is to be taken
    int currentWaypoint = 0; //sets the current waypoint the AI should be moving to
    bool reachedEndOfPath = false; //a boolean variable to determine if the AI should stop "following"
    Seeker seeker; //to reference the seeker script used to make paths
    Rigidbody2D rb; //referencing the Enemy's Rigid Body function
    
    

    // Start is called before the first frame update
    void Start()
    {   //gets the components already on the enemy
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("UpdatePath", 0f, .5f); //invokeRepeating causes a methof to repeat in the specified variables
        //variables for invoke : ( methos to be called, delay before it's called, time to repeat)
        target = GameObject.Find("Player").transform.Find("PlayerFollow");
    }
    void UpdatePath()
    {
        if(seeker.IsDone()) //making sure the method isnt already in use
            seeker.StartPath(rb.position, target.position, OnpathComplete); //creates a path using the A* graph nodes, first variable is the start point
        //second variable is the end point, the last one is a function that is called upon completion
    }
    void OnpathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;//sets path variable to current path (or path made by seeker at the beginning)
            currentWaypoint = 0; 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null) //tests if there is a valid path, if not it ust returns the value
            return;

        if(currentWaypoint >= path.vectorPath.Count) //checks if we are at the last waypoint
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; //gets the direction of the current waypoint and sets the Vector2 as 1
        
        Vector2 force = direction * speed * Time.deltaTime; //makes a force for which the Enemy will be moving delta time makes sure that the same amount will be moved
        //no matter how many cycles the CPU runs
        if(MaxChaseRange > Vector2.Distance(target.position,transform.position) && Vector2.Distance(target.position,transform.position) > StopRange)
        {
            rb.AddForce(force);
        }
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if ( distance < nextWaypointDistance) // checks if the distance left to travel is less than the current waypoint
        {
            currentWaypoint++; //changes to the next waypoint
        }

        if(force.x >= 0.01f)//checks the velocity wanted, if greater than 0.01 will chnage the graphics to face right
        {
            transform.localScale = new Vector3(1f,1f,1f);
        } else if (force.x <= -0.01f)//checks the velocity wanted, if less than 0.01 will chnage the graphics to face left
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        
    }
}
