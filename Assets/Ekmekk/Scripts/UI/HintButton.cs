﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    private Button button;
    private RectTransform rectTransform;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();

        button.onClick.AddListener(Hint);

        FindObjectOfType<GameManager>().OnGameEnd1 += () => { button.enabled = false; };
    }

    void Hint()
    {
        button.enabled = false;

        bool isHint = HintSystem.instance.Hint();

        if (isHint)
        {
            StartCoroutine(DisableHint());
        }
        else
        {
            rectTransform.DOShakePosition(1, 10, 100, 180).OnComplete(() => button.enabled = true);
        }
    }

    IEnumerator DisableHint()
    {
        image.enabled = false;

        yield return new WaitForSeconds(5);

        image.enabled = true;
        button.enabled = true;
    }
}