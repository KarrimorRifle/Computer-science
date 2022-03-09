using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public int level;
    Image img;
    public bool locked = true;
    void Start()
    {
        img = gameObject.GetComponent<Image>();
    }
    void Update()
    {
        if (level <= gameObject.GetComponentInParent<LevelManager>().levelLimit)
        {
            img.sprite = unlockedSprite;
            locked = false;
        }
        else
        {
            img.sprite = lockedSprite;
            locked = true;
        }
    }
}
