using UnityEngine;
using System.Collections;

public class InvisibleTrigger : MonoBehaviour {

	public Transform target;
	RemoteTriggerScript RTS;
	public bool deactivate;

	void Start () {
		RTS = target.GetComponent<RemoteTriggerScript> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
	if (other.tag == "Player") {
			RTS.Activated=!deactivate;
		}
	}
}
