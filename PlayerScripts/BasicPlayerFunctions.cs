using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BasicPlayerFunctions : PlayerInheritance {

    public Collider2D smashCol;
    public SpriteRenderer mask;
	public Transform cape;
	public Transform StoneSkinFlakes;
	public Transform launchSmoke;
	public Transform landingSmoke;
	public Transform RezGlow;
	public Transform groundSlideClouds;
	public Transform blinkAppear;
	public Transform trueParent;
	public Transform AblilityBar;
	public Transform rezPoint;
    public Transform rezPointRev;
    public Transform rezPoint2;
	public Transform rezPoint3;
	public Transform wallPoint2;
	public Transform wallPoint1;
	public Transform BlinkPoint;
	public Transform BlinkPoint2;
	public Transform HangCollider;
	public Transform AttackCollider;
	public Transform BodyCollider;
	public Transform FeetCollider;
	public Transform HeadCollider;
	public Transform dragSmoke;
	public Transform ledgePoint;
	public Transform groundPoint1;
	public Transform groundPoint2;
	public Transform groundPoint3;
	public Transform otherPlayer;
	public Transform player1;
	public Transform player2;
	public Transform onFireCollider;
    public Transform dragonPoint;
    public Transform floatDragon;
    public FollowScript dragonFollow;
    public Animator dragonAnim;
    public float jumpCooldown;
	public ParticleSystem momentumParticles;
    ParticleSystem.EmissionModule mpem;

	public AbilitiesBar AB;
	public PlayerControls otherPC;
	public PlayerSounds PSS;
	public CrystalColors CC;
	public RemoteTriggerScript FireBallRTS;
	public ProjectileSpawner PSpawner;
	public OopTriggerScript OTS;
	public SpiritScript SpiritS;


    public Animator anim;
	public Animator capeAnim;

	public Text ablText;

	public Vector3 StoneSkinOffset;

	public SpriteRenderer CrystalSprite;
	public SpriteRenderer rezSprite;





	public LayerMask whatIsPlayer;
	public LayerMask whatIsGround;
	public LayerMask whatIsIce;
	public LayerMask whatIsWall;
	public LayerMask whatIsLedge;
    public LayerMask whatIsEnemy;

	public string L1;
	public string R1;
	public string jStickVertical;
	public string jumpbtn;
	public string movebtn;
	public string attackbtn;
	public string grabbtn;
	public string givebtn;
	public string Rezbtn;
	public string switchPlayersbtn;
	public string slide;
	public string ablControlsUp;
	public string ablControlsDown;
	public string spellCastY;
	public string spellCastX;

    public bool cronened;
    bool floating;
    public bool flipDisabled;
	public bool isBlinking;
	public bool onIce;
	public bool IceCheck1;
	public bool IceCheck2;
	public bool CanRez;
	public bool hideAbls;
	public bool attackCoolingDown;
	public bool Ooping;
	public bool GivingCrystal;
	public bool hanging;
	public bool hangDisabled;
	public bool summoning;
	public bool rezing;
	public bool dying;
	public bool isPlayer1;
	public bool firstJump;
	public bool swinging;
	public bool climbing;
    public float climbTime;
	public bool facingRight = true;
	public bool onFire;
	public bool bisected;
	public bool fullyBisected;
	public bool wallJumpR;
	public bool wallJumpL;
	public bool wallJumpEnabled;
	public bool wallJumping;
	public bool impact;
	public bool playerCheck1;
	public bool playerCheck2;
	public bool groundCheck1;
	public bool groundCheck2;
	public bool groundCheck3;
	public bool grounded;
	public bool jumpLocked;
	public bool moveLocked;
	public bool ledgeNear;
	public bool grabbingLedge;
	public bool ledgeCheck;
	public bool ledgeFailCheck;
	public bool isTouchingWall;
	public bool iceWallCheck;
	public bool wallCheck1;
	public bool wallCheck2;
    public bool enemyCheck;
	public bool isAttacking = false;
	public bool absorbing;
	public bool BlinkBlockedF;
	public bool BlinkBlockedB;
	public bool rezPlatform1;
	public bool rezPlatform2;
    public bool rezPlatform1Rev;
    public bool rezPlatform2Rev;
    public bool rezBlocked;
    public bool rezBlocked2;
    public bool launching;
	public bool onTeleporter;
	public bool onButton;
	public bool teleporting;
	public bool ControlsEnabled;
	public bool exploding;
	public bool stoneSkinActivated;
	public bool stoneSkinCoolingdown;

	public float blinkDir;
	public float ablTextAlpha;
	public float rezDelay;

	public float stoneSkinTimer;
	public float absorbMagicTimer;
	public float wallJumpTimer;
	public float hangTimer;
	public float wallHoldTimer;
	public float idleTimer;
	public float wallDragTimer;
	public float blinkTimer;
	public float atkTimer;

	public float fallSpeed;
	public float timeGrounded;
	public float timeAirborne;
	public float groundRadius;
	public float atkCooldown;
	public float suicideTimer;
	public float slideInput;
	public float maxSpeed;
	public float runningMomentum;
	public float onFireSpeed;
	public float move;
	public float vertInput;
	public float jumpForce = 45f;
	public int currentAbility;
	int FailedRezAttempts;
	int rezAttemptCounter;
	public Quaternion startingRotation;
    public Rigidbody2D rbody;


	//assign child objects to their apropriate variables
	public void findChildren(){  
		OTS = GetComponentInChildren<OopTriggerScript> ();
		groundPoint2 = transform.FindChild ("groundPoint2");
		groundPoint1 = transform.FindChild ("groundPoint1");
		groundPoint3 = transform.FindChild ("groundPoint3");
		wallPoint2 = transform.FindChild ("wallPoint2");
		wallPoint1 = transform.FindChild ("wallPoint1");
		dragSmoke = transform.FindChild ("wallslidecloud");
		BodyCollider = transform.FindChild ("BodyCollider");
		FeetCollider = transform.FindChild ("FeetCollider");
		HeadCollider = transform.FindChild ("HeadCollider");
		ledgePoint = transform.FindChild ("LedgePoint");
		rezPoint = transform.FindChild ("rezPoint");
		rezSprite = rezPoint.GetComponent<SpriteRenderer> ();
		rezPoint2 = transform.FindChild ("rezPoint2");
		rezPoint3 = transform.FindChild ("rezPoint3");
        rbody = GetComponent<Rigidbody2D>();
        mpem = momentumParticles.emission;
	}
	
//attacks
	public void AttackUp(){
		if(!attackCoolingDown&&!hanging&&!swinging){
			anim.SetBool ("isAttackingUp", true);
			attackCoolingDown=true;
			isAttacking=true;
		}
	}


	public void AttackDown(){
		if(!attackCoolingDown&&!hanging&&!swinging){
			if(!grounded){
				if(anim.GetBool("AlleyOopToss")){
					StopGrabbing();
				}
				anim.SetBool ("isAttackingDown", true);
				attackCoolingDown=true;
				isAttacking=true;
			}
		}
	}
	public void endOfBlink(){
        cronened = true;
        isBlinking = false;
        rbody.isKinematic = false;
        rbody.gravityScale = 1;
		Instantiate (blinkAppear, transform.position, Quaternion.identity);
        anim.SetBool("Blinking", false);
        anim.SetBool("Cronen", true);
    }

    public void AttemptBlink(float slideInput){        
		if (!AB.CrystalList[0].enabled || !AB.CrystalList[0].unlocked||isBlinking||cronened) {
			return;
		}
		if (slideInput < -0.5f && facingRight && BlinkBlockedF) {
			return;
		}
		if (slideInput > 0.5f && !facingRight && BlinkBlockedF) {
			return;
		}
		if (slideInput < -0.5f && !facingRight && BlinkBlockedB) {
			return;
		}
		if (slideInput > 0.5f && facingRight && BlinkBlockedB) {
			return;
		}
		if (slideInput >0.5f||slideInput <-0.5f) {
            isBlinking = true;
            rbody.velocity = new Vector2(0, 0);
            rbody.gravityScale = 0;
            rbody.isKinematic = true;
			anim.SetBool ("Blinking", true);
            if (slideInput > 0.1f)
            {
                if (facingRight)
                {
                    Flip();
                }
                blinkDir = -1f;
            }
            else if (slideInput < -0.1)
            {
                if (!facingRight)
                {
                    Flip();
                }
                blinkDir = 1f;
            }

        }
    }


    public void lockAll()
    {
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

	public void AttemptAbsorb(float slideInput){
		if (!AB.CrystalList[4].enabled || !AB.CrystalList[4].unlocked||!grounded) {
			StopAbsorbing();
			return;
		}
		if (slideInput > 0.1f) {
			if (facingRight) {
				Flip ();
			}
			absorbing = true;
		} else if (slideInput < -0.1) {
			if (!facingRight) {
				Flip ();
			}
			absorbing = true;
		} else if(slideInput==0) {
			StopAbsorbing();
		}
	}

	public void StopAbsorbing(){
		anim.SetBool ("HoldingAbsorb", false);
		absorbing=false;
	}
	public void StoneSkinCooldown(){
		stoneSkinTimer += Time.deltaTime;
		if (stoneSkinTimer > 0.3f) {
			stoneSkinTimer=0;
			stoneSkinCoolingdown=false;
		}
	}
	public void TurnToStone(float slideInput){
		if (!AB.CrystalList[1].enabled || !AB.CrystalList[1].unlocked) {
			anim.SetBool ("Stoned", false);
			anim.SetBool ("FullStone", false);
			return;
		}
		if (stoneSkinCoolingdown) {
			return;
		}
		if(slideInput<-0.1f&&anim.GetBool ("FullStone")&&Input.GetAxis(spellCastX)==0&&Input.GetAxis(spellCastY)==0){
			if(stoneSkinActivated){
                    PSS.playStoneOff();
                Instantiate(StoneSkinFlakes,new Vector3(transform.position.x+StoneSkinOffset.x,transform.position.y+StoneSkinOffset.y,-12),Quaternion.identity);
			}
			stoneSkinCoolingdown=true;
			stoneSkinActivated=false;
            rbody.mass=1;
            if (name == "Player1")
            {
                GM.ZHB.anim.SetBool("Stoned", false);
            }
            else
            {
                GM.AHB.anim.SetBool("Stoned", false);
            }
            anim.SetBool ("Stoned", false);
			anim.SetBool ("FullStone", false);
            flipDisabled = false;
        }
        else if(slideInput<-0.1f&&!anim.GetBool ("FullStone")&&Input.GetAxis(spellCastX)==0&&Input.GetAxis(spellCastY)==0){
			stoneSkinActivated=true;
            rbody.mass=50;
            flipDisabled = true;
            if (name=="Player1")
            {
                GM.ZHB.anim.SetBool("Stoned", true);
            }else
            {
                GM.AHB.anim.SetBool("Stoned", true);
            }
            anim.SetBool ("Stoned", true);
		}
	}
	public void fullStone(){
			anim.SetBool ("FullStone", true);
	}

	public void Attack(){
		if(!attackCoolingDown&&!hanging&&!swinging){
			if (AB.CrystalList[2].enabled && AB.CrystalList[2].unlocked) {
				SpiritS.anim.SetBool("Attacking",true);
			}else{
				SpiritS.anim.SetBool("Attacking",false);
			}
			if(grounded){
				runningMomentum=0;
			}
			anim.SetBool ("isAttacking", true);
			attackCoolingDown=true;
			isAttacking=true;
		}
	}

	//rez other player
	public void Summon(){
		if (!rezBlocked && grounded && rezPlatform1 && rezPlatform2) {
			//move character to rez point
			if (isPlayer1 && PM.PDS2.playerDead&&(GM.MB[0].mana>0||GM.MB[1].mana>0)) {
				summoning = true;
				anim.SetBool ("Summoning", true);
			} else if (!isPlayer1 && PM.PDS1.playerDead&&(GM.MB[0].mana>0||GM.MB[1].mana>0)) {
				summoning = true;
				anim.SetBool ("Summoning", true);
			}
		} else if(!rezBlocked2 && grounded && rezPlatform1Rev && rezPlatform2Rev)
        {
            if (isPlayer1 && PM.PDS2.playerDead && (GM.MB[0].mana > 0 || GM.MB[1].mana > 0))
            {
                summoning = true;
                anim.SetBool("Summoning", true);
            }
            else if (!isPlayer1 && PM.PDS1.playerDead && (GM.MB[0].mana > 0 || GM.MB[1].mana > 0))
            {
                summoning = true;
                anim.SetBool("Summoning", true);
            }
        } else if (otherPC.PDS.playerDead&&grounded){
            if(timeGrounded>0.1f)
			anim.SetBool("FailedRez",true);
            PSS.PlayFailedRezSound();
			FailedRezAttempts+=1;
			if(FailedRezAttempts>rezAttemptCounter){
				if(isPlayer1){
					GM.HD[0].Talk("Not enough room to rez",133,43,-130,19,2);
				}else{
					GM.HD[1].Talk("Not enough room to rez",133,43,130,19,2);
				}
				rezAttemptCounter=FailedRezAttempts+FailedRezAttempts;
			}
		}
	}
	//release ledge for 0.2f seconds
	public void LedgeReleaseCooldown(){
		if (hangTimer < 0.4f) {
			hangTimer += Time.deltaTime;
		}else{
			hangTimer = 0;
			hangDisabled = false;
		}	
	}

//control the rate of attacks	
	public void AttackCooldown(){
		atkTimer+=Time.deltaTime;
		if (atkTimer > atkCooldown) {
			atkTimer = 0;
			attackCoolingDown = false;
			isAttacking = false;
		}

	}

//controls max jump velocity
	public void jumpCheck(){
		if (rbody.velocity.y > 50) {
          //  rbody.velocity=new Vector2(rbody.velocity.x,50);		
		}
	}
	//check if other player is dead before starting the rez process
	public void RezBlockedCheck(){
		rezBlocked = Physics2D.OverlapCircle (rezPoint.position, 0.25f, whatIsGround);
        rezPlatform1 = Physics2D.OverlapCircle (rezPoint2.position, 0.02f, whatIsGround);
		rezPlatform2 = Physics2D.OverlapCircle (rezPoint3.position, 0.02f, whatIsGround);
        rezPoint2.localPosition = new Vector3(rezPoint2.localPosition.x * -1, rezPoint2.localPosition.y, rezPoint2.localPosition.z);
        rezPoint3.localPosition = new Vector3(rezPoint3.localPosition.x * -1, rezPoint3.localPosition.y, rezPoint3.localPosition.z);
        rezBlocked2 = Physics2D.OverlapCircle(rezPointRev.position, 0.25f, whatIsGround);
        rezPlatform1Rev = Physics2D.OverlapCircle(rezPoint2.position, 0.02f, whatIsGround);
        rezPlatform2Rev = Physics2D.OverlapCircle(rezPoint3.position, 0.02f, whatIsGround);
        rezPoint2.localPosition = new Vector3(rezPoint2.localPosition.x * -1, rezPoint2.localPosition.y, rezPoint2.localPosition.z);
        rezPoint3.localPosition = new Vector3(rezPoint3.localPosition.x * -1, rezPoint3.localPosition.y, rezPoint3.localPosition.z);
    }

    //check if rez point is blocked
    public void AttemptRez(){
		if(summoning&&rezDelay>0.95f){
			//move character to rez point
			if(isPlayer1&&PM.PDS2.playerDead&&GM.MB[0].mana>=0){
				GM.camNum=1;
                otherPC.lockAll();
                if (!rezBlocked && grounded && rezPlatform1 && rezPlatform2)
                {             
                    otherPlayer.position = rezPoint.position;
                }else
                {
                    otherPlayer.position = rezPointRev.position;
                }
                PM.PC2.rezing=true;
			}else if(!isPlayer1&&PM.PDS1.playerDead&&GM.MB[1].mana>=0){
				GM.camNum=2;
                otherPC.lockAll();
                if (!rezBlocked && grounded && rezPlatform1 && rezPlatform2)
                {
                    otherPlayer.position = rezPoint.position;
                }
                else
                {
                    otherPlayer.position = rezPointRev.position;
                }
                PM.PC1.rezing=true;
			}
		}
	}

//wall jump	
	public void WallJump(float jumpForce){
		if(wallJumpEnabled&&!onFire&&!bisected)
		{
			wallJumping=true;
			runningMomentum=0.1f;
			if (wallJumping) {
                if (enemyCheck && (!wallJumpL || !wallJumpR))
                {
                    wallJumpL = true;
                    wallJumpR = true;
                }
                if (facingRight&&wallJumpR&&!iceWallCheck) {
                    if (grounded)
                    {
                        return;
                    }
                    StartCoroutine("LaunchL");
					if(!hanging){Flip ();}
				} else if(!facingRight&&wallJumpL&&!iceWallCheck){
                    if (grounded)
                    {
                        return;              
                    }
                    StartCoroutine("LaunchR");
					if(!hanging){Flip ();}
				}
			}
		}
	}
	

//wall drag
	public void WallDrag(float move){
		if(onFire||bisected){
			return;
		}
		if(wallDragTimer>0&&!hanging&&!ledgeCheck&&!grounded){
			dragSmoke.GetComponent<Renderer>().enabled=true;
		}else{
			dragSmoke.GetComponent<Renderer>().enabled=false;
		}
		if (wallJumpEnabled && wallCheck2 && wallCheck1&&!cronened) {
            anim.SetBool("WallDragging",true);
			if((move > 0.5f||move<-0.5f)&&rbody.velocity.y<-1&&!wallJumping&&!iceWallCheck){
			rbody.velocity=new Vector2(GetComponent<Rigidbody2D>().velocity.x,-5);
                if (!GetComponent<AudioSource>().loop)
                {
                    PSS.playWallDragSound();
                }
            }
		}else{
            PSS.stopWallDragSound();
            anim.SetBool("WallDragging",false);
		}
	}

    public IEnumerator LaunchR()
    {
        moveLocked = true;
        rbody.velocity = new Vector2(0, 0);
        if (hanging)
        {
            rbody.AddForce(new Vector2(0, 45), ForceMode2D.Impulse);
        }else {
            rbody.AddForce(new Vector2(40, 45), ForceMode2D.Impulse);
        }
        wallJumpL = false;
        wallJumpR = true;
        PSS.PlayJumpSound();
        yield return new WaitForSeconds(0.1f);
        moveLocked = false;
    }
    public IEnumerator LaunchL()
    {
        moveLocked = true;
        rbody.velocity = new Vector2(0, 0);
        if (hanging)
        {
            rbody.AddForce(new Vector2(0, 45), ForceMode2D.Impulse);
        }
        else {
            rbody.AddForce(new Vector2(-40, 45), ForceMode2D.Impulse);
        }
        wallJumpR = false;
        wallJumpL = true;
        PSS.PlayJumpSound();
        yield return new WaitForSeconds(0.1f);
        moveLocked = false;
    }

    //movement	
    public void movement(float tempMove, float maxSpeed, float onFireSpeed){
		if (bisected && !fullyBisected) {
			tempMove=0;
			runningMomentum=0;
			return;	
		}
        if (anim.GetBool("FullStone"))
        {
            rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y);
            return;
        }
		if (move > 0.8f && runningMomentum < 0.6f && !wallCheck2 && !wallCheck1 && grounded) {
			runningMomentum += 0.01f;
		} else if (move < -0.8f && runningMomentum > -0.6f && !wallCheck2 && !wallCheck1 && grounded) {
			runningMomentum -= 0.01f;
		} else if (Mathf.Abs (move) < 0.5f || wallCheck2) {
			runningMomentum = 0;
		}
		if(anim.GetBool ("HoldingCrystal")&&tempMove!=0){;
			stopGiving();
		}
		if ((!grounded&&isTouchingWall&&wallHoldTimer<0.5f&&!bisected&&!onFire)||PM.controlsLocked||PDS.playerDead||Ooping||GivingCrystal) {
            tempMove =0;
			runningMomentum=0;
			return;		
		}

		if (transform.parent.GetComponent<PlayerHoldScript>()!=null) {
			if(tempMove<0&&facingRight&&!anim.GetBool("CODGroundSpikes")){
				Flip ();
			}else if(tempMove>0&&!facingRight&&!anim.GetBool("CODGroundSpikes")){
				Flip ();
			}
		}

		//makes it so the character can't face the wrong direction
		if ((facingRight && transform.localScale.x < -1)||(!facingRight && transform.localScale.x > 1)) {
            if (!flipDisabled)
            {
                facingRight = !facingRight;
            }
		}

		if (impact&&groundCheck3) {
			//flip check
			if(GetComponent<Rigidbody2D>().velocity.x<0&&facingRight&&!anim.GetBool("CODGroundSpikes")){
				impact=false;
				fallSpeed=0;
				Flip ();
			}else if(GetComponent<Rigidbody2D>().velocity.x>0&&!facingRight&&!anim.GetBool("CODGroundSpikes")){
				impact=false;
				fallSpeed=0;
				Flip ();
			}
			//dive roll?
            /*
			if(timeGrounded<0.4f&&timeGrounded>0){
				if(tempMove>0){
					GetComponent<Rigidbody2D>().velocity=(new Vector2(30-(timeGrounded*2),GetComponent<Rigidbody2D>().velocity.y));
				}else if(tempMove<0){
					GetComponent<Rigidbody2D>().velocity=(new Vector2(-30+(timeGrounded*2),GetComponent<Rigidbody2D>().velocity.y));
				}else if(facingRight){
					GetComponent<Rigidbody2D>().velocity=(new Vector2(30-(timeGrounded*2),GetComponent<Rigidbody2D>().velocity.y));
				}else{
					GetComponent<Rigidbody2D>().velocity=(new Vector2(-30+(timeGrounded*2),GetComponent<Rigidbody2D>().velocity.y));
				}
			}else{
			if(tempMove==0&&!onIce){
					GetComponent<Rigidbody2D>().velocity=(new Vector2(0,GetComponent<Rigidbody2D>().velocity.y));
				}
			}
			return;
            */
		}
		if (tempMove == 0) {
			idleTimer+=Time.deltaTime;		
		}
			if(idleTimer>3.65f){
				//anim.SetBool("Blinking",false);
				idleTimer=0;
			}
			if (!grounded && GetComponent<Rigidbody2D>().velocity.x < maxSpeed && GetComponent<Rigidbody2D>().velocity.x > (maxSpeed * -1) && !wallJumping&&tempMove!=0) {
				GetComponent<Rigidbody2D>().AddForce(new Vector2((tempMove+runningMomentum)*100,0));
			}else {
			if(bisected&&fullyBisected){
				if(PDS.TOD<1){
				GetComponent<Rigidbody2D>().velocity = new Vector2 ((tempMove), GetComponent<Rigidbody2D>().velocity.y);
				}else if(PDS.TOD>1&&PDS.TOD<2){
					GetComponent<Rigidbody2D>().velocity = new Vector2 ((tempMove/2), GetComponent<Rigidbody2D>().velocity.y);
				}
			}
				if(onFire){
					GetComponent<Rigidbody2D>().velocity = new Vector2 ((tempMove * onFireSpeed), GetComponent<Rigidbody2D>().velocity.y);	
				}else if(PM.eventHappening){
					GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
				if (GetComponent<Rigidbody2D>().velocity.x> 0.1f && !facingRight&&!anim.GetBool("CODGroundSpikes")){
					Flip ();
				}else if (GetComponent<Rigidbody2D>().velocity.x < -0.1f && facingRight&&!anim.GetBool("CODGroundSpikes")){
					Flip ();
				}
				}else{
				if(onIce){
					if(GetComponent<Rigidbody2D>().velocity.x<26&&GetComponent<Rigidbody2D>().velocity.x>-26){
						GetComponent<Rigidbody2D>().AddForce(new Vector2(tempMove*maxSpeed,0));
					}else if(GetComponent<Rigidbody2D>().velocity.x>25){
						GetComponent<Rigidbody2D>().velocity=new Vector2(25,GetComponent<Rigidbody2D>().velocity.y);
					}else if(GetComponent<Rigidbody2D>().velocity.x<-25){
						GetComponent<Rigidbody2D>().velocity=new Vector2(-25,GetComponent<Rigidbody2D>().velocity.y);
					}

				}else{
                    rbody.velocity = new Vector2 (((tempMove+runningMomentum) * maxSpeed), rbody.velocity.y);	
				}
				}
		}
		if((!wallCheck1||grounded)&&!swinging){
			if (tempMove > 0.01f && !facingRight&&!anim.GetBool("CODGroundSpikes"))
				Flip ();
			else if (tempMove < -0.01f && facingRight&&!anim.GetBool("CODGroundSpikes"))
				Flip ();
		}
	}
//wall check
	public void wallCheck(float move){
        if (cronened)
        {
            wallJumpL = false;
            wallJumpR = false;
            return;
        }
		BlinkBlockedF = Physics2D.OverlapCircle (BlinkPoint.position, 3f, whatIsWall);
		BlinkBlockedB = Physics2D.OverlapCircle (BlinkPoint2.position, 3f, whatIsWall);
		iceWallCheck = Physics2D.OverlapCircle (wallPoint1.position, 0.25f, whatIsIce);
		ledgeCheck = Physics2D.OverlapCircle (ledgePoint.position, 1f, whatIsLedge);
		wallCheck1 = Physics2D.OverlapCircle (wallPoint1.position, 0.3f, whatIsGround);
		wallCheck2 = Physics2D.OverlapCircle (wallPoint2.position, 0.1f, whatIsWall);
        enemyCheck = Physics2D.OverlapCircle(wallPoint2.position, 0.1f, whatIsEnemy);

        if (facingRight&&(wallCheck1)){
			wallJumpL=true;
		}else if(!facingRight&&(wallCheck1)){
			wallJumpR=true;
		}
		if (wallCheck1) {
			isTouchingWall=true;
		}else{
			wallHoldTimer=0;
			isTouchingWall=false;
		}
		if ((wallCheck1||ledgeCheck)&&!grounded) {
			wallJumpEnabled=true;
		} else {
			wallJumpEnabled=false;
		}

		if(isTouchingWall&&!grounded&&!onFire&&!bisected){

			if(facingRight&&Input.GetAxis(movebtn)<0){
				wallHoldTimer+=Time.deltaTime;
				if(wallHoldTimer<0.3f){
					move=1;
				}
			}else if(!facingRight&&Input.GetAxis(movebtn)>0){
				wallHoldTimer+=Time.deltaTime;
				if(wallHoldTimer<0.3f){
					move=-1;
				}
			}
            
			if(Input.GetButtonDown(grabbtn)){
				wallHoldTimer=1;
			}
		}
		
	}
//stumble check
	public void StumbleCheck(){
		/*if(fallSpeed<-90&&grounded&&!isBlinking&&timeGrounded<0.4f){
			if(groundCheck3){
				impact=true;
				anim.SetFloat ("timeGrounded", timeGrounded);
			}
		}else{
			impact=false;*/
			anim.SetFloat ("timeGrounded", 1);
		//}
	}
	public void RunningCheck(){
		if (Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x) > 30&&mpem.rate.constantMax != 50) {
			mpem.rate = new ParticleSystem.MinMaxCurve (20);
		} else if(mpem.rate.constantMax != 0){
			mpem.rate=new ParticleSystem.MinMaxCurve(0);
		}
	}

	public void RemoveCrystal(int currentCrystal){
		AB.CrystalList[currentAbility].unlocked=false;
		AB.CrystalList[currentAbility].enabled=false;
		AB.CrystalsSRs[currentAbility].color=new Color(1,1,1,0);
		stopGiving();
    }

    public void stopAlleyOoping()
    {
        if (isBlinking||stoneSkinActivated)
        {
            return;
        }
        if (Ooping) { 
            rbody.isKinematic = true;
            SpiritS.OopPhase(0);
            Ooping = false;
            anim.SetBool("AlleyOoping", false);
            anim.SetBool("AlleyOopReady", false);
            anim.SetBool("AlleyOopToss", false);
            rbody.isKinematic = false;
        }
    }

	public void stopGiving (){
        if (isBlinking||stoneSkinActivated)
        {
            return;
        }
		CC.glow.GetComponent<Renderer>().enabled = false;
		GivingCrystal = false;
		//rbody.isKinematic = false;
		anim.SetBool ("GivingCrystal", false);
		anim.SetBool ("HoldingCrystal", false);
		ablText.color = new Color (1, 1, 1, 0);
	}

