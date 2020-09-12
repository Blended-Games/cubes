using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SuccessPanel : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject panel;
    [SerializeField] private Button retry;
    [SerializeField] private Button next;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI gainCoin;
    [SerializeField] private TextMeshProUGUI percent;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameEnd += (isWin) =>
        {
            if (isWin)
            {
                Init();
            }
        };
    }

    void Init()
    {
        panel.SetActive(true);

        PlayerPrefs.SetFloat("Coin", PlayerPrefs.GetFloat("Coin", 0) + 100);
        gainCoin.text = "100";

        coin.text = PlayerPrefs.GetFloat("Coin", 0).ToString();
        level.text = "Level " + PlayerPrefs.GetInt("currentLevel", 1);
        time.text = Convert.ToInt16(gameManager.elapsedTime).ToString() + " seconds";
        percent.text = "%"+CalculatePercent();

        retry.onClick.AddListener(() => { LevelManager.instance.RetryLevel(); });
        next.onClick.AddListener(() => { LevelManager.instance.NextLevel(); });
    }

    int CalculatePercent()
    {
        int percent = 0;
        int elapsedTime = Convert.ToInt16(gameManager.elapsedTime);

        if (elapsedTime <= LevelData.instance.bestTime)
        {
            percent = Random.Range(95, 100);
        }
        else if (elapsedTime > LevelData.instance.bestTime && elapsedTime <= LevelData.instance.worseTime)
        {
            percent = Random.Range(85, 95);
        }
        else
        {
            percent = Random.Range(60, 85);
        }

        return percent;
    }
}