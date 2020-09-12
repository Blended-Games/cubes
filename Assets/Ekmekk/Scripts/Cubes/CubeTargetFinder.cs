using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeTargetFinder : MonoBehaviour
{
    private List<Target> targets;

    private void Awake()
    {
        targets = new List<Target>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Target target = other.GetComponent<Target>();
            if (target.isEmpty)
                targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Target target = other.GetComponent<Target>();
            targets.Remove(target);
        }
    }

    public Target GetNearestTarget()
    {
        float minDistance = Mathf.Infinity;
        Target nearTarget = null;

        foreach (Target target in targets)
        {
            float distance = (target.pos - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearTarget = target;
            }
        }

        return nearTarget;
    }
}