using UnityEngine;
using System.Collections;

public class ToggleLeverScript : SoundFunctions {


    [FMODUnity.EventRef]
    string hitSound = "event:/NATECHECKTHISOUT/Swing_Hit_Lever_01";
    public FMOD.Studio.EventInstance hitEv;

    [FMODUnity.EventRef]
    string activationSound = "event:/Environment/General_Generic_Lever_Activate";
    public FMOD.Studio.EventInstance activationEv;


    public bool activated;
	public bool invert;
	public float activeTimer;
	public Transform[] target= new Transform[1];
	RemoteTriggerScript[] targetRTS;
	Animator anim;
	public float delayBetweenTargets;


	void Start () {
        if (invert)
        {
            activated = true;
        }
		FindGM ();
		targetRTS = new RemoteTriggerScript[target.Length];
		for (int i=0; i<target.Length; i++) {
			targetRTS[i]=target[i].GetComponent<RemoteTriggerScript>();
		}
		anim = GetComponent<Animator> ();
        hitEv = FMODUnity.RuntimeManager.CreateInstance(hitSound);
        activationEv = FMODUnity.RuntimeManager.CreateInstance(activationSound);
    }


    void FixedUpdate(){
	if (activated && activeTimer <= 0.35f) {
			activeTimer+=0.02f;
		}

	if (!activated && activeTimer >= 0) {
			activeTimer-=0.02f;
		}
		anim.SetFloat ("ActiveTime", activeTimer);
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="AttackCollider"){
			PlayerControls PC = other.GetComponentInParent<PlayerControls>();
            hitEv.setVolume(checkRange());
            hitEv.start();
            if (PC.facingRight){
				activated=true;
			}else{
				activated=false;
            }
            if (invert)
            {
                if (activated == targetRTS[0].Activated)
                {
                    activationEv.setVolume(checkRange());
                    activationEv.start();
                    StartCoroutine("ActivateSequence");
                }
            }
            else
            {
                if (activated != targetRTS[0].Activated)
                {
                    activationEv.setVolume(checkRange());
                    activationEv.start();
                    StartCoroutine("ActivateSequence");
                }
            }
		}
	}

	IEnumerator ActivateSequence(){
		for (int i=0; i<targetRTS.Length; i++) {
            if (invert)
            {
                targetRTS[i].Activated = !activated;
            }
            else
            {
                targetRTS[i].Activated = activated;
            }
			yield return new WaitForSeconds(delayBetweenTargets);
		}
	}
}
