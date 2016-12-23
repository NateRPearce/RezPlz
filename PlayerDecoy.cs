using UnityEngine;
using System.Collections;

public class PlayerDecoy : MonoBehaviour {

	public Collider2D Decoy;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
	if (other.tag == "Player") {
			StartCoroutine("DoIt");
		}
	}
	IEnumerator DoIt(){
		Decoy.enabled=true;
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
}
