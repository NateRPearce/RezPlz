using UnityEngine;
using System.Collections;

public class OnEnterScript : MonoBehaviour {

	
	public string[] triggers = new string[0];
	public Transform[] target = new Transform[0];
	RemoteTriggerScript[] RTS;
	
	void Awake(){
		RTS = new RemoteTriggerScript[target.Length];
		for (int i=0; i<target.Length; i++) {
			RTS[i]=target[i].GetComponent<RemoteTriggerScript>();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		foreach (string T in triggers) {
			if (other.tag == T) {
				for (int i=0; i<target.Length; i++) {
					RTS[i].Activated=true;
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D other){
		foreach (string T in triggers) {
			if (other.tag == T) {
				for (int i=0; i<target.Length; i++) {
					RTS[i].Activated=false;
				}
			}
		}
	}
}
