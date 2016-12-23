using UnityEngine;
using System.Collections;

public class LavaFallTriggerScript : RemoteControllable {


	public ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
	public ProjectileSpawner PSpawn;
	void Awake () {
		if (transform.GetComponentInChildren<ProjectileSpawner> () != null) {
			PSpawn=transform.GetComponentInChildren<ProjectileSpawner> ();
		}
		if(transform.GetComponent<RemoteTriggerScript>()!=null){
			RTS = GetComponent<RemoteTriggerScript> ();
		}
		PS = GetComponent<ParticleSystem> ();
        PE = PS.emission;
	}
	void Start(){
		CheckDifficulty ();
	}
	void Update () {
		if (PSpawn != null) {
			PSpawn.disabled = disabled;
			PSpawn.activated = activated;
			PSpawn.difficultyLvl=difficultyLvl;
		}
		CheckDifficulty();
		if (disabled) {
            PE.rate = new ParticleSystem.MinMaxCurve(0);
			return;
		}
		if (RemoteControl) {
			CheckActiveStatus ();
			if (activated) {
                PE.rate = new ParticleSystem.MinMaxCurve(200);
            } else {
                PE.rate = new ParticleSystem.MinMaxCurve(0);
            }
		} else {
			if (activated) {
                PE.rate = new ParticleSystem.MinMaxCurve(200);
            }
            else{
                PE.rate = new ParticleSystem.MinMaxCurve(0);
            }
		}
	}
}
