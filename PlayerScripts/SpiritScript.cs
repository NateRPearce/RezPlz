using UnityEngine;
using System.Collections;

public class SpiritScript : MonoBehaviour {

	public Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
public void OopPhase(int phase){
	if (phase == 1) {
			anim.SetBool ("Ooping", true);
		} else if (phase == 2) {
			anim.SetBool ("Ooping", false);
			anim.SetBool ("OopReady", true);
		} else if (phase == 3) {
			anim.SetBool ("OopToss", true);
			anim.SetBool ("OopReady", false);
		} else if (phase == 0) {
			anim.SetBool ("Ooping", false);
			anim.SetBool ("OopReady", false);
			anim.SetBool ("OopToss", false);
			anim.SetBool("Attacking",false);
		}
	}
}
