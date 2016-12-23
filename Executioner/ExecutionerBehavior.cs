using UnityEngine;
using System.Collections;

public class ExecutionerBehavior : MinionScript {

	public bool startingDirection;
	public Vector3 startingPos;
    PlayerLauncher PL;
    ExecutionerCollision EC;
    Rigidbody2D rbody;         
	// Use this for initialization
	void Start () {
        FindGM();
        rbody = GetComponent < Rigidbody2D>();
        StartCoroutine("RegiserEnemy");
        EC = GetComponentInChildren<ExecutionerCollision>();
        startingDirection = facingRight;
		startingPos = transform.position;
		anim = GetComponent<Animator> ();
        PL = GetComponentInChildren<PlayerLauncher>();
	}
	// Update is called once per frame
	void Update () {
		foundEdge = Physics2D.OverlapCircle (edgeCheck.position, 0.01f, whatIsGround);
		grounded = Physics2D.OverlapCircle (GroundCheck.position, 0.3f, whatIsGround);
		if (Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x) > 0.1f&&!isAttacking) {
			anim.SetBool ("Walk", true);
		} else {
			anim.SetBool("Walk",false);
		}
		if (isAttacking) {
			direction = 0;
			anim.SetBool ("isAttacking", true);
		} else if(!atkCoolingDown){
			anim.SetBool ("isAttacking", false);
			if (atkCoolingDown) {
				direction = 0;
				AttackCooldown ();
			} else {
				if (playerSpotted&&!wallFound) {
					Walk ();
					currentSpeed = defaultSpeed;
					if (facingRight) {
						direction = -1;
					} else if (!facingRight) {
						direction = 1;
					}
				} else if(!atkCoolingDown){
					returnToStartPos ();
				}
				if (!foundEdge) {
                    PL.xForce *= -1;
                    Flip ();
					direction*=-1;
				}
			}
		}
	}
	void FixedUpdate () {
		if (atkCoolingDown) {
			AttackCooldown();
		}
	}

	public void EndOfAttack(){
		atkCoolingDown = true;
		isAttacking = false;
		anim.SetBool ("isAttacking", false);
	}

	public void returnToStartPos(){
		if(transform.position.x>startingPos.x+2){
				if(facingRight){
                    PL.xForce *= -1;
                    Flip();
				}
			direction=-1;
			rbody.velocity = new Vector2 (defaultSpeed * direction,rbody.velocity.y);
		}else if(transform.position.x<startingPos.x-2){
				if(!facingRight){
                    PL.xForce *= -1;
					Flip();
				}
			direction=1;
			rbody.velocity = new Vector2 (defaultSpeed * direction, rbody.velocity.y);
			}else{
				if(facingRight!=startingDirection){
                    PL.xForce *= -1;
                    Flip ();
				}
				direction=0;
			}
	}

    public void FullDead()
    {
        EC.DisableCols();
    }
}
