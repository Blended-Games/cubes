using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public static ParticleDestroy Instante;

    private void Awake()
    {
        Instante = this;
    }

    private GameObject particle;
    public GameObject[] Particle;

    public void ParticleSorting(string colorname, GameObject cube)
    {
        string name = "";

        foreach (char c in colorname)
        {
            if (c == ' ')
                break;

            name += c;
        }

        if (IsStringMatch(name, "M_Blue"))
        {
            particle = Instantiate(ParticleDestroy.Instante.Particle[0]);
        }
        else if (IsStringMatch(name, "M_Green"))
        {
            particle = Instantiate(ParticleDestroy.Instante.Particle[1]);
        }
        else if (IsStringMatch(name, "M_Pink"))
        {
            particle = Instantiate(ParticleDestroy.Instante.Particle[2]);
        }
        else if (IsStringMatch(name, "M_Purple"))
        {
            particle = Instantiate(ParticleDestroy.Instante.Particle[3]);
        }
        else if (IsStringMatch(name, "M_Red"))
        {
            particle = Instantiate(ParticleDestroy.Instante.Particle[4]);
        }
        else if (IsStringMatch(name, "M_Yellow"))
        {
            particle = Instantiate(ParticleDestroy.Instante.Particle[5]);
        }

        particle.transform.position = (cube.transform.position);
    }

    bool IsStringMatch(string name, string target)
    {
        if (name.Equals(target))
            return true;

        return false;
    }
}