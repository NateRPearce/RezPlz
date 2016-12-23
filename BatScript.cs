using UnityEngine;
using System.Collections;

public class BatScript : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;
	bool facingRight;
	public float speed;
	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		rbody.AddForce (new Vector2(speed, 0));
		InvokeRepeating ("Flap", Time.deltaTime, 0.4f);
		InvokeRepeating ("ChangeDirection", Time.deltaTime, 2);
	}
	
	void Flap(){
		anim.SetTrigger ("Flap");
		rbody.AddForce (new Vector2 (0, Random.Range(30,50)));
	}

	void ChangeDirection(){
		int newDir = Random.Range (-1, 3);
		if (newDir < 0) {
			speed=speed*-1;
			rbody.AddForce (new Vector2(speed*2, 0));
			Flip();
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
