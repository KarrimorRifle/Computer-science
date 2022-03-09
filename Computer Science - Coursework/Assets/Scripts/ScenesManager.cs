using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager scenes;
    GameSaveManager save;
    private void Awake()
    {
        if(scenes == null)
            scenes = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);//allows this object to persist
    }
    void Start()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadMenu()
    {
        save = FindObjectOfType<GameSaveManager>();
        save.SavePlayerState();
        SceneManager.LoadScene("Menu");
    }
    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level " + levelNumber);
        save = FindObjectOfType<GameSaveManager>();
    }
}
