using UnityEngine;
using System.Collections;

public class LavaScript : GameStateFunctions {
	
	public Transform fireRing;
	Transform[] fireRingList = new Transform[10];
	int currentRing=0;
    public bool ignoreBoss;
	void Start(){
		for (int i=0; i<fireRingList.Length; i++) {
			Transform newRing;
			newRing = Instantiate (fireRing, transform.position, fireRing.rotation) as Transform;
			fireRingList [i] = newRing;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "InstaKillLava"||other.tag=="Fireball") {
			return;
		}
		if(other.GetComponent<Rigidbody2D>()!=null&&(other.tag!="Ground"||other.tag!="Untagged"|| other.tag != "InstaKillLava"|| other.tag != Tags.attackCollider)){
            if (ignoreBoss && (other.tag == "Enemy" || other.tag == Tags.attackCollider))
            {
                return;
            }
           // Debug.Log(other.name);
            Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();
            rbody.gravityScale=0;
            rbody.freezeRotation = true;
            rbody.velocity=new Vector2(0,-1);
		}
        if (other.GetComponentInParent<Rigidbody2D>() != null && (other.tag != "Ground" || other.tag != "Untagged" || other.tag != "InstaKillLava"))
        {
            if (ignoreBoss && (other.tag == "Enemy"|| other.tag == Tags.attackCollider))
            {
                return;
            }
            Rigidbody2D rbody = other.GetComponentInParent<Rigidbody2D>();
            rbody.gravityScale = 0;
            rbody.freezeRotation = true;
            rbody.velocity = new Vector2(0, -1);
        }
        if (other.name == "FeetCollider"||other.tag=="Corpse"||other.name=="LavaTrigger"||other.tag=="Explodable"||other.tag=="Enemy") {
			fireRingList[currentRing].position= new Vector3(other.transform.position.x,transform.position.y+3,-1);
			currentRing += 1;
			if (currentRing == fireRingList.Length) {
				currentRing=0;
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){		
		if (other.tag == "InstaKillLava"||other.tag=="Fireball") {
			return;
		}

		if (other.GetComponent<Rigidbody2D>() != null&&other.tag!="Ground") {
			if(other.tag=="Player"){
				other.GetComponent<Rigidbody2D>().velocity=new Vector2(0,1.2f);
			}
            if (other.tag == "Player" || other.GetComponentInParent<PlayerControls>() != null)
            {
                return;
            }
            other.GetComponent<Rigidbody2D>().velocity=new Vector2(0,-1);
		}
        if (other.GetComponentInParent<Rigidbody2D>() != null && other.tag != "Ground")
        {
            if (other.tag == "Player" || other.GetComponentInParent<PlayerControls>() != null)
            {
                return;
            }
                other.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, -0.5f);
        }
    }

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "InstaKillLava"||other.tag=="Fireball") {
			return;
		}
        if (ignoreBoss && (other.tag == "Enemy" || other.tag == Tags.attackCollider))
        {
            return;
        }
        if (other.GetComponent<Rigidbody2D>()!=null){
			other.GetComponent<Rigidbody2D>().mass=1;
			other.GetComponent<Rigidbody2D>().gravityScale=1;
		}
        if (other.GetComponentInParent<Rigidbody2D>() != null)
        {
            other.GetComponentInParent<Rigidbody2D>().mass = 1;
            other.GetComponentInParent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
