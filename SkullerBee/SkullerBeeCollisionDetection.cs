using UnityEngine;
using System.Collections;

public class SkullerBeeCollisionDetection : MonoBehaviour {

	SkullerBeeBehavior SBB;
	void Start () {
		SBB = GetComponentInParent<SkullerBeeBehavior> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == Tags.attackCollider) {
			PlayerControls PC=other.GetComponentInParent<PlayerControls>();
			SBB.Hit (PC.facingRight);
		}
	}
}
