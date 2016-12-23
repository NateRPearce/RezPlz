using UnityEngine;
using System.Collections;

public class PendulumBladeScript : RemoteControllable {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		if (RemoteControl)
			RTS = GetComponent<RemoteTriggerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckActiveStatus ();
	if (RemoteControl && activated) {
			anim.SetBool ("Active", true);
		} else if (activated) {
			anim.SetBool ("Active", true);
		}
	}
}
