using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveEffect : MonoBehaviour
{
    private Vector3 lastPos;
    private Vector3 currentPos;
    private Vector3 velocity;

    [SerializeField] private float rotateDuration = 0.35f;
    [SerializeField] private float maxRotation = 30;
    [SerializeField] private float rotationFactor = 0.8f;

    public bool isEffectOn = true;

    private void Start()
    {
        currentPos = lastPos = transform.position;
    }

    private void Update()
    {
        if (!isEffectOn)
            return;

        GetSpeed();

        ClampRotate();
        transform.DORotate(new Vector3(velocity.z * rotationFactor, 0, velocity.x * -1 * rotationFactor),
            rotateDuration);
    }

    void ClampRotate()
    {
        float clampValue = maxRotation / rotationFactor;
        float x = velocity.x;
        x = Mathf.Clamp(x, -1 * clampValue, clampValue);
        velocity.x = x;

        float z = velocity.z;
        z = Mathf.Clamp(z, -1 * clampValue, clampValue);
        velocity.z = z;
    }

    void GetSpeed()
    {
        currentPos = transform.position;
        velocity = (currentPos - lastPos) / Time.deltaTime;
        lastPos = transform.position;
    }
}