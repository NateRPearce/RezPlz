using UnityEngine;
using System.Collections;

public class FrogSpitCollision : Projectiles {

	Rigidbody2D rbody;
	public float detonationTime;

	void Awake () {
		anim = GetComponentInParent<Animator> ();
		rbody = GetComponentInParent<Rigidbody2D> ();
	}

	void FixedUpdate(){

		if (rbody.velocity.x == 0&&GetComponent<Collider2D>().enabled) {
			detonationTime += Time.deltaTime;
			if (detonationTime > 0.04f) {
				anim.SetTrigger ("Destroy");

				detonationTime = 0;
			}
		} else if (detonationTime > 0) {
			detonationTime = 0;
		} else if (detonationTime == 0) {
			anim.ResetTrigger("Destroy");
		}
	}


	public void OnTriggerEnter2D(Collider2D other){
		hit (other, tag);
		if (other.tag == "Player" || other.tag == "HitBox"|| other.tag == "AttackCollider") {
			//anim.SetBool ("Activated", false);
			anim.SetTrigger ("Destroy");
			rbody.velocity = new Vector2 (0, 0);
		}
	}
}
