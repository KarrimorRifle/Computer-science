using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesReader : MonoBehaviour
{
    public GameObject Player;
    PlayerCombat com;
    TextMeshProUGUI lives;
    void Start()
    {
        lives = GetComponentInChildren<TextMeshProUGUI>();
        com = Player.GetComponent<PlayerCombat>();
    }
    void Update()
    {
        lives.text = "Extra Lives: " + com.extraLives.ToString();
    }
}
