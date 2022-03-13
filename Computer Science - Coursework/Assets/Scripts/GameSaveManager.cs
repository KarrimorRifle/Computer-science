using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public int levelLimit = 1;
    LevelManager levelManager;
    //I want to only store certain variables: extra lives, Power, Ammo & mag count
    public int ammoCount,magCount,extraLives;
    public string pow;
    PlayerCombat player;
    GunController gun;
    private void Awake()
    {
        if(gameSave == null)
            gameSave = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);//allows this object to persist
    }
    
    void Update()
    {//sets the level limit
        levelManager = FindObjectOfType<LevelManager>();
        if( levelManager != null)
            if(levelManager.levelLimit < levelLimit)
                levelManager.levelLimit = levelLimit;
    }
    bool saved = false; //sees if there has already been data saved
    public void SavePlayerState()
    {//called to save the player's variables
    //finds the player and gun objects
        player = FindObjectOfType<PlayerCombat>();
        gun = FindObjectOfType<GunController>();
        //stores variables within itself
        ammoCount = gun.ammoCount;
        magCount = gun.magCount;
        extraLives = player.extraLives;
        pow = player.pow;
        //boolean is used to know if data was stored
        saved = true;
        //if this doesnt happen all variables
        //in player wouldve been set to null or 0

    }
    public void LoadPlayerState(GameObject playerLoad)
    {
        if(saved)
        {//if there has been data saved if so data is reloaded into the player
            PlayerCombat player = playerLoad.GetComponent<PlayerCombat>();
            GunController gun = playerLoad.GetComponentInChildren<GunController>();
            gun.ammoCount = ammoCount;
            gun.magCount = magCount;
            player.extraLives = extraLives;
            player.pow = pow;
        }
    }

}
