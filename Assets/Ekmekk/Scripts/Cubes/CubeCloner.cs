using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCloner : MonoBehaviour
{
    [SerializeField] private GameObject clone;
    [SerializeField] private bool up, down, left, right;

    public List<CloneCube> cloneCubes;

    private void Awake()
    {
        cloneCubes = new List<CloneCube>();
    }

    public void Clone(Action OnComplete)
    {
        int j = -1;
        List<Vector2> direction = new List<Vector2>();
        if (up)
        {
            j++;
            direction.Add(new Vector2(0, 1));
        }

        if (down)
        {
            j++;
            direction.Add(new Vector2(0, -1));
        }

        if (left)
        {
            j++;
            direction.Add(new Vector2(1, 0));
        }

        if (right)
        {
            j++;
            direction.Add(new Vector2(-1, 0));
        }

        if (j == -1)
        {
            OnComplete.Invoke();
        }
        else
        {
            GetComponent<MainCube>().movementCount = j + 1;
        }

        for (int i = 0; i <= j; i++)
        {
            CloneCube temp = Instantiate();
            temp.mainCube = GetComponent<MainCube>();
            cloneCubes.Add(temp);
            temp.GetComponent<CubeAnimations>().CloneMove(direction[i].y, direction[i].x, temp.ControlTarget);
        }
    }

    public void DestroyAll()
    {
        if (cloneCubes.Count == 0)
            return;

        foreach (CloneCube cloneCube in cloneCubes)
        {
            if (cloneCube != null)
                cloneCube.SafeDestroy();
        }

        cloneCubes.Clear();
    }

    CloneCube Instantiate()
    {
        GameObject temp = Instantiate(clone, transform.position, Quaternion.identity).gameObject;
        temp.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;

        return temp.GetComponent<CloneCube>();
    }
}