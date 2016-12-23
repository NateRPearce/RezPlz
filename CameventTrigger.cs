using UnityEngine;
using System.Collections;

public class CameventTrigger : GameStateFunctions {

	public bool triggerEvent;
	public bool endEvent;
	public bool lockCamera;
	public bool unlockCamera;
	public bool freezeCamera;
    public bool overrideCamera;
    public float unlockDelay;

    void Start(){
		FindGM ();
		FindCBS ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
            StartCoroutine("Unlock");
		}
	}
         IEnumerator Unlock()
        {
             yield return new WaitForSeconds(unlockDelay);
        CBS.maxPosOverride = overrideCamera;
        if (triggerEvent)
        {
            GM.PM.controlsLocked = true;
            CBS.CamSpeed = 100;
        }
        if (endEvent)
        {
            GM.PM.controlsLocked = false;
            CBS.CamSpeed = 2;
        }
        if (freezeCamera)
        {
            CBS.cameraEvent = true;
            CBS.maxPosOverride = true;
            CBS.EventLocation = CBS.cam.transform.position;
        }
        if (unlockCamera)
        {
            CBS.cameraEvent = false;
            CBS.maxPosOverride = false;
        }
    }
    }
