using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    //ui canvases set in the API
    public GameObject mainUI;
    public GameObject pauseMenuUI;
    void Update(){
        //tests if the escape key has been pressed
        if( Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused) //if the game is already paused
            //game will be unpaused
                Resume();
            else//if the game isnt paused
                Pause();//game is paused
        }
    }

    public void Resume()//public so it can be accessed by buttons
    {
        pauseMenuUI.SetActive(false);//turns off the pause UI
        mainUI.SetActive(true);//turns on the mainUI
        Time.timeScale = 1f;//makes time run
        GameIsPaused = false;//setting gamePaused Variable
    }

    public void Pause()//public so it can be accessed by buttons
    {
        pauseMenuUI.SetActive(true);//turns on the pause UI
        mainUI.SetActive(false);//turns off the main menu
        Time.timeScale = 0f;//turns off time
        GameIsPaused = true;//setting game paused to true
    }
    //functions to be created
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<ScenesManager>().LoadMenu();
        //loads menu scene which hasnt been created.
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