//ground check
	public void GroundCheck(){
		playerCheck1 = Physics2D.OverlapCircle (groundPoint1.position, 0.3f, whatIsPlayer);
		playerCheck2 = Physics2D.OverlapCircle (groundPoint2.position, 0.3f, whatIsPlayer);
		IceCheck1 = Physics2D.OverlapCircle (groundPoint1.position, groundRadius, whatIsIce);
		IceCheck2 = Physics2D.OverlapCircle (groundPoint2.position, groundRadius, whatIsIce);
		groundCheck1 = Physics2D.OverlapCircle (groundPoint1.position, groundRadius, whatIsGround);
		groundCheck2 = Physics2D.OverlapCircle (groundPoint2.position, groundRadius, whatIsGround);
		groundCheck3 = Physics2D.OverlapCircle (groundPoint3.position, groundRadius, whatIsGround);
		if (IceCheck1 || IceCheck2) {
			onIce = true;
		} else {
			onIce=false;
		}
		if (groundCheck1 || groundCheck2) {
			if(!playerCheck1&&!playerCheck2){
				if(timeGrounded==0&&timeAirborne>0.8f&&!dying&&!rezing&&!PDS.playerDead&&!isBlinking){
					CreateLaunchSmoke(landingSmoke);
					PSS.PlayLandSound();
				}
			}
			timeGrounded+=Time.deltaTime;
			if(hangDisabled){
				hangDisabled=false;
			}
            jumpCooldown = 0;
            //trigger for the instant the player touches the ground
            if (!grounded&&stoneSkinActivated)
            {
                PSS.playStoneSmash();
            }
            grounded = true;
            smashCol.enabled = false;
            if (!ledgeCheck)
            {
                hanging = false;
                anim.SetBool("HangeIdle", false);
                anim.SetBool("Hanging", false);
            }
			firstJump=true;
			timeAirborne=0;
			//reset wall jumps and jump counter
			wallJumpL=true;
			wallJumpR=true;
		} else {
            if (stoneSkinActivated&& anim.GetBool("FullStone"))
            {
                smashCol.enabled = true;
            }
            timeAirborne += Time.deltaTime;
			if(timeAirborne>0.1f){
			firstJump=false;
			}
			anim.SetBool ("Summoning",false);
			summoning = false;
            if (AB.CrystalList[3].enabled&&AB.CrystalList[3].unlocked&&GetComponent<Rigidbody2D>().velocity.y<-9&&Input.GetButton(jumpbtn)&&!isAttacking&&!dying&&ControlsEnabled){
				rbody.velocity=new Vector2(rbody.velocity.x,-9);
			}else{
                rbody.gravityScale=1f;
			}
            if (!wallJumpL || !wallJumpR)
            {
                jumpCooldown += Time.deltaTime;
                if (jumpCooldown > 2)
                {
                    wallJumpL = true;
                    wallJumpR = true;
                    jumpCooldown = 0;
                }
            }
			timeGrounded=0;
			grounded=false;
            if (Ooping)
            {
                stopAlleyOoping();
            }
		}
		if (!groundCheck3) {
			impact=false;
		}
        if (floating)
        {
            if (Input.GetButtonUp(jumpbtn) || grounded || isTouchingWall||isAttacking||dying)
            {
                dragonFollow.target = floatDragon;
                dragonAnim.SetBool("Floating", false);
                floating = false;
            }
        }
        else
        {
            if (AB.CrystalList[3].enabled && AB.CrystalList[3].unlocked && GetComponent<Rigidbody2D>().velocity.y < -8 && Input.GetButton(jumpbtn)&&!grounded&&!isTouchingWall&&!isAttacking&&!dying&&ControlsEnabled)
            {
                dragonFollow.target = dragonPoint;
                floating = true;
                dragonAnim.SetBool("Floating", true);
            }
        }
        anim.SetBool("Floating", floating);
    }

    //jump	
    public void Jump(float jumpForce){
		wallHoldTimer = 1f;
		if(firstJump&&!onFire&&!bisected){
            if (!isTouchingWall&&!playerCheck1&&!playerCheck2)
            {
                CreateLaunchSmoke(launchSmoke);          
            }
			anim.SetBool ("Ground", false);
			rbody.velocity=new Vector2(rbody.velocity.x,0);
			rbody.AddForce (new Vector2 (0, jumpForce),ForceMode2D.Impulse);
			PSS.PlayJumpSound();
		}
	}
	
	
