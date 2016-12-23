using UnityEngine;
using System.Collections;

public class FadeOutTutorialOBJs : MonoBehaviour {

	public int objectGroupNumber;
	public GameObject tutorialController;
	FirstLevelTutorialController FLTC;

	void Start(){
		FLTC = tutorialController.GetComponent<FirstLevelTutorialController> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player"){
//			FLTC.eventTriggered[objectGroupNumber]=true;
		}
	}
}
