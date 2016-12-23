using UnityEngine;
using System.Collections;

public class DetonationScript : MonoBehaviour {

	public PufferScript PS;

	void Awake(){
		PS = GetComponentInParent<PufferScript> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (PS.Entered&&(other.tag == "Player"||other.tag == "Lava"||other.tag == "GroundSpikes")) {
			PS.exploding=true;	
		}
	}
}
