using UnityEngine;
using System.Collections;

public class SkullerBeeBehavior : EnemyDeathScript {

	public Animator anim;
	public bool grounded;
	public float speed;
	public bool attacking;
	public bool attackCoolingDown;
	public Transform groundCheck;
	public Transform patrolPoint1;
	public Transform patrolPoint2;
	public Transform patrolTarget;
	public bool facingRight;
	public LayerMask whatIsGround;
    public Transform playerHolder;
    PlayerHoldScript PHS;
    Patrol pScript;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
        pScript = GetComponent<Patrol>();
        PHS = playerHolder.GetComponent<PlayerHoldScript>();
	}
	
    void Update()
    {
        if (pScript == null)
        {
            return;
        }
        if (patrolPoint1 != pScript.patrolPoint1)
        {
            patrolPoint1 = pScript.patrolPoint1;
        }
        if (patrolPoint2 != pScript.patrolPoint2)
        {
            patrolPoint2 = pScript.patrolPoint2;
        }
    }

	void FixedUpdate () {
	if (dead) {
                playerHolder.GetComponent<Collider2D>().enabled = false;
			if(anim.speed>0){
				grounded=Physics2D.OverlapCircle(groundCheck.position,1f,whatIsGround);
			}
			return;
		}
		if (attacking) {
			Attack();
			return;
        }
        Patrol ();
	}

	public void Patrol(){
		transform.position = Vector3.MoveTowards (transform.position, patrolTarget.position, Time.deltaTime*speed);
		if (transform.position.x == patrolPoint1.position.x) {
			patrolTarget=patrolPoint2;
		}else if(transform.position.x == patrolPoint2.position.x){
			patrolTarget=patrolPoint1;
		}
		if (patrolTarget.position.x > transform.position.x && !facingRight) {
			Flip();
		}
		if (patrolTarget.position.x < transform.position.x && facingRight) {
			Flip();
		}
	}

	public void Hit(bool facingRight){
		anim.SetBool ("Hit", true);
		GetComponent<Rigidbody2D>().isKinematic = false;
		if (facingRight) {
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (1000, 2000));
		} else {
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (-1000, 2000));
		}
		dead = true;
	}


	public void Attack(){
		if (dead) {
			return;
		}
		GetComponent<Rigidbody2D>().isKinematic = false;
		if (facingRight) {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (40, -40);
		} else {
			GetComponent<Rigidbody2D>().velocity = new Vector2 (-40, -40);
		}	
	}

	public IEnumerator AttackCooldown(){
		attackCoolingDown = true;
        PHS.col.enabled = false;
        PHS.DropPlayer();
        yield return new WaitForSeconds (3);
		attackCoolingDown = false;
	}

	public void DoneAttacking(){
        PHS.col.enabled = true; ;
		attacking = false;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().isKinematic = true;
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void stopAnimating(){
		if(grounded){
			anim.speed=0;
		}
	}
}
