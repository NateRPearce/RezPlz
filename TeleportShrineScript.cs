using UnityEngine;
using System.Collections;

public class TeleportShrineScript : GameStateFunctions {


	//general variables
	public bool porting;
	public float teleDelay;
	public bool playerOn;
	public string grabbtn;
	public Transform startLoc;
	public Transform endLoc;
	public Transform glow;
	public Animator anim;
	// Use this for initialization
	void Start () {
		FindGM ();
		grabbtn=GM.grabbtn[0];
		anim = startLoc.GetComponent<Animator> ();
	}

	void Update(){
		if (playerOn) {
			if(Input.GetButtonDown(grabbtn)){
				porting=true;
			}
			anim.SetBool("Activated",true);	
			glow.GetComponent<Light>().enabled=true;
		}else{
			anim.SetBool("Activated",false);
			glow.GetComponent<Light>().enabled=false;
			porting=false;
			teleDelay=0;
		}

	}

	void FixedUpdate(){
		if (porting) {
			teleDelay+=Time.deltaTime;
		}
		if (teleDelay > 1.3f) {
			teleDelay=0;
			playerOn=false;
		}
	}




	void OnTriggerStay2D(Collider2D other){
		if (other.name == "Player1"|| GM.numOfPlayers==1) {
			grabbtn=GM.grabbtn[0];
		} else if(other.name=="Player2"){
			grabbtn=GM.grabbtn[1];
		}
		if (other.tag == "Player") {
			playerOn=true;
			if(teleDelay>0.95f){
				other.transform.position=endLoc.transform.position;
			}
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			playerOn=false;
		}
	}

}

