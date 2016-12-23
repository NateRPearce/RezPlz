using UnityEngine;
using System.Collections;

public class EmbersnailBehaviorScript : MinionScript {

	public LayerMask whatIsObstacle;
	public bool foundWall;

	// Use this for initialization
	void Start(){
        FindGM();
        StartCoroutine("RegiserEnemy");

        anim = GetComponent<Animator> ();
		currentSpeed = defaultSpeed;
		setDead(false);
		dying = false;
	}
	
	// Update is called once per frame
	void Update () {
		DeadEndCheck ();
		foundEdge = Physics2D.OverlapCircle (edgeCheck.position, 0.01f, whatIsGround);
		foundWall = Physics2D.OverlapCircle (WallCheck.position, 0.01f, whatIsObstacle);
		if (foundWall&&!isAttacking||!foundEdge) {
			Flip();
			direction*=-1;
		}
		GroundedCheck ();
		Walk ();
	}
}
