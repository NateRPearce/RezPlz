using UnityEngine;
using System.Collections;

public class EntranceLavaScript : MonoBehaviour {

	RemoteTriggerScript RTS;
	bool activated;
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
		RTS=GetComponent<RemoteTriggerScript> ();
	}
	
	void FixedUpdate () {
		activated = RTS.Activated;
		if (activated&&!anim.GetBool("Activated")) {
			anim.SetBool("Activated", true);
		}
	}
}
