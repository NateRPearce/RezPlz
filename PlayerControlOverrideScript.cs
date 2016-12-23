using UnityEngine;
using System.Collections;

public class PlayerControlOverrideScript : MonoBehaviour {

	public bool activated;
	RemoteTriggerScript RTS;
	public float[] actionDuration = new float[1];
	public string[] action = new string[1];
	public Transform Player1;
	public Transform Player2;
	void Start () {
	if (GetComponent<RemoteTriggerScript>() != null) {
			RTS=GetComponent<RemoteTriggerScript>();		
		}
	}
	
	void Update () {
		activated = RTS.Activated;
	}

	void FixedUpdate(){

	
	}

	void ActionCheck(int actionNumber){
		switch (action[actionNumber]) {
		case "walkR":
			
			break;
		default:
			
			break;
		}
	}
}
