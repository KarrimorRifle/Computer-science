using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClear : MonoBehaviour
{
    public int levelUnlocked = 2;
    GameSaveManager save;
    ScenesManager scene;
    void Start()
    {
        save = FindObjectOfType<GameSaveManager>();
        scene = FindObjectOfType<ScenesManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            if(save.levelLimit < levelUnlocked)
                save.levelLimit = levelUnlocked;//sets new level
            scene.LoadMenu();//loads menu
        }
    }
}
