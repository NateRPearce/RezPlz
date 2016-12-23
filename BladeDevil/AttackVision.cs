using UnityEngine;
using System.Collections;

public class AttackVision : MonoBehaviour {


	public BladeDevilScript BDS;
	Rigidbody2D Rbody;
	
	void Awake(){
		BDS = GetComponentInParent<BladeDevilScript> ();
		Rbody = GetComponentInParent<Rigidbody2D> ();
	}
	
	
	void OnTriggerStay2D(Collider2D other){
		if (BDS.getDead()) {
			return;		
		}
		if (other.tag == "Player") {
			bool gapOk;
			gapOk = Physics2D.OverlapCircle (BDS.GroundCheck2.position, 0.1f, BDS.whatIsGround);
            float xDist;
			xDist = Mathf.Abs(other.transform.position.x-transform.position.x);
			if(!BDS.atkCoolingDown&&BDS.grounded){
				if(xDist<5){
					Rbody.AddForce(new Vector2(500*-BDS.direction,500));
				}else{
					if(!gapOk){
						return;
					}
					Rbody.AddForce(new Vector2(500*-BDS.direction,1500));
				}
				BDS.Attack();
			}
		}
	}
}
