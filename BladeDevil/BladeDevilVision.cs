using UnityEngine;
using System.Collections;

public class BladeDevilVision : MonoBehaviour {

	public BladeDevilScript BDS;
	
	void Awake(){
		BDS = GetComponentInParent<BladeDevilScript> ();
	}
	
	
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			BDS.playerSpotted=true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			BDS.playerSpotted=false;
		}
	}
}
