using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour
{

    public int qualityLvl;
    public ParticleLimiter[] PLs;
    public bool psFound;
    void Awake()
    {
        if (GetComponentsInChildren<ParticleLimiter>() != null)
        {
            psFound = true;
            PLs = new ParticleLimiter[GetComponentsInChildren<ParticleLimiter>().Length];
            PLs = GetComponentsInChildren<ParticleLimiter>();
        }
    }

    public void DisableParticles()
    {
        if (psFound)
        {
            for (int i = 0; i < PLs.Length; i++)
            {
                PLs[i].setLevel(qualityLvl);
            }
        }
    }
}
