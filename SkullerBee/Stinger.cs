using UnityEngine;
using System.Collections;

public class Stinger : MonoBehaviour {

	Animator anim;
	SkullerBeeBehavior SBB;
	Rigidbody2D	rbody;
	void Start () {
		anim = GetComponentInParent<Animator> ();
		SBB = GetComponentInParent<SkullerBeeBehavior> ();
		rbody = GetComponentInParent<Rigidbody2D> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerControls> ()) {
			anim.SetBool("Suicide",true);
			SBB.dead=true;
			rbody.isKinematic = false;
			rbody.AddForce (new Vector2(-rbody.velocity.x*75, 4000));
		}
	}
}
