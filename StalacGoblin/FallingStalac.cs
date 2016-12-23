using UnityEngine;
using System.Collections;

public class FallingStalac : MonoBehaviour {

	public string[] triggers = new string[1];
	ExplosionScript ES;
	Animator anim;
	Rigidbody2D rbody;
	public AudioClip hitGroundSound;
	private AudioSource source;

	void Start(){
		rbody = GetComponentInParent<Rigidbody2D> ();
		anim = GetComponentInParent<Animator> ();
		source = GetComponent<AudioSource> ();
		if (GetComponent<ExplosionScript> () != null) {
			ES = GetComponent<ExplosionScript> ();
		}
	}

	void Update () {
		if (rbody.velocity.y < -70) {
			rbody.velocity=new Vector2(0,-70);		
		}
		if (rbody.velocity.y < -1) {
			gameObject.tag="Explosion";		
		}else{
			ES.noBlood=true;
			gameObject.tag="Ground";
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		for(int i=0;i<triggers.Length;i++){
			if (other.tag == triggers[i]) {
				rbody.velocity=new Vector2(0,0);
				rbody.isKinematic=true;
				GetComponent<Collider2D>().isTrigger=false;
				anim.SetBool("HitGround",true);
				source.PlayOneShot(hitGroundSound,0.1f);
				ES.exploded=true;
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Explodable") {
			other.GetComponent<Rigidbody2D>().AddForce(new Vector2(600*(other.transform.position.x-transform.position.x),600*(other.transform.position.y-transform.position.y)));
		}
	}
}
