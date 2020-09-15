using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
    private Button button;
    private RectTransform rectTransform;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();

        button.onClick.AddListener(Undo);

        FindObjectOfType<GameManager>().OnGameEnd1 += () => { button.enabled = false; };
    }

    void Undo()
    {
        button.enabled = false;

        bool isUndo = UndoSystem.instance.Undo();

        if (!isUndo)
        {
            rectTransform.DOShakePosition(1, 10, 100, 180).OnComplete(() => button.enabled = true);
        }
        else
        {
            button.enabled = true;
        }
    }
}