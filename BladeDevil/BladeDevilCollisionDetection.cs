using UnityEngine;
using System.Collections;

public class BladeDevilCollisionDetection : MonoBehaviour {
	
	public BladeDevilScript BDS;

	void Awake(){
		BDS = GetComponentInParent<BladeDevilScript> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		BDS.HitBy (other.gameObject);
	}
	void OnTriggerStay2D(Collider2D other){
		BDS.HitBy (other.gameObject);
	}
}
