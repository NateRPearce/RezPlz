using UnityEngine;
using System.Collections;

public class RotatePlatScript : SoundFunctions {


    [FMODUnity.EventRef]
    string activationSound = "event:/Environment/General_Generic_Lever_Activate";
    public FMOD.Studio.EventInstance activationEv;

        public Transform[] gears=new Transform[2];
		public Animator[] gearAnims;
		public Animator platAnim;
		bool activated;
		public bool Invert;
		RemoteTriggerScript RTS;
		
		
	void Start () {
        soundStartFunctions();
			RTS = GetComponent<RemoteTriggerScript> ();
			gearAnims = new Animator[gears.Length];
			for (int i=0; i<gears.Length; i++) {
				gearAnims[i]=gears[i].GetComponent<Animator>();
			}
            activationEv = FMODUnity.RuntimeManager.CreateInstance(activationSound);
        }

	void Update(){
			if (Invert) {
				activated = !RTS.Activated;
			} else {
				activated=RTS.Activated;
			}
			platAnim.SetBool ("Active", activated);

		}

	void FixedUpdate(){
			if (activated) {
				gearAnims[0].SetBool("GearUp",true);
				gearAnims[0].SetBool("GearDown",false);
				gearAnims[1].SetBool("GearUp",true);
				gearAnims[1].SetBool("GearDown",false);
			} else {
				gearAnims[0].SetBool("GearDown",true);
				gearAnims[0].SetBool("GearUp",false);
				gearAnims[1].SetBool("GearDown",true);
				gearAnims[1].SetBool("GearUp",false);
			}
		}

	void StopGears(int on){
		if (on == 0) {
			gearAnims [0].SetBool ("Idle", true);
			gearAnims [1].SetBool ("Idle", true);
		} else {
			gearAnims [0].SetBool ("Idle", false);
			gearAnims [1].SetBool ("Idle", false);
		}
	}
    public void ActivateSound()
    {
        activationEv.setVolume(checkRange());
       // activationEv.start();
    }
}
