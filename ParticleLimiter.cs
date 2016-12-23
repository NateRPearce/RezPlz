using UnityEngine;
using System.Collections;

public class ParticleLimiter : MonoBehaviour {

	public ParticleSystem[] PS;
    ParticleSystem.EmissionModule[] PE;
    public bool psFound;
	// Use this for initialization
	void Start () {
		if (GetComponentsInChildren<ParticleSystem> () != null) {
			psFound = true;
		}
		if (psFound) {
			PS = new ParticleSystem[GetComponentsInChildren<ParticleSystem> ().Length];
			PS = GetComponentsInChildren<ParticleSystem> ();
            PE = new ParticleSystem.EmissionModule[PS.Length];
		}
	}
    public void setLevel(int qualityLvl)
    {
        for (int i = 0; i < PS.Length; i++)
        {
            PE[i] = PS[i].emission;
        }
        if (qualityLvl == 1)
        {
            for (int i = 0; i < PS.Length; i++)
            {
                PE[i].rate = new ParticleSystem.MinMaxCurve((int)PE[i].rate.constantMax * 0.5f);
            }
        }
        else if (qualityLvl == 2)
        {
            for (int i = 0; i < PS.Length; i++)
            {
                PE[i].rate = new ParticleSystem.MinMaxCurve(0);
            }
        }
    }
}
