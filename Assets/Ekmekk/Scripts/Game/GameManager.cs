using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MainCube[] mainCubes;
    private Target[] targetList;

    private bool isGameEnd;

    public float elapsedTime;

    public Action<bool> OnGameEnd;

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

    private void CheckGame()
    {
        if (isGameEnd)
            return;

        foreach (MainCube cube in mainCubes)
        {
            if (!cube.isMovementDone)
                return;
        }

        foreach (Target target in targetList)
        {
            if (target.isEmpty)
            {
                Lose();
                return;
            }
        }

        Win();
    }

    private void Update()
    {
        if (isGameEnd)
            return;

        elapsedTime += Time.deltaTime;
    }

    void Win()
    {
        isGameEnd = true;
        OnGameEnd?.Invoke(true);
    }

    void Lose()
    {
        isGameEnd = true;
        OnGameEnd?.Invoke(false);
    }
}