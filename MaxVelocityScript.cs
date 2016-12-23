using UnityEngine;
using System.Collections;

public class MaxVelocityScript : MonoBehaviour {

	public float maxXVel;
	public float maxYVel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
if (GetComponent<Rigidbody2D>().velocity.x > maxXVel) {
		GetComponent<Rigidbody2D>().velocity=new Vector2(maxXVel,GetComponent<Rigidbody2D>().velocity.y);		
		}
	if (GetComponent<Rigidbody2D>().velocity.y > maxYVel) {
			GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x,maxYVel);		
	}
		if (GetComponent<Rigidbody2D>().velocity.x < -1*maxXVel) {
			GetComponent<Rigidbody2D>().velocity=new Vector2(-1*maxXVel,GetComponent<Rigidbody2D>().velocity.y);		
		}
		if (GetComponent<Rigidbody2D>().velocity.y < -1*maxYVel) {
			GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x,-1*maxYVel);		
		}
	}
}
