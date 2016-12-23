using UnityEngine;
using System.Collections;

public class Dissolve : MonoBehaviour {


	public Animator anim;
	LightDetectionScript LDS;
	public PlayerControls PCcontroller;
	public Transform parent;

	void Start(){
		if (transform.parent != null) {
			parent = transform.parent;
		}
		if (transform.GetComponent<Animator> () != null) {
			anim = GetComponent<Animator> ();
		} else {
			anim=GetComponentInChildren<Animator>();
		}
		if (transform.GetComponent<LightDetectionScript> () != null) {
			LDS = GetComponent<LightDetectionScript> ();
		} else {
			LDS = GetComponentInChildren<LightDetectionScript> ();
		}
	}


	void Update () {
		if (PCcontroller == null) {
			return;
		}
		if (PCcontroller.rezing){
			LDS.dissolving=true;
			anim.SetBool("Dissolve", true);	
			if(GetComponent<Rigidbody2D>()!=null){
				//rigidbody2D.isKinematic=false;
			}
			tag="Untagged";
			resetParent();
		}else {
			LDS.dissolving=false;
			anim.SetBool("Dissolve", false);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Lava") {
			GetComponent<Rigidbody2D>().mass=10;
			GetComponent<Rigidbody2D>().freezeRotation = true;
			GetComponent<Rigidbody2D>().gravityScale=0;
			GetComponent<Rigidbody2D>().velocity=new Vector3(0,-0.8f);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Lava") {
			GetComponent<Rigidbody2D>().mass=1;
			GetComponent<Rigidbody2D>().freezeRotation = false;
			GetComponent<Rigidbody2D>().gravityScale=1;
		}
	}

	public void resetParent(){
		transform.parent = parent;
	}

	public void setBool(string s,bool b){
		anim.SetBool (s, b);
	}
}
