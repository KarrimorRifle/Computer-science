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
    {
        levelManager = FindObjectOfType<LevelManager>();
        if( levelManager != null)
            if(levelManager.levelLimit < levelLimit)
                levelManager.levelLimit = levelLimit;
    }
    bool saved = false; //sees if there has already been data saved
    public void SavePlayerState()
    {
        player = FindObjectOfType<PlayerCombat>();
        gun = FindObjectOfType<GunController>();
        ammoCount = gun.ammoCount;
        magCount = gun.magCount;
        extraLives = player.extraLives;
        pow = player.pow;
        saved = true;

    }
    public void LoadPlayerState(GameObject playerLoad)
    {
        if(saved)
        {
            PlayerCombat player = playerLoad.GetComponent<PlayerCombat>();
            GunController gun = playerLoad.GetComponentInChildren<GunController>();
            gun.ammoCount = ammoCount;
            gun.magCount = magCount;
            player.extraLives = extraLives;
            player.pow = pow;
        }
    }

}
