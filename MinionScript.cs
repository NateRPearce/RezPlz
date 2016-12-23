using UnityEngine;
using System.Collections;

public class MinionScript : GameStateFunctions {



	public Animator anim;

	public Transform GroundCheck;
	public Transform WallCheck;

	public float defaultSpeed;
	public float currentSpeed;
	public float MaxSpeed;
	public float atkCooldown;
	public float atkTimer;

	public int direction = 1;

	public bool largeGapSpotted;
	public bool wallFound;
	bool dead;
	public bool facingRight;
	public bool playerSpotted;
	public bool foundEdge;
	public Transform edgeCheck;
	public bool foundBottom;
	public Transform pitCheck;
	public bool grounded;
	public bool isAttacking;
	public bool dying;
	public bool atkCoolingDown;
	public float fallTime;
	public LayerMask whatIsGround;
	public Vector3 playerPos;
	public PlayerControls targetPC;
    int enemyNumber;

    public IEnumerator RegiserEnemy()
    {
        enemyNumber = GM.numberOfEnemies;
        GM.numberOfEnemies++;
        yield return new WaitForSeconds(1f);
        if (GM.CC.enemies.Length != 0)
        {
            GM.CC.enemies[enemyNumber] = transform;
        }
    }


	//attack
	public virtual void Attack(){
		if (atkCoolingDown || isAttacking) {
				return;
		}else{
			anim.SetBool ("isAttacking", true);
			atkCoolingDown=true;
			isAttacking=true;
		}
	}

	//control the rate of attacks	
	public void AttackCooldown(){
		atkTimer+=Time.deltaTime;
		if(atkTimer>atkCooldown){
			playerSpotted=false;
			atkCoolingDown=false;
			atkTimer=0;
		}
	}


	//chase spotted players
	public void searchForPlayers(){
		if (playerSpotted||largeGapSpotted) {
			currentSpeed=MaxSpeed;		
		}else{
			currentSpeed=defaultSpeed;
		}
	}


	public void stuckCheck(){
	if (!grounded && foundEdge) {
			GetComponent<Rigidbody2D>().velocity=new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
		}
	}
//set walking velocity
	public void Walk(){
		if(!isAttacking&&grounded){
			Rigidbody2D rbody = GetComponent<Rigidbody2D>();
			rbody.velocity = new Vector2 (currentSpeed * direction*-1, rbody.velocity.y);
		}
	}

	public void DeadEndCheck(){
		wallFound = Physics2D.OverlapCircle (WallCheck.position, 0.2f, whatIsGround);
	}

//check for edges and control direction
	public void EdgeCheck(){
		foundEdge = Physics2D.OverlapCircle (edgeCheck.position, 0.01f, whatIsGround);
		if (pitCheck != null) {
			foundBottom = Physics2D.OverlapCircle (pitCheck.position, 0.1f, whatIsGround);
		} else {
			foundBottom=false;
		}
			if (grounded&&!isAttacking) {
			if(!foundEdge){
				if(!foundBottom){
				currentSpeed=defaultSpeed;
				Flip ();
				direction*=-1;
				}
			}
		}
	}
	//check for ground
	public void GroundedCheck(){
		grounded = Physics2D.OverlapCircle (GroundCheck.position, 0.3f, whatIsGround);
		anim.SetBool ("Grounded", grounded);
	}

//flip the sprite on the x axis		
	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

//control most animation states
	public void Walk_Run_Check(){
		Rigidbody2D rbody = GetComponent<Rigidbody2D>();
		if (Mathf.Abs(rbody.velocity.x) > 0&&currentSpeed==defaultSpeed) {
			anim.SetBool ("Walking",true);		
		}else{
			anim.SetBool ("Walking",false);	
		}
		if (Mathf.Abs(rbody.velocity.x) > 0&&currentSpeed==MaxSpeed) {
			anim.SetBool ("Running",true);		
		}else{
			anim.SetBool ("Running",false);
		}
	}
	public bool getDead(){
		return dead;
	}

	public void setDead(bool tempDead){
		dead = tempDead;
		anim.SetBool ("Alive", !tempDead);
	}
}
