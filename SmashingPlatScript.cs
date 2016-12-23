using UnityEngine;
using System.Collections;

public class SmashingPlatScript : MonoBehaviour {

	RemoteTriggerScript RTS;
	public Vector3 startPos;
	public Vector3 currentPos;
	public Transform smashTarget;
	public Collider2D smashCollider;
	public bool moveTowardsTarget;
	public float speedTowardTarget;
	public float returnSpeed;
	public float startDelay;
	public bool movesHorizontal;
	public bool toggle;
	public bool deactivateEnabled;
	public bool inverted;

	void Start(){
		startPos = transform.position;
		if (GetComponent<RemoteTriggerScript> () != null) {
			RTS=GetComponent<RemoteTriggerScript>();
		}
	}

	void Update(){
		if (toggle) {
			if(inverted){
				moveTowardsTarget=!RTS.Activated;
			}else{
				moveTowardsTarget=RTS.Activated;
			}
		} else {
			//determines which direction the platform is moving
			//if the plat is close to its target(return to start pos)
			if (transform.position == smashTarget.position) {
				moveTowardsTarget = false;
			}
			//if the plat is close to its starting pos(return to target pos)
			if (transform.position == startPos) {
				moveTowardsTarget = true;
			}
		}
		if (movesHorizontal) {
			if ((transform.position.x > smashTarget.position.x - 3 && transform.position.x < smashTarget.position.x + 3)&&moveTowardsTarget) {
				smashCollider.enabled = true;
			} else {
				smashCollider.enabled = false;
			}
		}else{
			//enables and disables the smash collider when the plat comes close to a wall
			if ((transform.position.y > smashTarget.position.y - 4 && transform.position.y < smashTarget.position.y + 4)&&moveTowardsTarget) {
				smashCollider.enabled = true;
			} else {
				smashCollider.enabled = false;
			}
		}
	}


	void FixedUpdate () {
		currentPos = transform.position;
		if (startDelay > 0) {
			startDelay -= Time.deltaTime;
			return;
		}
		if (deactivateEnabled && RTS.Activated) {
			transform.position=Vector3.MoveTowards(transform.position,startPos,returnSpeed);
			return;
		}
		if (moveTowardsTarget) {
			transform.position=Vector3.MoveTowards(transform.position,smashTarget.position,speedTowardTarget);
		} else {
			transform.position=Vector3.MoveTowards(transform.position,startPos,returnSpeed);
		}
	}
}
