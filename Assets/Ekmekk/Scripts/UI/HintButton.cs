using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    private Button button;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(Hint);

        FindObjectOfType<GameManager>().OnGameEnd += (isWin) =>
        {
            gameObject.SetActive(false);
        };
    }

    void Hint()
    {
        HintSystem.instance.Hint();
        StartCoroutine(DisableHint());
    }

    IEnumerator DisableHint()
    {
        image.enabled = false;
        button.enabled = false;

        yield return new WaitForSeconds(5);

        image.enabled = true;
        button.enabled = true;
    }
}