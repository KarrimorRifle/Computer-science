using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    GameObject levelSelector;
    GameObject mainMenu;
    GameObject skinSelector;
    GameObject settings;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {//gets the canvas menus
            switch(transform.GetChild(i).name)
            {
                case "Level Selection":
                    levelSelector = transform.GetChild(i).gameObject;
                    break;
                case "MainMenu":
                    mainMenu = transform.GetChild(i).gameObject;
                    break;
                case "Skin Selection":
                    skinSelector = transform.GetChild(i).gameObject;
                    break;
                case "Settings":
                    settings = transform.GetChild(i).gameObject;
                    break;
                
            }
        }
        MainMenu();
    }
    public void LevelMenu()
    {//enabling the level menu
        levelSelector.SetActive(true);
        mainMenu.SetActive(false);
        skinSelector.SetActive(false);
        settings.SetActive(false);
    }
    public void MainMenu()
    {//enabling main menu
        levelSelector.SetActive(false);
        mainMenu.SetActive(true);
        skinSelector.SetActive(false);
        settings.SetActive(false);
    }
    public void SettingsMenu()
    {//enabling settings
        levelSelector.SetActive(false);
        mainMenu.SetActive(false);
        skinSelector.SetActive(false);
        settings.SetActive(true);
    }
    public void SkinMenu()
    {//enabling skin menu
        levelSelector.SetActive(false);
        mainMenu.SetActive(false);
        skinSelector.SetActive(true);
        settings.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
