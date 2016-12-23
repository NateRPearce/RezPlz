using UnityEngine;
using System.Collections;

public class knockBackScript : MonoBehaviour {

	public float forceMultipier;

	public void Launched(float xforce,float yforce){
		if (GetComponent<Rigidbody2D>() == null) {
			Rigidbody2D rbody = GetComponentInParent<Rigidbody2D>();
			rbody.AddTorque(xforce/5);
			rbody.AddForce (new Vector2 (xforce * forceMultipier, yforce * forceMultipier));
		} else {
			GetComponent<Rigidbody2D>().AddTorque(-xforce/5);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (xforce * forceMultipier, yforce * forceMultipier));
		}
	}
}
