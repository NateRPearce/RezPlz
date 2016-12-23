using UnityEngine;
using System.Collections;

public class BehindSensor : MonoBehaviour {

	public BladeDevilScript BDS;
	public Transform groundCheck;
	public bool playerFound;
	public LayerMask whatIsPlayer;
	Rigidbody2D Rbody;
	bool gapOk;

	void Start () {
		BDS = GetComponentInParent<BladeDevilScript> ();
		Rbody = GetComponentInParent<Rigidbody2D> ();
	}
	void Update(){
		playerFound = Physics2D.OverlapCircle (transform.position, 2, whatIsPlayer);
	}

	void FixedUpdate(){
		if(playerFound&&!BDS.playerSpotted&&BDS.grounded) {
			gapOk = Physics2D.OverlapCircle (BDS.GroundCheck2.position, 0.1f, BDS.whatIsGround);
			BDS.Flip();
			BDS.direction*=-1;
			if(!BDS.atkCoolingDown&&BDS.grounded){
				if(gapOk){
					Rbody.AddForce(new Vector2(1000*-BDS.direction,1500));
					BDS.Attack();
				}
			}
		}
	}
}
