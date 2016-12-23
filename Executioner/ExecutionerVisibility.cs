using UnityEngine;
using System.Collections;

public class ExecutionerVisibility : MonoBehaviour {

	public Transform visionPoint;
	public Transform attackPoint;
	public Transform attackPoint2;
	public ExecutionerBehavior EB;
	public LayerMask whatIsPlayer;
	bool attackCheck1;
	bool attackCheck2;
	// Use this for initialization
	void Start () {
		EB = GetComponent<ExecutionerBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
		EB.playerSpotted = Physics2D.OverlapCircle (visionPoint.position, 10f, whatIsPlayer);
		EB.wallFound = Physics2D.OverlapCircle (attackPoint.position, 1f, EB.whatIsGround);
		if (!EB.isAttacking) {
			attackCheck1 = Physics2D.OverlapCircle (attackPoint.position, 5f, whatIsPlayer);
			attackCheck2 = Physics2D.OverlapCircle (attackPoint2.position, 5f, whatIsPlayer);
		}
		if (attackCheck1 || attackCheck2) {
			EB.isAttacking = true;
		} else {
			EB.isAttacking = false;
		}
	}
}
