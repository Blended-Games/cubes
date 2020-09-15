using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleEffect : MonoBehaviour
{
    [SerializeField] private GameObject particleObj;

    private void Awake()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.OnWin = () => { particleObj.SetActive(true); };
    }
}