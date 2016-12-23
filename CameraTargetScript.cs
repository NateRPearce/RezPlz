using UnityEngine;
using System.Collections;

public class CameraTargetScript : GameStateFunctions {

	public bool useControllerToDeactivate;
	public bool timerToDeactivate;
	public bool playerExitToDeactivate;
	public bool singleUse;
	bool cameraInPosition;



	void Start(){
		FindGM ();
	}


	void Update(){
		if (cameraInPosition) {
			if(useControllerToDeactivate){
				if(Input.GetButtonUp("P2_Jump")||Input.GetButtonUp("P1_Jump")){
					CBS.cameraEvent=false;
				    //GM.PM.controlsLocked=false;
					if(singleUse){
						Destroy(gameObject);
					}
				}	
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "CameraCollider") {
			cameraInPosition=true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player"&&playerExitToDeactivate) {
			if(singleUse){
				CBS.cameraEvent=false;
				GM.PM.controlsLocked=false;
				if(singleUse){
					Destroy(gameObject);
				}
			}
		}
	}
}
