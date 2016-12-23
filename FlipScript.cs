using UnityEngine;
using System.Collections;

public class FlipScript : GameStateFunctions{

	public bool mimicTarget;
	public Transform target;
    FollowScript FS;
	bool facingRight = true;

	void Start(){
		FindGM ();
        FS = GetComponent<FollowScript>();
	}
	void Update () {
        if (FS.target == null)
        {
            return;
        }
        target = FS.target;
		//Flip the character when direction is changed
		if (mimicTarget) {
			if (target.position.x>transform.position.x && !facingRight) {
				Flip ();
			} else if (target.position.x<transform.position.x && facingRight) {
				Flip ();
			}
		} else {
			if (GetComponent<Rigidbody2D>().velocity.x > 0 && !facingRight) {
				Flip ();
			} else if (GetComponent<Rigidbody2D>().velocity.x < 0 && facingRight) {
				Flip ();
			}
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
