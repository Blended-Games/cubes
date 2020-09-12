using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CubeLooper : MonoBehaviour
{
    public GameObject loopCube;

    public List<LoopCube> loopCubes;

    private void Awake()
    {
        loopCubes = new List<LoopCube>();
    }

    public void Loop(Vector2 direction)
    {
            LoopCube temp = Instantiate();
            temp.Init(direction,GetComponent<MainCube>(),this.gameObject);
    }

    public void DestroyAll()
    {
        if (loopCubes.Count == 0)
            return;

        foreach (LoopCube loopCube in loopCubes)
        {
            if (loopCube != null)
                loopCube.SafeDestroy();
        }

        loopCubes.Clear();
    }

    LoopCube Instantiate()
    {
        GameObject temp = Instantiate(loopCube, transform.position, Quaternion.identity).gameObject;
        temp.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

        return temp.GetComponent<LoopCube>();
    }


}
