using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    //defining  variables
    public int levelLimit = 1;//current level limit
    List<GameObject> vectors;//list of waypoints
    //List<List<Vector2>> vector2s; //old 2d list
    public int position = 0;//position of the player
    public GameObject player;//references the player
    (int,int)[,] vector2s = new (int,int)[9,9]{ //2D array containing movement vectors
        {(0,0),(0,-1),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)},
        {(0,1),(0,0),(1,0),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)},
        {(0,0),(-1,0),(0,0),(0,1),(1,0),(0,0),(0,0),(0,0),(0,0)},
        {(0,0),(0,0),(0,-1),(0,0),(0,0),(0,0),(0,0),(0,0),(0,0)},
        {(0,0),(0,0),(-1,0),(0,0),(0,0),(1,0),(0,0),(0,0),(0,0)},
        {(0,0),(0,0),(0,0),(0,0),(-1,0),(0,0),(0,-1),(1,0),(0,0)},
        {(0,0),(0,0),(0,0),(0,0),(0,0),(0,1),(0,0),(0,0),(1,0)},
        {(0,0),(0,0),(0,0),(0,0),(0,0),(-1,0),(0,0),(0,0),(0,-1)},
        {(0,0),(0,0),(0,0),(0,0),(0,0),(0,0),(-1,0),(0,1),(0,0)}
        };
    void Start()
    {
        //instantiates the list
        vectors = new List<GameObject>();
        
        for (int i = 0; i < transform.childCount; i++)
        {//for loop for every child in level manager's game object
            //Debug.Log(transform.GetChild(i).gameObject.name);
            if(transform.GetChild(i).gameObject.name == "Waypoints")
            {
            //if object containing the waypoints is found
                for (int x = 0; x < transform.GetChild(i).childCount; x++)
                {//each waypoint is added to the vectors list
                    vectors.Add(transform.GetChild(i).GetChild(x).gameObject);
                    //Debug.Log(transform.GetChild(i).GetChild(x).GetComponent<RectTransform>().position + transform.GetChild(i).GetChild(x).gameObject.name);  
                }    
            }    
            //gets the pplayer gameobject    
            if(transform.GetChild(i).gameObject.name == "Player")
                player = transform.GetChild(i).gameObject;
        }
        // //setting (1,0) direction//(2,1), (4,2), (5,4), (7,5), (8,6)
        //     vector2s[2,1] = (1,0);
        //     vector2s[4,2] = (1,0);
        //     vector2s[5,4] = (1,0);
        //     vector2s[7,5] = (1,0);
        //     vector2s[8,6] = (1,0);
        // //setting (-1,0) direction//(1,2), (2,4), (4,5), (5,7), (6,8)
        //     vector2s[1,2] = (-1,0);
        //     vector2s[2,4] = (-1,0);
        //     vector2s[4,5] = (-1,0);
        //     vector2s[5,7] = (-1,0);
        //     vector2s[6,8] = (-1,0);
        // //setting (0,1) direction//(0,1) , (3,2), (5,6), (7,8)
        //     vector2s[0,1] = (0,1);
        //     vector2s[3,2] = (0,1);
        //     vector2s[5,6] = (0,1);
        //     vector2s[7,8] = (0,1);
        // //setting 0,-1) direction
        //     vector2s[1,0] = (0,-1);
        //     vector2s[2,3] = (0,-1);
        //     vector2s[6,5] = (0,-1);
        //     vector2s[8,7] = (0,-1);
        // //(1,0), (2,3), (6,5) , (8,7)
    }
    public float lastMovedTime;
    public float moveCooldown = 0.3f;
    public ScenesManager scene;
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal").ToString() + Input.GetAxisRaw("Vertical").ToString());
        //tests if movement buttons are pressed and if the player has already been moved
        if((Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) != (0,0) && (Time.time - lastMovedTime >= moveCooldown))
        {
            for(int i = 0; i < 9; i++)
            {//runs through every object inside the row of the position of player
                // Debug.Log(position.ToString() + i);
                // Debug.Log(vector2s[position,i]);
                if(vector2s[position,i] == (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) && !vectors[i].GetComponent<Waypoint>().locked)
                {//if the vector in the row matches up with the direction vector & the vector is unlocked
                //player is moved to the new waypoint
                    lastMovedTime = Time.time;
                    position = i;
                    player.GetComponent<RectTransform>().position = vectors[i].GetComponent<RectTransform>().position - new Vector3(0,30,0);
                    break;
                }
            }
        }
        if(vectors[position].GetComponent<Waypoint>().level > levelLimit)
            position = levelLimit - 1;
            player.GetComponent<RectTransform>().position = vectors[position].GetComponent<RectTransform>().position - new Vector3(0,30,0);
        if(Input.GetKeyDown("space"))
         scene.LoadLevel(position + 1);
    }

}
