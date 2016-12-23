using UnityEngine;
using System.Collections;

public class BladeDevilScript : MinionScript {

	WeaknessesScript WS;
	public Transform WallCheck2;
	public Transform GroundCheck2;
	public Transform GroundCheck3;
	public Transform GroundCheck4;
	public SpriteRenderer sr;
	public Transform horn;
	public Transform blades;
	public Transform bladePoint;
	public Transform hornPoint;
    BladeDevilSounds BDSounds;
	public Collider2D attackCollider;
	public float attackTimer;

	Rigidbody2D rbody;
	void Awake(){
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		rbody = GetComponent<Rigidbody2D> ();
		WS = GetComponent<WeaknessesScript> ();
	}

    void Start(){
        FindGM();
        StartCoroutine("RegiserEnemy");
        BDSounds = GetComponent<BladeDevilSounds>();
		currentSpeed = defaultSpeed;
		anim.SetBool ("Alive", true);
		setDead(false);
		dying = false;
	}
	
	
	void Update(){
		if (anim.speed == 0) {
			return;		
		}
		if (getDead()) {
			sr.sortingOrder=1;
			return;		
		}
		DeadEndCheck ();
		if (wallFound&&!isAttacking&&grounded) {
			Flip();
			direction*=-1;
		}
		GroundedCheck ();
		Walk ();
		GapJumpCheck ();
		Walk_Run_Check ();
        if (playerSpotted && currentSpeed != MaxSpeed)
        {
           // BDSounds.PlaySnicker();
        }
		searchForPlayers ();
	}
	
	void FixedUpdate(){
		if (getDead()) {
			attackTimer=0;
			return;		
		}
		if(atkCoolingDown){
			AttackCooldown();
		}
		if (isAttacking) {
			attackTimer += Time.deltaTime;
			if (attackTimer > 0.4f && attackTimer < 0.48f) {
				attackCollider.enabled = true;
			} else {
				attackCollider.enabled = false;
			}
		} else {
			attackTimer=0;
		}
		stuckCheck ();
	}



	public void destroy(){
		Destroy (gameObject);
	}
	public void stopAnimating(){
		anim.speed = 0;
	}
	//stop attack animation
	public void endOfAttack(){
		anim.SetBool ("isAttacking", false);
		largeGapSpotted=false;	
		isAttacking = false;
	}
 public void GapJumpCheck(){
		bool gapOk;
		bool check3;
		bool check4;
		bool wallFound;
		Rigidbody2D rbody = GetComponent<Rigidbody2D>();
		foundEdge = Physics2D.OverlapCircle (edgeCheck.position, 0.1f, whatIsGround);
		grounded = Physics2D.OverlapCircle (GroundCheck.position, 0.1f, whatIsGround);
		gapOk = Physics2D.OverlapCircle (GroundCheck2.position, 0.1f, whatIsGround);
		check3 = Physics2D.OverlapCircle (GroundCheck3.position, 0.1f, whatIsGround);
		check4 = Physics2D.OverlapCircle (GroundCheck4.position, 0.1f, whatIsGround);
		wallFound = Physics2D.OverlapCircle (WallCheck2.position, 0.1f, whatIsGround);
		if (!gapOk && !check3 && check4) {
			largeGapSpotted=true;
		}
		if ((!foundEdge && grounded && gapOk&&!atkCoolingDown)&&!wallFound) {
			rbody.AddForce(new Vector2(500*-direction,1500));
			Attack();	
		}else{
			EdgeCheck ();
		}
	}
	public void CreateCorpse(){
		Instantiate (blades, new Vector3(bladePoint.position.x+0.1f,bladePoint.position.y,transform.position.z), Quaternion.identity);
		Instantiate (blades, new Vector3(bladePoint.position.x-0.1f,bladePoint.position.y,transform.position.z), Quaternion.identity);
		Instantiate (horn, hornPoint.position, Quaternion.identity);
	}
	public void HitBy(GameObject other)
	{
        if (other.tag == Tags.attackCollider)
        {
            BDSounds.Play_Hit_Sound();
        }
		if (other.tag == "Lava") {
			rbody.gravityScale=0;
			rbody.velocity=new Vector3(0,-0.6f);
		}
		for(int i=0;i<WS.weakness.Length;i++){
			if (other.tag == WS.weaknessArray[i]&&!getDead()) {
				setDead(true);
                anim.SetBool("Running", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("Walking", false);
            }
		}
	}
}