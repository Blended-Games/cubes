using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private MainCube[] mainCubes;
    private Target[] targetList;

    private bool isGameEnd;

    public float elapsedTime;

    public Action<bool> OnGameEnd2;
    public Action OnGameEnd1;
    public Action OnWin;

    string sceneName="";
    private void Awake()
    {
        elapsedTime = 0;

        targetList = FindObjectsOfType<Target>();
        mainCubes = FindObjectsOfType<MainCube>();

        foreach (MainCube mainCube in mainCubes)
        {
            mainCube.OnMovementTotalyDone += CheckGame;
        }
    }

    private void Start()
    {
      sceneName = SceneManager.GetActiveScene().name;
    }
    private void CheckGame()
    {
        if (isGameEnd)
            return;
    
        foreach (MainCube cube in mainCubes)
        {
            if (!cube.isMovementDone)
                return;
        }

        isGameEnd = true;
        OnGameEnd1?.Invoke();

        foreach (Target target in targetList)
        {
            if (target.isEmpty)
            {
                StartCoroutine(Lose());
                return;
            }
        }

        StartCoroutine(Win());
    }

    private void Update()
    {
        if (isGameEnd)
            return;

        elapsedTime += Time.deltaTime;
    }

    IEnumerator Win()
    {
        OnWin?.Invoke();
        yield return new WaitForSeconds(2);
        
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        
        PlayerPrefs.SetFloat("Coin", PlayerPrefs.GetFloat("Coin", 0) + 100);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + sceneName);
        OnGameEnd2?.Invoke(true);
    }

    IEnumerator Lose()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level" +sceneName);
        yield return new WaitForSeconds(1);
        OnGameEnd2?.Invoke(false);

    }
}