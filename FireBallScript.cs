using UnityEngine;
using System.Collections;

public class FireBallScript : Projectiles {

	//public Transform hitEffect;
	public Transform fireExplosion;
	public ParticleSystem PS;
    public ParticleSystem.EmissionModule PE;
    Projectile_Sounds PSounds;
	void Awake () {
		PS = GetComponentInChildren<ParticleSystem> ();
        PE = PS.emission;
		anim = GetComponent<Animator> ();
        PSounds = GetComponent<Projectile_Sounds>();
	}



	public void Deflected(Vector2 direction){
		GetComponent<Rigidbody2D>().velocity=direction;
        PSounds.playCollisionSound();
    }
	public void Destroy(){
        PE.rate = new ParticleSystem.MinMaxCurve(0);
        PS.transform.parent = null;
		Destroy(gameObject);
	}
	public void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Ground"||other.tag=="Player"||other.tag=="HitBox"||other.tag=="Fireball"||other.tag=="Enemy"||other.tag=="MagicBlast") {
            PSounds.playCollisionSound();
            anim.SetBool("Activated",false);
			anim.SetTrigger("Destroy");
			GetComponent<Rigidbody2D>().velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x/10,GetComponent<Rigidbody2D>().velocity.y/10);
		}
		if (other.tag == "Absorb") {
			anim.SetBool("Activated",false);
			anim.SetTrigger("Absorb");
		}
		if (other.name == "FireShrine") {
			transform.position = Vector3.MoveTowards(transform.position,new Vector3(other.transform.position.x,other.transform.position.y-1f,transform.position.z),1);
		}
	}

	public void CreateFireExplosion(){
        PSounds.playCollisionSound();
		Transform newExplosion=	Instantiate (fireExplosion, transform.position, Quaternion.identity)as Transform;
		newExplosion.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x*5,GetComponent<Rigidbody2D>().velocity.y*5);
	}

}
