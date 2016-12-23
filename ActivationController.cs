using UnityEngine;
using System.Collections;

public class ActivationController : MonoBehaviour {

	public float delay;
	float t;
	Animator anim;
	public bool StartOn;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		t = 0;
		if (StartOn) {
			anim.SetBool("Active",true);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (StartOn) {
			return;
		}
		t += Time.deltaTime;
		if (t > delay) {
			anim.SetBool("Active",true);
		}
	}
}
