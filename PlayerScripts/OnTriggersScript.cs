using UnityEngine;
using System.Collections;

public class OnTriggersScript : PlayerInheritance {


	void Start () {
		FindPC ();
	}
	
	void OnTriggerStay2D(Collider2D other){
		if (PC.eidolonMode) {
			return;
		}
		if (other.tag == "Teleporter") {
			PC.onTeleporter=true;
		}	
		if (other.tag == "Button") {
			PC.onButton=true;		
		}
		if (other.tag == "Explosion") {
			PC.exploding=true;		
		}
		if (other.tag == "Untagged") {
			PC.onButton=false;		
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (PC.eidolonMode) {
			return;
		}
		if (other.tag == "Teleporter") {
			PC.onTeleporter=false;
		}
		if (other.tag == "Button") {
			PC.onButton=false;		
		}
	}
}
