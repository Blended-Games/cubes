using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData instance;

    private void Awake()
    {
        instance = this;
    }

    public int bestTime;
    public int worseTime;
}


