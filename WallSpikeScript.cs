using UnityEngine;
using System.Collections;

public class WallSpikeScript : MonoBehaviour {
	
	public Animator anim;
	public bool playerBledOut;
	void Start(){
		anim = GetComponentInParent<Animator> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (!playerBledOut) {
			if (other.name == "BodyCollider") {
				anim.SetBool ("PlayerOnSpike", true);
				playerBledOut = true;
			}
		}
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (!playerBledOut) {
			if (other.name == "BodyCollider") {
				anim.SetBool ("PlayerOnSpike", true);
				playerBledOut = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.name == "BodyCollider") {
			playerBledOut=false;
		}
	}
}
