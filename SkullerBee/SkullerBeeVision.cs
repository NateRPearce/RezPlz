using UnityEngine;
using System.Collections;

public class SkullerBeeVision : MonoBehaviour {

	SkullerBeeBehavior SBB;


	void Start () {
		SBB = GetComponentInParent<SkullerBeeBehavior> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"&&!SBB.attackCoolingDown) {
			SBB.anim.SetTrigger("Attack");
			SBB.StartCoroutine("AttackCooldown");
		}
	}
}
