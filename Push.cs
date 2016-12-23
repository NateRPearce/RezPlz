using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other){
	if (other.GetComponent<knockBackScript> () != null) {
		knockBackScript KBS=other.GetComponent<knockBackScript>();
			Debug.Log ("pushed");
			KBS.Launched(GetComponent<Rigidbody2D>().velocity.x*50,GetComponent<Rigidbody2D>().velocity.y*50);
		}
	}
}
