using UnityEngine;
using System.Collections;

public class Detonator : MonoBehaviour {

	Animator anim;
    Rigidbody2D rbody;
    public Transform ExplosionParticles;
    bool Exploded;
	void Start(){
		anim = GetComponentInParent<Animator> ();
        rbody = GetComponentInParent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D other){
        if (Exploded)
        {
            GetComponent<Collider2D>().enabled = false;
            return;
        }
		if (other.GetComponentInParent<PlayerControls>()!=null||other.GetComponent<DeadlyObject>()!=null) {
            rbody.velocity = Vector2.zero;
            rbody.isKinematic = true;
			anim.SetBool("Exploding",true);
            Instantiate(ExplosionParticles, transform.position, ExplosionParticles.rotation);
            Exploded = true;
            return;
        }
	}
}
