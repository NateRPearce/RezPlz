using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EidolonBehavior : GameStateFunctions {


	public RectTransform[] ArcanHud= new RectTransform[2];
	public RectTransform[] ZephHud= new RectTransform[2];
	public RectTransform[] EidolonHUD = new RectTransform[1];
	public ManaBar lifeBarMB;
	public Collider2D bodyCollider;
	public bool controlsDisabled;
    Vector3[] EidonlonHudPos = new Vector3[2];
    Vector3[] arcanHudPos = new Vector3[2];
	Vector3[] zephHudPos = new Vector3[2];
    public SpriteRenderer godBeams;
    bool aiming;
    public Rigidbody2D rbody;
    public Transform floatCollider;
    public Transform glyph;
    Animator glyphAnim;
    public LayerMask whatIsGround;
    public bool grounded;
	public bool transformed;
	public float transformTime;
	public bool eidolonActivated;
	public SpriteRenderer crossHair;
	public Transform groundPoint;
	public Transform swordL;
    public Transform swordR;
    EnergySwordBehavior swordLESB;
    EnergySwordBehavior swordRESB;
    public bool swordRReady;
    public bool swordLReady;
	public float speed;
	public float jumpForce;
	public bool facingRight;
	public bool attacking;
	public Animator anim;
	public bool fireArmL;
	Vector2 dir;
	bool doubleJumped;
	SpriteRenderer[] sr=new SpriteRenderer[0];
	float moveInput;
	string moveBtn;
	string jumpBtn;
	public string aimInputX;
	public string aimInputY;
	string shootBtn;
    public Vector2 fireDir;
    EidolonSounds ES;
    public float EidolonHUDOffset;
    public float EidolonMBOffset;
	void Start(){
        ES = GetComponent<EidolonSounds>();
		FindGM ();
        rbody = GetComponent<Rigidbody2D>();
        glyphAnim = glyph.GetComponent<Animator>();
		sr = GetComponentsInChildren<SpriteRenderer> ();
		dir = new Vector2 (20, 0);
        bodyCollider.enabled = false;
        swordLESB = swordL.GetComponent<EnergySwordBehavior>();
        swordRESB = swordR.GetComponent<EnergySwordBehavior>();
        swordL.gameObject.SetActive(false);
        swordR.gameObject.SetActive(false);
        foreach (SpriteRenderer s in sr) {
			s.enabled = false;
		}
		sr [0].enabled = true;
		for(int i=0;i<2;i++){
			zephHudPos[i]=ZephHud[i].localPosition;
			arcanHudPos[i]=ArcanHud[i].localPosition;
            EidonlonHudPos[i] = EidolonHUD[i].localPosition;
		}
        rbody.isKinematic = true;
		anim = GetComponent<Animator> ();
		lifeBarMB = EidolonHUD[1].GetComponent<ManaBar> ();
		crossHair.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void Update(){
		if(!eidolonActivated||controlsDisabled){
			return;
		}else if(eidolonActivated&&!transformed){
			sr[0].enabled=true;
            return;
		}
			if (Input.GetButtonDown (jumpBtn)) {
				if ((grounded || !doubleJumped)) {
					Jump ();
				}
			}
        if (Input.GetButton(shootBtn)&& (swordLReady || swordRReady))
        {
            aiming = true;
        }
        else
        {
            aiming = false;
        }
			if (Input.GetButtonUp (shootBtn)){
                if(swordLReady||swordRReady) {
                    Attack();
                 }
			}
	}


	void FixedUpdate(){
		if (!eidolonActivated) {
            if (!rbody.isKinematic)
            {
                rbody.isKinematic = true;
            }
			return;
		} else if (eidolonActivated && !transformed) {
			if (ArcanHud [0].localPosition.y != arcanHudPos [0].y - 105) {
				for (int i=0; i<2; i++) {
					ArcanHud [i].localPosition = Vector3.MoveTowards (ArcanHud [i].localPosition, new Vector3 (ArcanHud [i].localPosition.x, arcanHudPos [i].y - 105, ArcanHud [i].localPosition.z), 3);
					ZephHud [i].localPosition = Vector3.MoveTowards (ZephHud [i].localPosition, new Vector3 (ZephHud [i].localPosition.x, zephHudPos [i].y - 105, ZephHud [i].localPosition.z), 3);
				}
            }
            else if (EidolonHUD[0].localPosition.y != EidonlonHudPos[0].y + EidolonHUDOffset || EidolonHUD[1].localPosition.y != EidonlonHudPos[1].y + EidolonMBOffset)
            {
                EidolonHUD[0].localPosition = Vector3.MoveTowards(EidolonHUD[0].localPosition, new Vector3(EidolonHUD[0].localPosition.x, EidonlonHudPos[0].y + EidolonHUDOffset, EidolonHUD[0].localPosition.z), 3);
                EidolonHUD[1].localPosition = Vector3.MoveTowards(EidolonHUD[1].localPosition, new Vector3(EidolonHUD[1].localPosition.x, EidonlonHudPos[1].y + EidolonMBOffset, EidolonHUD[1].localPosition.z), 3);
            }
            return;
		}
		if (ArcanHud [0].localPosition.y != arcanHudPos [0].y - 105) {
			for (int i=0; i<2; i++) {
				ArcanHud [i].localPosition = Vector3.MoveTowards (ArcanHud [i].localPosition, new Vector3 (ArcanHud [i].localPosition.x, arcanHudPos [i].y - 105, ArcanHud [i].localPosition.z), 3);
				ZephHud [i].localPosition = Vector3.MoveTowards (ZephHud [i].localPosition, new Vector3 (ZephHud [i].localPosition.x, zephHudPos [i].y - 105, ZephHud [i].localPosition.z), 3);
			}
		} else if (EidolonHUD [0].localPosition.y != EidonlonHudPos[0].y + EidolonHUDOffset|| EidolonHUD[1].localPosition.y!=EidonlonHudPos[1].y+EidolonMBOffset) {
				EidolonHUD [0].localPosition = Vector3.MoveTowards (EidolonHUD [0].localPosition, new Vector3 (EidolonHUD [0].localPosition.x, EidonlonHudPos[0].y+EidolonHUDOffset, EidolonHUD [0].localPosition.z), 3);
                EidolonHUD[1].localPosition = Vector3.MoveTowards(EidolonHUD[1].localPosition, new Vector3(EidolonHUD[1].localPosition.x, EidonlonHudPos[1].y + EidolonMBOffset, EidolonHUD[1].localPosition.z), 3);
        }

        if (!sr[sr.Length-1].enabled) {
			foreach (SpriteRenderer s in sr) {
				s.enabled = true;
			}
			//rbody.isKinematic=false;
			crossHair.enabled = true;
			bodyCollider.enabled=true;
		}
        GroundCheck();
        glyphAnim.SetBool("Grounded", grounded);
        if (grounded) {
			doubleJumped = false;
		}
        if (!aiming)
        {
            moveInput = Input.GetAxis(moveBtn);
        }
        else if(grounded)
        {
            moveInput = 0;
        }
		if (controlsDisabled) {
            godBeams.color = new Color(1, 1, 1, 0.1f);
		} else if(!aiming){
            godBeams.color = new Color(1, 1, 1, 0.8f);
            if (Input.GetButton("KB_MoveL"))
            {
                Movement(-1);
            }
            else if (Input.GetButton("KB_MoveR"))
            {
                Movement(1);
            }
            else
            {
                Movement(moveInput);
            }
		}
		aimCrossHair(Input.GetAxis (aimInputX), Input.GetAxis (aimInputY));
	}

	void Jump(){
		if (!grounded) {
        	rbody.velocity=new Vector2(0,0);
			doubleJumped = true;
            glyph.position = floatCollider.position;
        }
        ES.PlayJump();
        glyphAnim.SetTrigger("Break");
        rbody.AddForce(new Vector2(0,jumpForce));
	}

	void Attack() {
        if (Input.GetAxis(aimInputX)>0&&!facingRight){
			Flip();
		}else if(Input.GetAxis(aimInputX)<0&&facingRight){
			Flip();
		}
        fireDir = new Vector2(Input.GetAxis(aimInputX), Input.GetAxis(aimInputY)).normalized * 40;
        if (fireArmL) {
			anim.SetTrigger ("AttackL");
		} else {
			anim.SetTrigger ("AttackR");
		}
		attacking = true;
		fireArmL = !fireArmL;
        Fire();
	}

	void aimCrossHair(float X, float Y){
		if (X != 0 || Y != 0) {
			dir = new Vector2 (X, Y).normalized * 20;
		} else if (facingRight) {
			X = 1;
			Y = 0;
			dir = new Vector2 (X, Y).normalized * 20;
		} else if (!facingRight) {
			X = -1;
			Y = 0;
			dir = new Vector2 (X, Y).normalized * 20;
		}
			crossHair.transform.position = Vector3.MoveTowards(crossHair.transform.position, new Vector3 (transform.position.x + dir.x, transform.position.y - dir.y, -10),Time.deltaTime*1000);
        if (crossHair.transform.position.y > transform.position.y + 10)
        {
            anim.SetBool("AttackUP", true);
        }
        else
        {
            anim.SetBool("AttackUP", false);
        }
        //If firing and position isn't at the top or bottom of the spectrum
		//HeadTracking ();
	}

	void Movement(float move){
		rbody.velocity = new Vector2 (move * speed, rbody.velocity.y);
		if (attacking) {
			return;
		}
		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	public void SetControls(PlayerControls P1,PlayerControls P2){
		moveBtn = P1.movebtn;
		jumpBtn=P1.jumpbtn;
		aimInputX = P2.movebtn;
		aimInputY = P2.jStickVertical;
		shootBtn = P2.attackbtn;
	}



	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void FinishedTransforming(){
        transformed = true;
        swordL.gameObject.SetActive(true);
        swordR.gameObject.SetActive(true);
    }

    public void Fire()
    {
        if (fireDir == new Vector2(0, 0))
        {
            if (facingRight)
            {
                fireDir.x = 40;
            }
            else {
                fireDir.x = -40;
            }
        }
        if (!fireArmL)
        {
            swordLESB.xForce = fireDir.x * 2;
            swordLESB.yForce = -fireDir.y * 2;
            if (!swordLReady)
            {
                return;
            }
            swordLESB.Attack();
            swordLReady = false;
        }
        else
        {
            swordRESB.xForce = fireDir.x * 2;
            swordRESB.yForce = -fireDir.y * 2;
            if (!swordRReady)
            {
                return;
            }
            swordRESB.Attack();
            swordRReady = false;
        }
    }

    public void GroundCheck()
    {
        grounded = Physics2D.OverlapCircle(floatCollider.position, 1, whatIsGround);
        if (grounded)
        {
            glyph.position = floatCollider.position;  
        }
    }

    public void EndOfAttack()
    {
       attacking = false;
    }
}
