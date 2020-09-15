using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MainCube : MonoBehaviour, Clickable
{
    public int id;

    private CubeTargetFinder cubeTargetFinder;
    private CubeAnimations cubeAnimations;
    private CubeCloner cubeCloner;
    private CubeLooper cubeLooper;

    [SerializeField] private MeshRenderer[] clonableIndicator;
    private Vector3 startPos;
    private Target currentTarget;
    public bool isOnTarget;

    private Vector2 loopDir;

    public Action OnMovementTotalyDone;
    public Action OnMovementDone;
    public bool isMovementDone;
    public int movementCount;
    private int currentMovementCount;

    private void Awake()
    {
        OnMovementDone += () =>
        {
            currentMovementCount++;

            if (currentMovementCount >= movementCount)
            {
                isMovementDone = true;
                OnMovementTotalyDone?.Invoke();
            }
        };

        startPos = transform.position;

        cubeLooper = GetComponent<CubeLooper>();
        cubeCloner = GetComponent<CubeCloner>();
        cubeAnimations = GetComponent<CubeAnimations>();
        cubeTargetFinder = GetComponent<CubeTargetFinder>();
    }

    public void OnClickDown()
    {
        Reset();
    }

    public void Undo()
    {
        Reset();
        transform.DOJump(startPos, 10, 1, 0.5f);
    }

    public void OnClickUp()
    {
        Target target = cubeTargetFinder.GetNearestTarget();

        if (!target || !target.isEmpty)
        {
            transform.position = startPos;
        }
        else
        {
            UndoSystem.instance.Move(this);
            ClonableIndicatorActive(false);
            isOnTarget = true;
            currentTarget = target;
            currentTarget.Fill();
            if (currentTarget.direction == Vector2.zero)
            {
                cubeAnimations.SettleDownTarget(target, CreateClone);
            }
            else
            {
                loopDir = currentTarget.direction;
                cubeAnimations.SettleDownTarget(target, CreateLoop);
            }
        }
    }

    void CreateClone()
    {
        if (isOnTarget)
        {
            cubeCloner.Clone(() => { OnMovementDone?.Invoke(); });
        }
    }

    void CreateLoop()
    {
        movementCount++;
        cubeLooper.Loop(loopDir);
    }

    void ClonableIndicatorActive(bool isActive)
    {
        foreach (MeshRenderer indicator in clonableIndicator)
        {
            if (indicator != null)
                indicator.enabled = isActive;
        }
    }

    void Reset()
    {
        currentMovementCount = 0;
        movementCount = 0;
        isOnTarget = false;
        isMovementDone = false;

        ClonableIndicatorActive(true);

        if (currentTarget != null)
        {
            cubeCloner.DestroyAll();
            cubeLooper.DestroyAll();
            currentTarget.isEmpty = true;
            currentTarget = null;
        }
    }
}