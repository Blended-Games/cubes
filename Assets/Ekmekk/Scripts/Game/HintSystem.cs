using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HintSystem : MonoBehaviour
{
    public static HintSystem instance;

    private Target[] targetList;
    public MainCube[] mainCubes;

    private void Awake()
    {
        instance = this;

        mainCubes = FindObjectsOfType<MainCube>();
        targetList = FindObjectsOfType<Target>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Hint();
    }

    public void Hint()
    {
        List<int> IDlist = new List<int>();

        foreach (MainCube mainCube in mainCubes)
        {
            if (!mainCube.isOnTarget)
                IDlist.Add(mainCube.id);
        }

        if(IDlist.Count == 0)
            return;
        
        int randomIndex = Random.Range(0, IDlist.Count);

        foreach (Target target in targetList)
        {
            if (target.id == IDlist[randomIndex])
            {
                target.Hint();
            }
        }
    }
}