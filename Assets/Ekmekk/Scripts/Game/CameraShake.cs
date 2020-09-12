using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [SerializeField] private float duration, strength;
    [SerializeField] private int vibrato;
    [SerializeField] private bool snapping, fadeOut;

    private void Awake()
    {
        instance = this;
    }

    public void Shake()
    {
        transform.DOShakePosition(duration, strength, vibrato, 90, snapping, fadeOut);
    }
}