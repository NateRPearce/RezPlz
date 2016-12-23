using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour {

	ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
	RemoteTriggerScript RTS;
	public int emissionRate;
	Collider2D col;
	Light Plight;


	// Use this for initialization
	void Start () {
		col = GetComponentInChildren<Collider2D> ();
		PS = GetComponent<ParticleSystem> ();
        PE = PS.emission;
		if (GetComponent<RemoteTriggerScript> () != null) {
			RTS=GetComponent<RemoteTriggerScript>();
		}
		if (GetComponentInChildren<Light> () != null) {
			Plight = GetComponentInChildren<Light> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<RemoteTriggerScript> () != null) {
			if (RTS.Activated) {
				PE.rate = new ParticleSystem.MinMaxCurve(emissionRate);
			} else {
				PE.rate = new ParticleSystem.MinMaxCurve(0);
			}
			col.enabled = RTS.Activated;
			if (GetComponentInChildren<Light> () != null) {
				Plight.enabled=RTS.Activated;
			}
		}
	}
}
