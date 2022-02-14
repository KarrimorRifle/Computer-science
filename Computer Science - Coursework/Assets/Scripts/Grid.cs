using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid{
    //creating basic variables
    private int width;
    private int height;
    private int[,] gridArray; //creates a 2d int array
    public grid(int width, int height)
    {
        this.width = width; //setting width and height
        this.height = height;
        
        gridArray = new int[width, height];
        //cycle through array
        for( int x = 0; x < gridArray.GetLength(0); x++) //checking the grid array is working
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                Debug.Log(x + "," + y);
                
            }
        }
    }
}

