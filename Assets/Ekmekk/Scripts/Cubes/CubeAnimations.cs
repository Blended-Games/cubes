using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeAnimations : MonoBehaviour
{
    private float settleDownSpeed = 8;
    private float cloneSpeed = 10;

    private Tween currentTween;

    public void SettleDownTarget(Target target, Action OnComplete)
    {
        Vector3 aboveTarget = new Vector3(target.pos.x, transform.position.y, target.pos.z);
        currentTween = transform.DOMove(aboveTarget, settleDownSpeed).SetSpeedBased().OnComplete(() =>
        {
            currentTween = transform.DOMove(target.pos, settleDownSpeed).SetSpeedBased()
                .OnComplete(() =>
                {
                    VibrationMTO.Instante.VibrateStandart();
                    OnComplete?.Invoke();
                });
        });
    }

    public void CloneMove(float x, float z, Action OnComplete)
    {
        Vector3 pos1 = transform.position;

        Vector3 target = new Vector3(pos1.x, pos1.y + 1.5f, pos1.z);
        currentTween = transform.DOMove(target, cloneSpeed).SetSpeedBased().OnComplete(() =>
        {
            Vector3 pos2 = transform.position;
            Vector3 target2 = new Vector3(pos2.x + (x * 4f), pos2.y, pos2.z + (z * 4f));

            currentTween = transform.DOMove(target2, cloneSpeed).SetSpeedBased().OnComplete(() => OnComplete?.Invoke());
        });
    }

    public void Loop(float x, float z, Action OnComplete)
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;
        Vector3 rotate = new Vector3(rot.x + (z * 180), rot.y, rot.z + (x * -180));
        Vector3 target = new Vector3(pos.x + (x * 4.1f), pos.y, pos.z + (z * 4.1f));
        GameObject temp = new GameObject();
        temp.transform.position = new Vector3(pos.x + (x * 4.1f * 0.5f), pos.y, pos.z + (z * 4.1f * 0.5f));
        transform.SetParent(temp.transform);
        currentTween = temp.transform.DORotate(rotate, cloneSpeed * 28).SetSpeedBased().OnComplete(() =>
        {
            VibrationMTO.Instante.VibrateStandart();
            transform.SetParent(null);
            Destroy(temp);
            OnComplete?.Invoke();
        });
    }

    public void AlignTarget(Target target, Action OnComplete)
    {
        currentTween = transform.DOMove(target.pos, settleDownSpeed).SetSpeedBased()
            .OnComplete(() => OnComplete?.Invoke());
    }

    public void JustGoDown()
    {
        Vector3 pos = transform.position;
        currentTween = transform.DOMove(new Vector3(pos.x, pos.y - 3, pos.z), settleDownSpeed).SetSpeedBased();
    }

    private void OnDestroy()
    {
        if (currentTween != null && currentTween.active)
        {
            currentTween.Kill();
        }
    }
}