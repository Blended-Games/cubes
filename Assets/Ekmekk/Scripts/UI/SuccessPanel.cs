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
    [SerializeField] private Image emojiImage;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI gainCoin;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private TextMeshProUGUI successText;
    [SerializeField] private Sprite[] emojis;

    private string[] successes = {"Good Job!", "Fabulous!", "Excellent!"};

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

        Debug.Log("Win");
        
        gainCoin.text = "100";

        time.text = Convert.ToInt16(gameManager.elapsedTime) + " seconds";
        percent.text = "%" + CalculatePercent();

        int successTextIndex = Random.Range(0, successes.Length);
        successText.text = successes[successTextIndex];

        int emojiIndex = Random.Range(0, emojis.Length);
        emojiImage.sprite = emojis[emojiIndex];

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