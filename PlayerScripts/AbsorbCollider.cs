using UnityEngine;
using System.Collections;

public class AbsorbCollider : MonoBehaviour {

	bool absorbed;
	float reload;
	LightDetectionScript LDS;
	PlayerControls PC;
	knockBackScript playerKBS;
	PlayerDeathScript PDS;

	void Start(){
		playerKBS = GetComponentInParent<knockBackScript> ();
		PC = GetComponentInParent<PlayerControls> ();
		PDS = GetComponentInParent<PlayerDeathScript> ();
		LDS = PC.BodyCollider.GetComponent<LightDetectionScript> ();
	}

	void FixedUpdate(){
	if (absorbed) {
			reload+=Time.deltaTime;
			if(reload>0.4f){
				absorbed=false;
				reload=0;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<FireBallScript>()!=null&&!absorbed) {
			FireBallScript FBS = other.GetComponent<FireBallScript>();
			PC.absorbMagicTimer=1;
			PDS.attackDeflected=true;
			other.GetComponent<Collider2D>().enabled=false;
			other.GetComponent<Rigidbody2D>().velocity=new Vector2(other.GetComponent<Rigidbody2D>().velocity.x/1000,other.GetComponent<Rigidbody2D>().velocity.y/1000);
			FBS.anim.SetBool("Activated",false);
			FBS.anim.SetTrigger("Absorb");
			//other.transform.position=Vector3.MoveTowards(other.transform.position,new Vector3(transform.position.x,transform.position.y,-1),50);
			if(PC.facingRight){
				playerKBS.Launched(-2000,0);
			}else{
				playerKBS.Launched(2000,0);
			}
			LDS.energyAbsorbed=true;
			absorbed=true;
		}
	}
}
