using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoSystem : MonoBehaviour
{
    public static UndoSystem instance;

    private Stack<MainCube> cubeMoves;

    private void Awake()
    {
        instance = this;

        cubeMoves = new Stack<MainCube>();
    }

    public void Move(MainCube cube)
    {
        cubeMoves.Push(cube);
    }

    public bool Undo()
    {
        while (cubeMoves.Count > 0)
        {
            MainCube cube = cubeMoves.Pop();

            if (cube.isOnTarget)
            {
                cube.Undo();
                return true;
            }
        }

        return false;
    }

    private void OnDestroy()
    {
        cubeMoves.Clear();
    }
}