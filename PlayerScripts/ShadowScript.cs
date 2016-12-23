using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour {

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundPoint;
	public PlayerControls PC;
	public PlayerDeathScript PDS;
	public bool dead;
	// Use this for initialization
	void Start () {
		PC = GetComponentInParent<PlayerControls> ();
		PDS = GetComponentInParent<PlayerDeathScript> ();
	}
	
	void FixedUpdate(){
		if (PC.eidolonMode) {
			GetComponent<Renderer>().enabled=false;
			return;
		}
		grounded=	Physics2D.OverlapCircle (groundPoint.transform.position, 0.1f, whatIsGround);
		if (grounded&&PC.grounded&&!dead&&!PC.isBlinking) {
			GetComponent<Renderer>().enabled=true;		
		}else{
			GetComponent<Renderer>().enabled=false;
		}
		dead = PDS.playerDead;
	}
}
