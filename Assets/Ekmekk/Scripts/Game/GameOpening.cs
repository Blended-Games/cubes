using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOpening : MonoBehaviour
{
    void Awake()
    {
        int index = PlayerPrefs.GetInt("currentSceneIndex", 1);
        index = 13;
        SceneManager.LoadScene(index);
    }
}