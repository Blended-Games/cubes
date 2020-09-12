using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopCube : MonoBehaviour, Clickable
{
    private MainCube mainCube;
    private GameObject collisionMask;

    private CubeTargetFinder cubeTargetFinder;
    private CubeAnimations cubeAnimations;

    private Target currentTarget;

    private Vector2 loopDir;

    private bool isDestroyable;

    private void Awake()
    {
        cubeAnimations = GetComponent<CubeAnimations>();
        cubeTargetFinder = GetComponent<CubeTargetFinder>();
    }

    public void Init(Vector2 loopDir, MainCube mainCube, GameObject lastObject)
    {
        collisionMask = lastObject;
        this.mainCube = mainCube;
        mainCube.GetComponent<CubeLooper>().loopCubes.Add(this);
        cubeAnimations.Loop(loopDir.y, loopDir.x, ControlTarget);
    }

    void ControlTarget()
    {
        Target target = cubeTargetFinder.GetNearestTarget();

        if (!target || !target.isEmpty)
        {
            isDestroyable = true;
        }
        else
        {
            currentTarget = target;
            currentTarget.Fill();
            if (currentTarget.direction == Vector2.zero)
            {
                cubeAnimations.AlignTarget(target, () => { mainCube.OnMovementDone?.Invoke(); });
            }
            else
            {
                loopDir = currentTarget.direction;
                cubeAnimations.AlignTarget(target, CreateLoop);
            }
        }
    }

    public void SafeDestroy()
    {
        if (currentTarget != null)
        {
            currentTarget.isEmpty = true;
        }

        Destroy(this.gameObject);
    }

    public void OnClickDown()
    {
        FindObjectOfType<InputSystem>().SetForceClickable(mainCube.transform);
        mainCube.OnClickDown();
    }

    public void OnClickUp()
    {
    }

    void CreateLoop()
    {
        GameObject temp = Instantiate(this, transform.position, Quaternion.identity).gameObject;
        temp.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

        temp.GetComponent<LoopCube>().Init(loopDir, mainCube, this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDestroyable && other.CompareTag("Cube"))
        {
            if (other.gameObject == collisionMask)
                return;

            ParticleDestroy.Instante.ParticleSorting(transform.GetComponent<MeshRenderer>().material.name,
                transform.gameObject);
            Destroy(transform.gameObject);
            CameraShake.instance.Shake();
            VibrationMTO.Instante.VibrateStandart();
            mainCube.OnMovementDone?.Invoke();
        }
    }
}