using UnityEngine;
using System.Collections;

public class CaveBossBehavior : GameStateFunctions {

    public Transform NewAbl;
    public Vector3 bossFightCamPosition;
    public Transform savior;
    public Vector2 eidolonPos;
    public Vector2 aimDir;
	public Transform Mouth;
	public Transform groundcheck;
    public Transform groundcheck2;
    public bool groundFound1;
    public bool groundFound2;
    public bool charging;
    public Transform exit;
	public Collider2D stompCol;
	public Collider2D weakPoint;
    public Collider2D bodyCol;
	public bool grounded;
	public bool activated;
	public bool facingRight;
	public bool taunting;
    public bool trackingSavior;
    public bool airborne;
    public float attackDelay;
	public float jumpForce;
	public float xSpeed;
    public bool hit;
	public LayerMask whatIsGround;

    public RemoteTriggerScript[] geysersRTS;
    public RemoteTriggerScript[] gravityBallEmitters;
	RemoteTriggerScript mouthRTS;
	RemoteTriggerScript exitRTS;
    UlmockSounds US;
	ProjectileSpawner mouthPS;

	public Animator anim;
	public int health;
    Rigidbody2D rbody;
    Vector2 delayRange;
    public int attackNum;
    public float targetX;
    public int previousATK;
    public int numOfAttacks;
    public int minAttacks;
    int targetGeyser;
    public bool readyToAttack;

    void Awake()
    {
        weakPoint.enabled = true;
        xSpeed = 0;
        attackDelay = 0.6f;
        mouthRTS = Mouth.GetComponent<RemoteTriggerScript>();
        mouthPS = Mouth.GetComponent<ProjectileSpawner>();
        exitRTS = exit.GetComponent<RemoteTriggerScript>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        US = GetComponent<UlmockSounds>();
        health = 4;
    }

    void Start () {
        FindCBS();
		FindGM ();
	}
	
	void FixedUpdate () {
		if (!activated) {
			return;
		}
        if (health <= 0)
        {
            return;
        }
        if (!grounded)
        {
            airborne = true;
        }
		groundFound1 = Physics2D.OverlapBox (groundcheck.position, new Vector2(1,2f),0, whatIsGround);
        groundFound2 = Physics2D.OverlapBox(groundcheck2.position, new Vector2(1, 2f), 0, whatIsGround);

        if (!anim.GetBool("Attacking"))
        {
            ChooseAttack(health);
            anim.SetBool("Attacking", true);
        }
        if (groundFound1 || groundFound2)
        {
            grounded = true;
        }else
        {
            grounded = false;
        }

        if (airborne && grounded)
        {
            xSpeed = 0;
            airborne = false;
        }
        anim.SetBool("Grounded", grounded);
        if (rbody.velocity.y>-1&&stompCol.enabled) {
			stompCol.enabled=false;
		}

		if (!grounded) {
			if(GetComponent<Rigidbody2D>().velocity.y<-1&&!stompCol.enabled){
				stompCol.enabled=true;
			}
		} else if(trackingSavior)
        {
			TrackSavior();
		}
		Movement (xSpeed);
	}

	public void Hit(){
        hit = true;
        readyToAttack = false;
		health -= 1;
        weakPoint.enabled = false;
        StopAllCoroutines();
        if (health <= 0)
        {
            anim.SetTrigger("Death");
            bodyCol.gameObject.layer = 11;
            weakPoint.enabled = false;          
        }
        else {
            anim.SetTrigger("Damaged");
        }
        minAttacks += 3 - health;
        numOfAttacks = 0;
        foreach(RemoteTriggerScript R in geysersRTS)
        {
            R.Activated = false;
        }
        foreach (RemoteTriggerScript R in gravityBallEmitters)
        {
            R.Activated = false;
        }
        attackDelay -= 0.1f;
        StartCoroutine("GetBackUp");
	}

