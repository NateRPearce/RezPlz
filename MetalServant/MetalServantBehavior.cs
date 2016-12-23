using UnityEngine;
using System.Collections;

public class MetalServantBehavior : MinionScript {


	public Transform batParticles;
	public Transform playerOverGroundPoint; 
	public bool isPlayerOverGround;
	public Transform Fireball;
	public Transform FirePoint;
	public SpriteRenderer sr;
	public float projectileSpeed;
	public Collider2D visionCollider;
	public Transform playerAboveGroundPoint;
	public bool clawBlocked;
	public Transform batPoint;

	float clawPoint;

	void Awake(){
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}
	void Start(){
        FindGM();
        StartCoroutine("RegiserEnemy");

        currentSpeed = defaultSpeed;
		anim.SetBool ("Alive", true);
		setDead (false);
		dying = false;
	}
	
	
	void Update(){
		if (anim.speed == 0) {
			return;		
		}
		if (getDead ()) {
			sr.sortingOrder=1;
			return;		
		}
		DeadEndCheck ();
		if (wallFound) {
			Flip();
			direction*=-1;
		}
		GroundedCheck ();
		EdgeCheck ();
		Walk_Run_Check ();
		searchForPlayers ();
	}
	
	void FixedUpdate(){
		if(atkCoolingDown){
			AttackCooldown();
		}
	}
	public void destroy(){
		Destroy (gameObject);
	}
	//stop attack animation
	public void endOfAttack(){
		anim.SetBool ("isAttacking", false);
		isAttacking = false;
	}
	public void stopAnimating(){
		anim.speed = 0;
	}
	
	public void FireBreathe(){
		Instantiate (Fireball, new Vector3 (clawPoint, FirePoint.position.y, Fireball.position.z), Fireball.rotation);
	}


	public override void Attack ()
	{
		PlayerOverGroundCheck ();
		if (!isPlayerOverGround||clawBlocked) {
			StartCoroutine("TempDisable");
		}

		if (atkCoolingDown || isAttacking) {
			return;
		}else if(isPlayerOverGround&&!clawBlocked){
			anim.SetBool ("isAttacking", true);
			atkCoolingDown=true;
			isAttacking=true;
		}
	}
	void PlayerOverGroundCheck(){
		playerOverGroundPoint.position=new Vector3 (playerPos.x, playerOverGroundPoint.position.y, FirePoint.position.z);
		playerAboveGroundPoint.position=new Vector3 (playerPos.x, playerAboveGroundPoint.position.y, FirePoint.position.z);
		clawPoint = playerPos.x+(targetPC.GetComponent<Rigidbody2D>().velocity.x+targetPC.runningMomentum)/2;

		if (!facingRight && clawPoint > transform.position.x) {
			clawPoint=transform.position.x-3;
		}

		if (facingRight && clawPoint < transform.position.x) {
			clawPoint=transform.position.x+3;
		}

		isPlayerOverGround = Physics2D.OverlapCircle (new Vector3(clawPoint,playerOverGroundPoint.position.y,playerOverGroundPoint.position.z), 0.1f,whatIsGround);
		clawBlocked=Physics2D.OverlapCircle (new Vector3(clawPoint,playerAboveGroundPoint.position.y,playerOverGroundPoint.position.z), 0.1f,whatIsGround);
	}

	IEnumerator TempDisable(){
		visionCollider.enabled = false;
		yield return new WaitForSeconds (0.05f);
		visionCollider.enabled = true;
	}
	public void CreateBats(){
		//ParticleSystem PS = batParticles.GetComponent<ParticleSystem> ();
		//PS.startSpeed = (transform.localScale.x/10)*15;
		Instantiate (batParticles, batPoint.position,transform.rotation);
	}

}
