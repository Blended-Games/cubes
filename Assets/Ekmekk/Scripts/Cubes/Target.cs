using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int id;

    private MeshRenderer meshRenderer;

    [SerializeField] public Vector3 pos;
    [SerializeField] public bool isEmpty = true;
    [SerializeField] private bool left, right, up, down;
    [HideInInspector] public Vector2 direction;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        pos = transform.position;

        if (up)
        {
            direction = new Vector2(0, 1);
        }
        else if (down)
        {
            direction = new Vector2(0, -1);
        }
        else if (left)
        {
            direction = new Vector2(1, 0);
        }
        else if (right)
        {
            direction = new Vector2(-1, 0);
        }
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        MainCube[] mainCubes = HintSystem.instance.mainCubes;

        foreach (MainCube mainCube in mainCubes)
        {
            if (mainCube.id == id)
            {
                Color temp = mainCube.GetComponent<MeshRenderer>().material.color;
                temp.a = 0f;
                meshRenderer.material.color = temp;
            }
        }
    }

    public void Fill()
    {
        isEmpty = false;
        meshRenderer.enabled = false;
    }

    public void Hint()
    {
        StartCoroutine(OpenForHint());
    }

    IEnumerator OpenForHint()
    {
        if (!isEmpty)
            yield break;

        meshRenderer.enabled = true;

        Color temp = meshRenderer.material.color;

        float elapsedTime = 0;
        float maxTime = 2;

        while (elapsedTime < maxTime)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / maxTime;
            float alpha = Mathf.Lerp(0, 0.5f, progress);
            temp.a = alpha;
            meshRenderer.material.color = temp;

            yield return null;
        }

        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            float progress = elapsedTime / maxTime;
            float alpha = Mathf.Lerp(0f, 0.5f, progress);
            temp.a = alpha;
            meshRenderer.material.color = temp;

            yield return null;
        }

        meshRenderer.enabled = false;
    }
}