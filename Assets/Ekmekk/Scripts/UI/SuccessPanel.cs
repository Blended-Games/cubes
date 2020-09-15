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
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI gainCoin;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private TextMeshProUGUI successText;

    private string[] successes = {"Good Job!", "Fabolous!", "Excellent!"};

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameEnd2 += (isWin) =>
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

        time.text = Convert.ToInt16(gameManager.elapsedTime).ToString() + " seconds";
        percent.text = "%" + CalculatePercent();

        int index = Random.Range(0, successes.Length);
        successText.text = successes[index];

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