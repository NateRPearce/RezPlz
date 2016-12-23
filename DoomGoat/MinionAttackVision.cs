using UnityEngine;
using System.Collections;

public class MinionAttackVision : MonoBehaviour {

	public MinionScript MS;

	void Awake(){
		MS = GetComponentInParent<MinionScript> ();
	}
	
	
	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<PlayerControls>()!=null) {
			MS.targetPC=other.GetComponent<PlayerControls>();
			MS.Attack();
			MS.playerPos=other.transform.position;
		}
		if (other.tag=="Decoy") {
			MS.Attack();
			MS.playerPos=other.transform.position;
		}
	}
}
