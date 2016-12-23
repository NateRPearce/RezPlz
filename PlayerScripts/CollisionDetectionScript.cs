using UnityEngine;
using System.Collections;

public class CollisionDetectionScript : GameStateFunctions {


	Rigidbody2D mainRBody;
	PlayerControls PC;

	void Start () {
		FindGM ();
		PC = GetComponentInParent<PlayerControls> ();
		mainRBody = GetComponentInParent<Rigidbody2D> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Rope"&&!Input.GetButton(PC.grabbtn)) {
			if(other.GetComponent<Rigidbody2D>().isKinematic){
				return;
			}
			other.GetComponent<Rigidbody2D>().velocity=new Vector3(mainRBody.velocity.x/2,other.GetComponent<Rigidbody2D>().velocity.y,other.GetComponent<Rigidbody2D>().velocity.y);		
		}
	}
}