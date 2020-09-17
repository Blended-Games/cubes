using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
    }

    public void NextLevel()
    {
        int nextSceneIndex = 0;
        if (PlayerPrefs.GetInt("isGameEnd", 0) == 0)
        {
            nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings <= nextSceneIndex)
            {
                PlayerPrefs.SetInt("LastEndLevel", nextSceneIndex - 1);
                PlayerPrefs.SetInt("isGameEnd", 1);
            }
        }

        if (PlayerPrefs.GetInt("isGameEnd", 0) == 1)
        {
            nextSceneIndex = Random.Range(4, SceneManager.sceneCountInBuildSettings);
        }

        PlayerPrefs.SetInt("currentSceneIndex", nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}