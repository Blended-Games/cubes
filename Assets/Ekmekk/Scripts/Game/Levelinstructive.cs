using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Levelinstructive : MonoBehaviour
{
    [SerializeField] private Image hand;
    [SerializeField] private RectTransform handEndPos;
    public static Levelinstructive Instante;
    private Tween t;

    void Awake()
    {
        Instante = this;

        if (PlayerPrefs.GetInt("currentLevel",1) == 1)
            Instructive();
    }

    public void Instructive()
    {
        hand.gameObject.SetActive(true);
        t = hand.rectTransform.DOMoveY(handEndPos.position.y, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            t.Kill();
            hand.gameObject.SetActive(false);
        }
    }
}