using UnityEngine;
using System.Collections;

public class LavaRainTrigger : MonoBehaviour {

	public Transform[] target = new Transform[1];
	RemoteTriggerScript[] targetRTS;
	// Use this for initialization
	void Start () {
		targetRTS = new RemoteTriggerScript[target.Length];
		for(int i=0;i<target.Length; i++){
			targetRTS[i]=target[i].GetComponent<RemoteTriggerScript>();
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other) {
	if (other.tag == "InstaKillLava") {
			StartCoroutine("Trigged");
		}
	}

	IEnumerator Trigged(){
		GetComponent<Collider2D>().enabled=false;
		for(int i=0;i<target.Length; i++){
			targetRTS[i].Activated=true;
		}
		yield return new WaitForSeconds (2);
		GetComponent<Collider2D>().enabled = true;
	}
}
