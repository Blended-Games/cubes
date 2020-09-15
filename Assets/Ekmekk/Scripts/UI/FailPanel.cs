using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailPanel : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject panel;
    [SerializeField] private Button retry;
    [SerializeField] private TextMeshProUGUI coin;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI time;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.OnGameEnd2 += (isWin) =>
        {
            if (!isWin)
            {
                Init();
            }
        };
    }


    void Init()
    {
        panel.SetActive(true);
        time.text = Convert.ToInt16(gameManager.elapsedTime).ToString() + " seconds";
        retry.onClick.AddListener(() => { LevelManager.instance.RetryLevel(); });
    }
}