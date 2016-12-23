using UnityEngine;
using System.Collections;

public class HitEffectScript : MonoBehaviour {

	public float timeEmitting;
	public float destroyedTime;
	float time=0;
    ParticleSystem PS;
    ParticleSystem.EmissionModule PE;

    void Awake()
    {
        PS = GetComponent<ParticleSystem>();
        PE = PS.emission;
    }
    void FixedUpdate () {
		time += Time.deltaTime;
		if (time > timeEmitting) {
            PE.rate = new ParticleSystem.MinMaxCurve(0);
		}
		if (time > destroyedTime) {
			Destroy(gameObject);		
		}
	}
}
