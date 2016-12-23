using UnityEngine;
using System.Collections;

public class RemoteTimer : MonoBehaviour {

	public float timeOn;
	public float timeOff;
	public float startDelay;
	RemoteTriggerScript RTS;
	public Transform target;
	public float gameTime;
	// Use this for initialization
	void Start () {
		RTS = target.GetComponent<RemoteTriggerScript> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (startDelay > 0) {
			startDelay -= Time.deltaTime;
		} else {
			gameTime += Time.deltaTime;
		if(gameTime<timeOn){
				RTS.Activated=true;
			}else if(gameTime>timeOn&&gameTime<timeOff){
				RTS.Activated=false;
			}else{
				gameTime=0;
			}
		}
	}
}
