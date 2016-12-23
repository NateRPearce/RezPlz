using UnityEngine;
using System.Collections;

public class FlyingMinionScript : EnemyDeathScript {

    public LayerMask whatIsGround;
	public Transform groundCheck;
	public Collider2D bodyCollider;
	public Transform AttackCollider;
	public Transform facingWallCheck;
    public Transform enterancePoint;

    [HideInInspector]
	public Rigidbody2D rBody;
	[HideInInspector]
	public Animator anim;
	[HideInInspector]
	public float flapTime,xSpeed,yspeed,atkCooldown,atkTimer,playerPosX,playerPosY;

//wall sensors and bools
	public Transform WestSensor;
	public Transform NorthSensor;
	public Transform SouthSensor;

	public bool playerIsNorth,wallFound,grounded,dying,isAttacking,playerIsSouth,playerIsEast,playerIsWest,wallFoundN,wallFoundE, wallFoundS,wallFoundW,hitByPlayer,facingRight,playerSpotted;

	public void WallCheckFront(){
		wallFound = Physics2D.OverlapCircle (facingWallCheck.position, 0.2f, whatIsGround);
	}

	public void CheckForGround(){
		grounded = Physics2D.OverlapCircle (groundCheck.position, 2, whatIsGround);
	}

	public void CheckForWalls(){
		wallFoundS = Physics2D.OverlapCircle (SouthSensor.position, 0.1f, whatIsGround);
		wallFoundN = Physics2D.OverlapCircle (NorthSensor.position, 0.1f, whatIsGround);
		if (facingRight) {
			wallFoundE = Physics2D.OverlapCircle (WestSensor.position, 0.1f, whatIsGround);
			wallFoundW=false;
		} else {
			wallFoundW = Physics2D.OverlapCircle (WestSensor.position, 0.1f, whatIsGround);
			wallFoundE=false;
		}

	}

	public void FlapNE(float flapStrength){

		rBody.AddForce (new Vector2 (flapStrength, flapStrength));
	}
	public void FlapSE(float flapStrength){
		rBody.AddForce (new Vector2 (flapStrength, -flapStrength));
	}
	public void FlapSW(float flapStrength){
		rBody.AddForce (new Vector2 (-flapStrength, -flapStrength));
	}
	public void FlapNW(float flapStrength){
		rBody.AddForce (new Vector2 (-flapStrength, flapStrength));
	}
	public void FlapN(float flapStrength){
		rBody.AddForce (new Vector2 (0, flapStrength));
	}
	//flip the sprite on the x axis		
	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
