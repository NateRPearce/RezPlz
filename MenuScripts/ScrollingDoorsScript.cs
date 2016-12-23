using UnityEngine;
using System.Collections;

public class ScrollingDoorsScript : MonoBehaviour {

	LevelEnteranceControllerScript LECS;
	public Vector3 startingPos;
	public float offsetL;
	public float offsetR;
	// Use this for initialization
	void Start () {
		startingPos = transform.localPosition;
		LECS = GetComponentInParent<LevelEnteranceControllerScript> ();
	}
	void Update(){
		if (LECS.direction <0.5f) {
			if (transform.localPosition.x < startingPos.x - offsetL) {
				transform.localPosition = new Vector3 (transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
				LECS.LevelNumber[0].text="Level " + LECS.doorNumber;
				LECS.LevelNumber[1].text="Level " + LECS.doorNumber;
				LECS.direction = 0;
				return;
			}
		} else if (LECS.direction >0.5f) {
			if (transform.localPosition.x > startingPos.x + offsetR) {
				transform.localPosition = new Vector3 (transform.localPosition.x - 180, transform.localPosition.y, transform.localPosition.z);
				LECS.LevelNumber[0].text="Level " + LECS.doorNumber;
				LECS.LevelNumber[1].text="Level " + LECS.doorNumber;
				LECS.direction = 0;
				return;
			}
		}
		if (LECS.direction == 0) {
			LECS.FillLevelStats();
			LECS.coolDown=false;
		}
	}
	void FixedUpdate () {
		scroll (LECS.direction);
	}

	void scroll(float dir){
		transform.position = new Vector3 (transform.position.x+dir, transform.position.y, transform.position.z);
	}
	
}
