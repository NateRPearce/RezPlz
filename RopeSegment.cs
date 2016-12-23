using UnityEngine;
using System.Collections;

public class RopeSegment : GameStateFunctions {

	public Transform chainParent;
	public RopeScript RS;
	public int segmentNum;
	public bool isLanternChain;
	// Use this for initialization
	void Start () {
		FindGM ();
		if (isLanternChain) {
			RS = chainParent.GetComponentInParent < RopeScript> ();
		} else {
			RS = GetComponentInParent < RopeScript> ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "HandCollider") {
			if (other.GetComponentInParent<PlayerControls> () != null && RS.playersOnRope==0) {
				RS.PC[0] = other.GetComponentInParent<PlayerControls> ();
				if(RS.PC[0].onFire||RS.PC[0].bisected){
					return;
				}
				RS.currentSegment[0] = segmentNum;
				if (Input.GetButton (RS.PC[0].grabbtn)) {
					RS.Player[0] = RS.PC[0].transform;
						RS.playersOnRope += 1;
				}
			}else if(other.GetComponentInParent<PlayerControls> () != null && RS.playersOnRope==1){
				RS.PC[RS.playersOnRope] = other.GetComponentInParent<PlayerControls> ();
				if(RS.PC[RS.playersOnRope].name==RS.PC[0].name){
					return;
				}else{
					RS.currentSegment[RS.playersOnRope] = segmentNum;
					if(Mathf.Abs (RS.currentSegment[0]-RS.currentSegment[1])<5){
						return;
					}
					if (Input.GetButton (RS.PC[RS.playersOnRope].grabbtn)) {
						RS.Player[RS.playersOnRope] = RS.PC[RS.playersOnRope].transform;
							RS.playersOnRope += 1;
					}
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.name == "HandCollider") {
			if (other.GetComponentInParent<PlayerControls> () != null && RS.playersOnRope==0) {
				RS.PC[0] = other.GetComponentInParent<PlayerControls> ();
				if(RS.PC[0].onFire||RS.PC[0].bisected){
					return;
				}
				if (Input.GetButton (RS.PC[0].grabbtn)) {
					RS.Player[0] = RS.PC[0].transform;
					RS.playersOnRope += 1;
				}
			}else if(other.GetComponentInParent<PlayerControls> () != null && RS.playersOnRope==1){
				RS.PC[RS.playersOnRope] = other.GetComponentInParent<PlayerControls> ();
				if(RS.PC[RS.playersOnRope].onFire||RS.PC[RS.playersOnRope].bisected){
					return;
				}
				if(RS.PC[RS.playersOnRope].name==RS.PC[0].name){
					return;
				}else{
					if(Mathf.Abs (RS.currentSegment[0]-RS.currentSegment[1])<5){
						return;
					}
					if (Input.GetButton (RS.PC[RS.playersOnRope].grabbtn)) {
						RS.Player[RS.playersOnRope] = RS.PC[RS.playersOnRope].transform;
						RS.playersOnRope += 1;
					}
				}
			}
		}
	}
}
