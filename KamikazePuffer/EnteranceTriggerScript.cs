using UnityEngine;
using System.Collections;

public class EnteranceTriggerScript : MonoBehaviour {

	public Collider2D PufferEnterance;

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			PufferEnterance.enabled=true;		
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			PufferEnterance.enabled=false;		
		}
	}
}
