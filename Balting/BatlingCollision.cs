using UnityEngine;
using System.Collections;

public class BatlingCollision : DeathScript {

	public BatlingBehavior BB;
    GibCorpse GB;
    bool dead;
    void Start () {
		BB = GetComponentInParent<BatlingBehavior> ();
        GB = GetComponentInParent<GibCorpse>();
	}

	void OnTriggerEnter2D(Collider2D other){
	if (other.tag == Tags.attackCollider) {
			BB.hitByPlayer=true;
			BB.GetComponent<Rigidbody2D>().freezeRotation = false;
			PlayerControls PC=other.GetComponentInParent<PlayerControls>();
			Rigidbody2D rbody=GetComponentInParent<Rigidbody2D>();
			Animator anim=GetComponentInParent<Animator>();
			if(PC.facingRight){
				anim.speed=0;
				rbody.velocity=new Vector2(0,0);
				if(BB.grounded){
					rbody.AddForce(new Vector2(1000,500));
				}else{
					rbody.AddForce(new Vector2(1000,2000));
				}
				anim.speed=1;
			}else{
				anim.speed=0;
				rbody.velocity=new Vector2(0,0);
				if(BB.grounded){
					rbody.AddForce(new Vector2(-1000,500));
				}else{
				rbody.AddForce(new Vector2(-1000,2000));
				}
				anim.speed=1;
			}
		}
	
	}
	
	public override void HitBy (string OBJ)
	{
        if (dead)
        {
            return;
        }


		if (OBJ == "AttackCollider" || OBJ == "Fireball"|| OBJ == Tags.deathSplit||OBJ==Tags.explosion) {
            if (OBJ == Tags.explosion)
            {
                BB.anim.SetTrigger("Exploded");
                GB.createGiblets();
                dead = true;
            }
            BB.hitByPlayer=true;
			BB.GetComponent<Rigidbody2D>().freezeRotation = false;
		}
	}
}