    public IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attacking",false);
        yield return new WaitForSeconds(0.05f);
        trackingSavior = true;
        weakPoint.enabled = true;
        readyToAttack = true;
        yield return new WaitForSeconds(4f);
        StartCoroutine("ActivateGravityBalls");
    }

    public void EnemyDefeated(){
		exitRTS.Activated = true;
	}
    
    public void EndOfAttack()
    {
        if (hit)
        {
            anim.SetTrigger("Roar");
            hit = false;
        }
        else
        {
            anim.SetBool("Attacking", false);
        }
        trackingSavior = true;      
    }



    public void ChooseAttack(int life)
    {
        if (!readyToAttack)
        {
            return;
        }
        switch (life)
        {
            case (4):
                delayRange=new Vector2(3, 4);
                attackNum = Mathf.RoundToInt(Random.Range(0f, 1f));
                break;
            case (3):
                delayRange = new Vector2(1, 2);
                attackNum = Mathf.RoundToInt(Random.Range(0f, 1f));
                break;
            case (2):
                delayRange = new Vector2(.5f, 1);
                attackNum = Mathf.RoundToInt(Random.Range(0f, 2f));
                break;
            case (1):
                delayRange = new Vector2(.2f, .5f);
                attackNum = Mathf.RoundToInt(Random.Range(0f, 2f));
                break;
            default:
                break;
        }
        StartCoroutine("RandomAttack");
    }

    IEnumerator RandomAttack()
    {
        if (previousATK == attackNum)
        {
            Debug.Log("repeat");
            ChooseAttack(health);
            yield break;
        }
        if (numOfAttacks < minAttacks && attackNum == 0)
        {
            Debug.Log("Can't Jump");
            ChooseAttack(health);
            yield break;
        }
        yield return new WaitForSeconds(Random.Range(delayRange.x, delayRange.y));
        previousATK = attackNum;
        switch (attackNum)
        {          
            case (2):
                Debug.Log("Meteor Strike");
                anim.SetTrigger("MeteorStrike");
                numOfAttacks += 1;
                yield return new WaitForSeconds(Random.Range(1.5f, 2));
                anim.SetBool("Attacking", false);
                break;
            case (1):
                Debug.Log("Fire Breath");
                numOfAttacks += 1;
                anim.SetTrigger("BreatheFire");
                break;
            case (0):
                Debug.Log("Jump");
                anim.SetTrigger("Jump");
                foreach (RemoteTriggerScript R in geysersRTS)
                {
                    R.Activated = false;
                }
                foreach (RemoteTriggerScript R in gravityBallEmitters)
                {
                    R.Activated = false;
                }
                numOfAttacks += 1;
                trackingSavior = false;
                yield return new WaitForSeconds(Random.Range(3.5f, 4));
                trackingSavior = true;
                anim.SetTrigger("GetUp");
                break;
            default:
                break;
        }
    }
 

	void Jump(){
        GetComponent<Rigidbody2D>().velocity=new Vector2(0, 0);
        CalculateJumpForce();
        GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce));
        US.Play_Jump_Sound();
    }


    IEnumerator ActivateGravityBalls()
    {
        targetGeyser = Random.Range(0, 4);

        yield return new WaitForSeconds(attackDelay * 2);
        if (health > 1)
        {
            gravityBallEmitters[targetGeyser].Activated = true;
            yield return new WaitForSeconds(1 - attackDelay);
            gravityBallEmitters[targetGeyser].Activated = false;
        }
        else
        {
            geysersRTS[targetGeyser].Activated = true;
            yield return new WaitForSeconds(1 - attackDelay);
            geysersRTS[targetGeyser].Activated = false;
        }
        StartCoroutine("ActivateGravityBalls");
    }

    public IEnumerator AllGeysers()
    {
        foreach(RemoteTriggerScript r in geysersRTS)
        {
            r.Activated = true;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (RemoteTriggerScript r in geysersRTS)
        {
            r.Activated = false;
        }
    }
    void CalculateJumpForce()
    {
        float jumpDistance =savior.position.x - transform.position.x;
        if(jumpDistance > 80){
            jumpDistance = 80;
        }else if (jumpDistance < -80)
        {
            jumpDistance = -80;
        }
        jumpForce = 165000+(Mathf.Abs(jumpDistance)*900);
        xSpeed = jumpDistance*.6f;
    }

	void Movement(float x){
		GetComponent<Rigidbody2D>().velocity = new Vector2 (xSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	public void TrackSavior(){
	    if (savior.position.x > transform.position.x && !facingRight) {
			Flip();
		}
		if (savior.position.x < transform.position.x && facingRight) {
			Flip();
		}
	}

    void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void CreateAbilityPickUp()
    {
       // NewAbl.position = transform.position;
    }

	IEnumerator FireBarrage(){
        FindTarget();
        SpitFire();
        if (health == 4)
        {
            yield return new WaitForSeconds(0.1f);
            anim.SetBool("Attacking", false);
            yield break;
        }
		yield return new WaitForSeconds (attackDelay);
        FindTarget();
        SpitFire();
        if (health == 3)
        {
            yield return new WaitForSeconds(0.1f);
            anim.SetBool("Attacking",false);
            yield break;
        }
        yield return new WaitForSeconds(attackDelay);
        FindTarget();
        SpitFire();
        if (health == 2)
        {
            yield return new WaitForSeconds(0.1f);
            anim.SetBool("Attacking", false);
            yield break;
        }
        yield return new WaitForSeconds(attackDelay);
        FindTarget();
        SpitFire();
        yield return new WaitForSeconds(attackDelay);
        FindTarget();
        SpitFire();
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Attacking", false);
    }

    void FindTarget()
    {
        eidolonPos = savior.position;
        aimDir = new Vector2(eidolonPos.x - transform.position.x, eidolonPos.y - transform.position.y).normalized;
        mouthPS.xForce = aimDir.x * 15;
        mouthPS.yForce = aimDir.y * 15;
    }

    void SpitFire()
    {
        mouthRTS.Activated = true;
    }
    public void EndOfDemo()
    {
        anim.SetBool("Taunt", false);
    }

    public IEnumerator MinorShake()
    {
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 4, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x - 4, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 4, bossFightCamPosition.y + 6, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = bossFightCamPosition;
    }

    IEnumerator ShakeCamera()
    {
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 6, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x - 6, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 6, bossFightCamPosition.y + 6, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x - 6, bossFightCamPosition.y - 6, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x + 6, bossFightCamPosition.y, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = new Vector3(bossFightCamPosition.x - 6, bossFightCamPosition.y + 6, bossFightCamPosition.z);
        yield return new WaitForSeconds(0.05f);
        CBS.EventLocation = bossFightCamPosition;
    }
}
