using UnityEngine;
using System.Collections;

public class OnTriggerEnableScript : MonoBehaviour {

	public GameObject[] targets = new GameObject[1];
	// Use this for initialization
	void Start () {
		for (int i = 0; i<targets.Length; i++) {
			targets[i].SetActive (false);
		}
	}
	
public void OnTriggerStay2D(Collider2D other){
	if (other.tag == "Player") {
			for (int i = 0; i<targets.Length; i++) {
				targets[i].SetActive (true);
			}
			Destroy(gameObject);
		}
	}
}