//flip	
	public void Flip()
	{
        if (flipDisabled)
        {
            return;
        }
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}




//hang check
	public void LedgeCheck(){
		if (Input.GetButton (grabbtn)) {
			HangCollider.GetComponent<Collider2D>().enabled=false;
			return;
		}
		HangCollider.GetComponent<Collider2D>().enabled = ledgeCheck;
	}



	public void AlleyOopFacingCheck(){
		if (Ooping||GivingCrystal) {
			rbody.velocity=new Vector2(0,rbody.velocity.y);
			FeetCollider.gameObject.layer = 9;
			if (otherPlayer.position.x > transform.position.x) {
				if (transform.localScale.x < 0) {
					Flip ();
				}
			} else {
				if (transform.localScale.x > 0) {
					Flip ();
				}
			}
		} else {
			FeetCollider.gameObject.layer = 10;
		}
	}




	public void RezFacingCheck(){
			if(otherPlayer.position.x>transform.position.x){
				if(transform.localScale.x<0){
					Flip ();
				}
			}else{
				if(transform.localScale.x>0){
					Flip ();
				}
			}
	}




	public void CreateLaunchSmoke(Transform smoke){
		Transform newSmoke;
		float xOffset;
		if (facingRight) {
			xOffset = -0.6f;
		} else {
			xOffset = 0.6f;
		}
		newSmoke=Instantiate (smoke, new Vector3 (transform.position.x+xOffset, transform.position.y-1.7f, transform.position.z),Quaternion.identity)as Transform;
		Vector3 newScale= newSmoke.localScale;
		if (!facingRight) {
			newScale.x*=-1;
			newSmoke.localScale=newScale;
		}
	}
	




	public void switchPlayers(){
		if(GM.numOfPlayers==1){
			if(!PM.PDS1.playerDead&&!PM.PDS2.playerDead&&!PM.eventHappening){
				ControlsEnabled = !ControlsEnabled;
				if(ControlsEnabled&&(Ooping||GivingCrystal)){
					stopAlleyOoping();
					stopGiving();
				}
			}
		}
	}




	public void DoneRolling(){
		impact=false;
		fallSpeed=0;
	}





	public void endOfToss(){
		launching = false;
		anim.SetBool("Tossed",false);
	}

	void hideAblText(){
	if (ablTextAlpha > 0) {
			ablTextAlpha -= Time.deltaTime;
			ablText.color = new Color (1, 1, 1, ablTextAlpha);
		} else {
			ablText.color = new Color (1, 1, 1, 0);
		}
	}


	public void StartGiving()
	{
		if (!AB.CrystalList [currentAbility].unlocked) {
			ablText.text="No ability selected";
			ablTextAlpha=1;
			ablText.color = new Color (1, 1, 1, 1);
			return;
		}
		CC.glow.GetComponent<Renderer>().enabled = true;
		CC.GetComponent<NewAbilityObject>().abilityName = AB.CrystalList [currentAbility].name;
		ablText.text = AB.CrystalList [currentAbility].name;
		ablTextAlpha=1;
		ablText.color = new Color (1, 1, 1, 1);
		CrystalSprite.color = CC.CrystalColor [currentAbility];
		CC.glow.color= new Color (CC.CrystalColor [currentAbility].r, CC.CrystalColor [currentAbility].g, CC.CrystalColor [currentAbility].b, 0.25f);
		DoneRolling();
			if(grounded&&(!playerCheck1&&!playerCheck2)){
				GivingCrystal=true;
				anim.SetBool("GivingCrystal",true);
			}
	}



	public void StartGrabbing()
	{
		//only set up an alley oop if both players are alive
		if (isPlayer1 && !GM.PM.PDS2.playerDead || !isPlayer1 && !GM.PM.PDS1.playerDead) {
			if(onButton){
				return;
			}
			if (AB.CrystalList [2].enabled && AB.CrystalList [2].unlocked) {
				OTS.strength = 80;
				SpiritS.anim.SetBool ("Active", true);
			} else {
				OTS.strength = 55;
				SpiritS.anim.SetBool ("Active", false);
			}
			DoneRolling();
			if(grounded&&(!playerCheck1&&!playerCheck2)&&!onTeleporter&&!GM.raceMode){
				Ooping=true;
				anim.SetBool("AlleyOoping",true);
			}
		}
		if(onTeleporter){
			anim.SetBool("Teleporting",true);
			teleporting=true;
		}else if(onButton){
		}
	}

    void CheckRot_Scale()
    {
        if (swinging)
        {
            return;
        }

        if (facingRight && (transform.localScale.x > 10||transform.localScale.x < 10))
        {
            transform.localScale = new Vector3(10, 10, 10);
        }

        if (!facingRight && (transform.localScale.x < -10|| transform.localScale.x > -10))
        {
            transform.localScale = new Vector3(-10, 10, 10);
        }

        transform.rotation = startingRotation;
    }


    public void StopGrabbing()
	{
		SpiritS.OopPhase (0);
		facingRight = true;
		Vector3 absScale = transform.localScale;
		absScale.x = Mathf.Abs (absScale.x);
		transform.localScale = absScale;
		cape.localPosition=new Vector3(0,0,0);
		cape.localRotation = Quaternion.AngleAxis (0, Vector3.forward);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(move*20,550));
		swinging=false;
		anim.SetBool("Swinging",swinging);
		transform.rotation=startingRotation;
		GetComponent<Rigidbody2D>().freezeRotation=true;
		transform.rotation=startingRotation;
		Vector3 theScale = transform.localScale;
		theScale.y = Mathf.Abs (theScale.y);
		transform.localScale = theScale;
	}



	public void AttemptAttack(){
		if(anim.GetBool("FailedRez")){
			anim.SetBool("FailedRez",false);
		}
		DoneRolling();
		endOfToss();
		stopAlleyOoping();
		stopGiving ();
		Attack();
	}




	public void ControllerInputs(){
		if (ControlsEnabled&&!rezing&&!summoning&&!teleporting&&!onFire&&!bisected&&Time.timeScale==1) {
			//all controller inputs
			//initiate resurrect
			/*if(Input.GetButton(Rezbtn)&&!stoneSkinActivated&&otherPC.PDS.playerDead&&grounded){
				rezSprite.color=new Color(1,1,1,1);

			}else{
			rezSprite.color=new Color(1,1,1,0);
			}
			*/
			if (Input.GetButtonDown (Rezbtn)&&!stoneSkinActivated&&!isBlinking&&!cronened&&!GM.raceMode) {
				DoneRolling();
				stopGiving ();
				stopAlleyOoping();
				RezBlockedCheck();
				Summon();
			}

			//initiate jump
            if(Input.GetButtonDown(jumpbtn) && stoneSkinActivated)
            {
                anim.SetBool("Wiggle", true);
            }
			if (Input.GetButtonDown (jumpbtn)&&!jumpLocked&&!absorbing&&!stoneSkinActivated&&!isBlinking) {
                hangDisabled = true;
                DoneRolling();
				stopAlleyOoping();
				stopGiving ();
				Jump (jumpForce);
				WallJump(jumpForce);
				if(anim.GetBool("FailedRez")){
					anim.SetBool("FailedRez",false);
				}
			}
			
			//initiate attack
			if (Input.GetButtonDown (attackbtn)&&!stoneSkinActivated&&!isBlinking && !cronened) {
				AttemptAttack();
			}
			TurnToStone(slideInput);
			AttemptBlink(slideInput);
			AttemptAbsorb(slideInput);
			anim.SetBool ("isSliding", absorbing);
			//initiate grab
			if (Input.GetButtonDown (grabbtn)&&!bisected&&!onFire&&!stoneSkinActivated&&!isBlinking&&!hanging && !cronened) {
				StartGrabbing();
				stopGiving ();
			}
			if(Input.GetButtonDown(grabbtn)&&hanging&&!hangDisabled && !cronened)
            {
				hangDisabled=true;
			}
			if (Input.GetButtonDown (givebtn)&&!onFire&&!bisected&&!stoneSkinActivated&&!isBlinking&&!cronened) {
				stopGiving();
				StartGiving();
				stopAlleyOoping();
			}
			if (Input.GetButtonDown (grabbtn)&&!onFire&&!bisected&&!stoneSkinActivated&&anim.GetBool("AlleyOopReady")&&!cronened) {
				stopAlleyOoping();
				stopGiving ();
			}
			//Realease Grab button
			if((Input.GetButtonUp(grabbtn)||onFire||bisected)&&swinging && !cronened)
            {               
				StopGrabbing();
			}
			//ChangeAbilities (Input.GetAxis (spellCastX),Input.GetAxis (spellCastY));
		}
	}






	public void CheckControls (){
		if (gameObject.name == "Player1") {
			L1 = GM.L1 [0];
			R1 = GM.R1 [0];
			jStickVertical = GM.jStickVertical [0];
			spellCastX=GM.spellCastX[0];
			spellCastY=GM.spellCastY[0];
			jumpbtn = GM.jumpbtn [0];
			movebtn = GM.movebtn [0];
			attackbtn = GM.attackbtn [0];
			grabbtn = GM.grabbtn [0];
			givebtn=GM.givebtn[0];
			Rezbtn = GM.Rezbtn [0];
			switchPlayersbtn = GM.switchPlayersbtn [0];
			slide = GM.slide [0];
			ablControlsUp =GM.ablControlsUp[0];
			ablControlsDown =GM.ablControlsDown[0];
		} else {
			L1 = GM.L1 [1];
			R1 = GM.R1 [1];
			jStickVertical = GM.jStickVertical [1];
			jumpbtn = GM.jumpbtn [1];
			movebtn = GM.movebtn [1];
			spellCastX=GM.spellCastX[1];
			spellCastY=GM.spellCastY[1];
			attackbtn = GM.attackbtn [1];
			grabbtn = GM.grabbtn [1];
			givebtn=GM.givebtn[1];
			Rezbtn = GM.Rezbtn [1];
			switchPlayersbtn = GM.switchPlayersbtn [1];
			slide = GM.slide [1];
			ablControlsUp =GM.ablControlsUp[1];
			ablControlsDown =GM.ablControlsDown[1];

		}
	}




	public void SwitchPlayerCheck()
	{
        if (GM.raceMode)
        {
            return;
        }
		if (Input.GetButtonDown (switchPlayersbtn)&&!GM.raceMode) {
			switchPlayers();
		}
		if ((Input.GetButtonUp(grabbtn)||Input.GetButtonUp(givebtn))&&((Ooping||GivingCrystal)&&ControlsEnabled||(otherPC.Ooping||otherPC.GivingCrystal)&&!ControlsEnabled)){
			switchPlayers();
			if (GM.camNum == 1&&GM.PM.PC2.ControlsEnabled) {
				GM.camNum = 2;
			}else if (GM.camNum == 2&&GM.PM.PC1.ControlsEnabled) {
				GM.camNum = 1;
			}
		}
	}





	public void RunStart()
	{
		trueParent = transform.parent;
		FindGM ();
		FindCBS ();
		FindPM ();
		FindPDS ();
		FindLDS ();
		startingRotation = transform.rotation;
		timeGrounded = 1;
		ControlsEnabled = true;
		anim = GetComponent<Animator> ();
		CC=CrystalSprite.GetComponent<CrystalColors> ();
		anim.SetBool ("Panicing", false);
		CheckControls ();
		AB = AblilityBar.GetComponent<AbilitiesBar> ();
        dragonAnim = floatDragon.GetComponent<Animator>();
        dragonFollow = floatDragon.GetComponent<FollowScript>();
		if (name == "Player1") {
			GM.AB [0] = AB;
		} else {
			GM.AB [1] = AB;
		}
		AB.targetPlayer = transform;
		PSS = GetComponent <PlayerSounds> ();
        InvokeRepeating("CheckRot_Scale", Time.deltaTime,1);
	}

    public void RunUpdate(){
        if (CBS == null)
        {
            return;
        }
        if (isBlinking)
        {
            rbody.velocity = new Vector2(0, 0);
        }
        if (!launching){
			GroundCheck ();
		}
		if (!grounded) {
			fallSpeed=GetComponent<Rigidbody2D>().velocity.y;
		}
		if (CBS.cameraEvent) {
			PM.controlsLocked = true;		
		}
		if (GM.numOfPlayers == 2&&!PDS.playerDead) {
			ControlsEnabled=true;
		}
		//initiate camControls
		//disable Controls if dead
		if (PDS.playerDead||PM.controlsLocked) {
			if(GetComponent<Rigidbody2D>().velocity.x<0&&facingRight){
				Flip ();
			}else if(GetComponent<Rigidbody2D>().velocity.x>0&&!facingRight){
				Flip ();
			}
			return;		
		}
		SwitchPlayerCheck ();
		ControllerInputs ();
	}


    

	public void RunFixedUpdate(){
		vertInput=Input.GetAxis(jStickVertical);
		if(Input.GetButton("KB_Up")&&(GM.numOfPlayers==1||gameObject.name=="Player1")){
			vertInput=-1;
		}
		if(Input.GetButton("KB_Down")&&(GM.numOfPlayers==1||gameObject.name=="Player1")){
			vertInput=1;
		}
		if (summoning) {
			rezDelay+=Time.deltaTime;		
		}
		if (PDS.playerDead) {
			rbody.velocity=new Vector2(rbody.velocity.x,rbody.velocity.y);
			return;		
		}
		StumbleCheck ();
		slideInput = Input.GetAxis (slide);

		if(ControlsEnabled&&!summoning&&!teleporting&&!isBlinking&&!rezing&&!absorbing&& Time.timeScale==1){
			if(!stoneSkinActivated&&!anim.GetBool("AlleyOopReady")&&!Ooping&&!GivingCrystal){
				if(Input.GetButton("KB_MoveL")&&(GM.numOfPlayers==1||gameObject.name=="Player1")){
					move=-1;
				}else if(Input.GetButton("KB_MoveR")&&(GM.numOfPlayers==1||gameObject.name=="Player1")){
					move=1;
				}else{
					move=Input.GetAxis(movebtn);
				}
			}else{
				move=0;
			}
		}else if(!anim.GetBool("FullStone")){
			move=0;
		}
        if (climbing)
        {
            if (climbTime < 1)
            {
                climbTime += .1f;
            }else
            {
                climbTime = 1;
            }
        }else
        {
            if (climbTime > 0)
            {
                climbTime -= .1f;
            }
            else
            {
                climbTime = 0;
            }
        }
		if (Mathf.Abs (move) > 0) {
			rezing=false;
			rezDelay=0;
		}
		if(stoneSkinCoolingdown){
			StoneSkinCooldown();
		}
		if (attackCoolingDown) 
		{
			AttackCooldown();
		}

		if (hangDisabled) 
		{
			LedgeReleaseCooldown();
		}
		if (wallJumping) 
		{
			wallJumping=false;	
		}
		if (hideAbls||!ControlsEnabled) {
			AB.HideAbilities(currentAbility);
		}
        if (Input.GetAxis(jStickVertical) < 0.1f && Input.GetAxis(jStickVertical) > -0.1f&&(!Input.GetButton("KB_Up")&& !Input.GetButton("KB_Down")))
        {
            cape.GetComponent<Renderer>().enabled = swinging;
        }else
        {
            cape.GetComponent<Renderer>().enabled = false;
        }
        AlleyOopFacingCheck ();
		jumpCheck ();
		wallCheck (move);
		WallDrag (move);
		AttemptRez ();
		LedgeCheck();
		hideAblText ();
		//EdgeCheck ();
		if(!swinging||!isBlinking||!rezing||!summoning||!absorbing||!stoneSkinActivated){
			if((move > 0.1f || move < -0.1f)&&!moveLocked) {
				if(anim.GetBool("FailedRez")){
				anim.SetBool("FailedRez",false);
				}
				movement(move,maxSpeed,onFireSpeed);
			}else if(!moveLocked){
				movement(0,maxSpeed,onFireSpeed);
			}
			if(moveLocked){
				if(GetComponent<Rigidbody2D>().velocity.x>0&&!facingRight){
					Flip();
				}
				if(GetComponent<Rigidbody2D>().velocity.x<0&&facingRight){
					Flip();
				}
			}
		}
	}
	public void ChangeAbilityOverride(int selAbility){
		currentAbility = selAbility;
		anim.SetBool("Stoned",false);
		anim.SetBool("FullStone",false);
		if(stoneSkinActivated){
			Instantiate(StoneSkinFlakes,new Vector3(transform.position.x+StoneSkinOffset.x,transform.position.y+StoneSkinOffset.y,-12),Quaternion.identity);
		}
		stoneSkinActivated=false;
		GetComponent<Rigidbody2D>().mass=1;
		//AB.DisplayAbilities (selAbility);
		hideAbls=false;
		for (int i =0; i<5; i++) {
			if(selAbility==i){
				AB.CrystalList[i].enabled=true;
			}else{
				AB.CrystalList[i].enabled=false;
			}
		}
		if (AB.CrystalList[4].enabled && AB.CrystalList[4].unlocked) {
			anim.SetBool ("AbsorbEnabled", true);
		} else {
			anim.SetBool ("AbsorbEnabled", false);
		}
		if (AB.CrystalList[2].enabled && AB.CrystalList[2].unlocked) {
			OTS.strength=80;
			SpiritS.anim.SetBool ("Active", true);
		} else {
			OTS.strength=55;
			SpiritS.anim.SetBool ("Active", false);
		}
		if (name == "Player1") {
			GM.UpdateHUD (selAbility, 1);
		} else {
			GM.UpdateHUD (selAbility, 2);
		}
	}


	public void ChangeAbilities(float x,float y){
		if (x < 0.1f && x > -0.1f && y < 0.1f && y > -0.1f) {
			hideAbls = true;
			return;
		} else {
			anim.SetBool("Stoned",false);
			anim.SetBool("FullStone",false);
			if(stoneSkinActivated){
				Instantiate(StoneSkinFlakes,new Vector3(transform.position.x+StoneSkinOffset.x,transform.position.y+StoneSkinOffset.y,-12),Quaternion.identity);
			}
			stoneSkinActivated=false;
			GetComponent<Rigidbody2D>().mass=1;
		}
		currentAbility = AB.SelectAbility (x,y, currentAbility);
			AB.DisplayAbilities (currentAbility);
			hideAbls=false;
		for (int i =0; i<5; i++) {
		if(currentAbility==i){
				AB.CrystalList[i].enabled=true;
			}else{
				AB.CrystalList[i].enabled=false;
			}
		}
		if (AB.CrystalList[4].enabled && AB.CrystalList[4].unlocked) {
			anim.SetBool ("AbsorbEnabled", true);
		} else {
			anim.SetBool ("AbsorbEnabled", false);
		}
	}

    public void attemptFireBall(){
		if (LDS.energyAbsorbed) {
			if(facingRight){
				PSpawner.yForce=0;
				PSpawner.xForce=20;
			}else{
				PSpawner.yForce=0;
				PSpawner.xForce=-20;
			}
			FireBallRTS.Activated = true;
			LDS.energyAbsorbed = false;
		}
	}
	
    public void CheckRotation()
    {
    }
}




