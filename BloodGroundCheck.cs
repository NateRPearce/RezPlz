using UnityEngine;
using System.Collections;

public class BloodGroundCheck : MonoBehaviour {
	public bool groundFoundL;
	public bool groundFoundR;
	public Transform groundCheckL;
	public Transform groundCheckR;
	public LayerMask whatIsGround;
	public bool activated;

	// Update is called once per frame
	void Update () {
		groundFoundL=	Physics2D.OverlapCircle (groundCheckL.transform.position, 0.1f, whatIsGround);
		groundFoundR=	Physics2D.OverlapCircle (groundCheckR.transform.position, 0.1f, whatIsGround);
		if (activated&&!groundFoundL || !groundFoundR) {
			Debug.Log("puddle Destroyed");
			Destroy(gameObject);		
		}
		if (groundFoundL || groundFoundR) {
			activated=true;
		}
	}
}
