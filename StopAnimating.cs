using UnityEngine;
using System.Collections;

public class StopAnimating : MonoBehaviour {

	Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
	}
	void StopAnim(){
		anim.speed = 0;
	}
}
