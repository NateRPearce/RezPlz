using UnityEngine;
using System.Collections;

public class AcidSkeletonBehavior : MonoBehaviour {

	Rigidbody2D[] rbodies = new Rigidbody2D[13];
	// Use this for initialization
	void Start () {
		rbodies = GetComponentsInChildren<Rigidbody2D> ();
		StartCoroutine ("DropBones");
	}
	
IEnumerator DropBones(){
		yield return new WaitForSeconds (1);		 
	foreach (Rigidbody2D rb in rbodies) {
			rb.isKinematic=false;
		}
	}
}
