using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamePanel : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI level;

    void Awake()
    {
        Init();

        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameEnd1 += () => { panel.SetActive(false); };
    }

    void Init()
    {
        panel.SetActive(true);

        PlayerPrefs.SetFloat("Coin", PlayerPrefs.GetFloat("Coin", 0) + 100);

        coin.text = PlayerPrefs.GetFloat("Coin", 0).ToString();
        level.text = "Level " + PlayerPrefs.GetInt("currentLevel", 1);
    }
}