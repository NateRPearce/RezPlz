using UnityEngine;
using System.Collections;

public class LavaGeiser : RemoteControllable {

	
	public ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
	public ProjectileSpawner PSpawn;
	public Transform liftPlatform;
	public RemoteTriggerScript PlatRTS;
	public float timeOn;
	public float blastIntervals;
	public float blastTime;
	public bool on;
	public bool toggle;
    public GeyserDetector GD;
	void Awake () {
		if (transform.GetComponent<ProjectileSpawner> () != null) {
			PSpawn=transform.GetComponent<ProjectileSpawner> ();
		}
		if(transform.GetComponent<RemoteTriggerScript>()!=null){
			RTS = GetComponent<RemoteTriggerScript> ();
		}
		if (liftPlatform != null) {
			PlatRTS = liftPlatform.GetComponent<RemoteTriggerScript> ();
		} else {
			RTS=GetComponent<RemoteTriggerScript>();
		}
        if (GetComponent<GeyserDetector>() != null)
        {
            GD = GetComponent<GeyserDetector>();
        }
		PS = GetComponent<ParticleSystem> ();
        PE = PS.emission;
	}
	void Start(){
		//CheckDifficulty ();
	}
	void Update () {
		if (PSpawn != null) {
			PSpawn.disabled = disabled;
			PSpawn.activated = on;
			PSpawn.difficultyLvl=difficultyLvl;
		}
		if(PlatRTS!=null){
			PlatRTS.Activated = on;
		}
		CheckDifficulty();
		if (disabled) {
            PE.rate = new ParticleSystem.MinMaxCurve(0);
            return;
		}
		if (RemoteControl) {
			CheckActiveStatus ();
		}
			if (on) {
            PE.rate = new ParticleSystem.MinMaxCurve(100);
        }
        else{
            PE.rate = new ParticleSystem.MinMaxCurve(0);
        }
	}

	void FixedUpdate(){
		if (toggle) {
            if (RTS != null)
            {
                if (GD != null)
                {
                    if (GD.ulmockNear)
                    {
                        on = false;
                        return;
                    }
                }
                on = RTS.Activated;
            }
			return;
		} else if (activated) {
			blastTime += Time.deltaTime;
			if (blastTime > blastIntervals && blastTime < blastIntervals + timeOn) {
				on = true;
			} else if (blastTime > blastIntervals + timeOn) {
				on = false;
				blastTime = 0;
			}
		}
	}
}
