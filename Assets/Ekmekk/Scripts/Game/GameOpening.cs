using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOpening : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.GetInt("isGameEnd", 0) == 1)
        {
            if (PlayerPrefs.GetInt("LastEndLevel") < SceneManager.sceneCountInBuildSettings)
            {
                PlayerPrefs.SetInt("isGameEnd", 0);
            }
        }

        int index = PlayerPrefs.GetInt("currentSceneIndex", 1);
        SceneManager.LoadScene(index);
    }
}