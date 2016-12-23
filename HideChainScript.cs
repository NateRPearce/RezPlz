using UnityEngine;
using System.Collections;

public class HideChainScript : MonoBehaviour {
	Collider2D col;


	void Start(){
		col = GetComponent<Collider2D> ();
		InvokeRepeating ("disableColliders", Time.deltaTime, 1);
	}
	IEnumerator disableColliders(){
		col.enabled = false;
		yield return new WaitForSeconds (0.5f);
		col.enabled = true;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Target"&&GetComponent<Renderer>().enabled) {
			GetComponent<Renderer>().enabled=false;
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Target"&&!GetComponent<Renderer>().enabled) {
			GetComponent<Renderer>().enabled=true;
		}
	}

}
