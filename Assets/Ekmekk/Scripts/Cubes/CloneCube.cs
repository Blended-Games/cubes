using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CloneCube : MonoBehaviour, Clickable
{
    public MainCube mainCube;

    private CubeTargetFinder cubeTargetFinder;
    private CubeAnimations cubeAnimations;

    private Target currentTarget;

    private bool isDestroyable;

    private Vector2 loopDir;

    private void Awake()
    {
        cubeAnimations = GetComponent<CubeAnimations>();
        cubeTargetFinder = GetComponent<CubeTargetFinder>();
    }

    public void ControlTarget()
    {
        Target target = cubeTargetFinder.GetNearestTarget();

        if (!target || !target.isEmpty)
        {
            isDestroyable = true;
            cubeAnimations.JustGoDown();
        }
        else
        {
            currentTarget = target;
            currentTarget.Fill();
            if (currentTarget.direction == Vector2.zero)
            {
                cubeAnimations.SettleDownTarget(target, () => { mainCube.OnMovementDone?.Invoke(); });
            }
            else
            {
                loopDir = currentTarget.direction;
                cubeAnimations.SettleDownTarget(target, CreateLoop);
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
        GameObject temp =
            Instantiate(mainCube.GetComponent<CubeLooper>().loopCube, transform.position, Quaternion.identity)
                .gameObject;
        temp.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

        temp.GetComponent<LoopCube>().Init(loopDir, mainCube, this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDestroyable && (other.CompareTag("Map") || other.CompareTag("Cube")))
        {
            ParticleDestroy.Instante.ParticleSorting(transform.GetComponent<Renderer>().sharedMaterial.name,
                transform.gameObject);
            Destroy(transform.gameObject);
            VibrationMTO.Instante.VibrateStandart();
            CameraShake.instance.Shake();
            mainCube.OnMovementDone?.Invoke();
        }
    }
}