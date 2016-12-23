using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour {

	RemoteTriggerScript RTS;
	Animator anim;
	public bool invert;
	// Use this for initialization
	void Start () {
		RTS = GetComponent<RemoteTriggerScript> ();
		anim = GetComponent<Animator> ();
		InvokeRepeating ("OpenCheck",Time.deltaTime,0.5f);
	}
	
	public void OpenCheck(){
		if (invert) {
			if(!RTS.Activated){
				anim.SetBool ("Activated", true);
			}
			}else if (RTS.Activated) {
			anim.SetBool ("Activated", true);
		}
	}
}
