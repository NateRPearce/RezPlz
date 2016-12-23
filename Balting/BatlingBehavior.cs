using UnityEngine;
using System.Collections;

public class BatlingBehavior : FlyingMinionScript {

	[HideInInspector]
	public float flapStrength;
	float idlePower=1720;
	public bool ascending;
	float attackingSpeed=300;
	public bool hover;
	Quaternion startingRot;
	public Transform targetPlayer;
	BatlingVision BV;
    public float deadTImer;
    public bool enter;
    BatlingSounds BS;

    void Awake(){
		anim = GetComponent<Animator> ();
        rBody = GetComponent<Rigidbody2D>();
		startingRot = transform.rotation;
		BV = GetComponentInChildren<BatlingVision> ();
        BS = GetComponent<BatlingSounds>();
        flapStrength = idlePower;
        if (!enter)
        {
            transform.localScale = new Vector3(4, 4, 4);
            GetComponent<SpriteRenderer>().sortingLayerName = "Background";
            transform.position = new Vector3(transform.position.x, transform.position.y,20);
            bodyCollider.enabled = false;
        }
        else
        {
            anim.SetTrigger("PreEnter");
        }
    }
	public void FixedUpdate(){
        if (enter && Mathf.Abs(transform.localScale.x) != 10)
        {
            if (!facingRight)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(10, 10, 10), .1f);
            }else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(-10, 10, 10), .1f);
            }
        }
		if (hitByPlayer&&!dead) {
			anim.SetBool("Dead",true);
            BS.StopAttack();
            BS.Stopwoosh();
            BS.StopFlap();
			dead=true;
		}
		if (dead) {
            deadTImer += Time.deltaTime;
			if(rBody.isKinematic){
				rBody.isKinematic=false;
			}
			if(rBody.velocity.x<0&&!facingRight){
				Flip ();
			}else if(rBody.velocity.x>0&&facingRight){
				Flip ();
			}
			CheckForGround ();
			rBody.freezeRotation=grounded;
			anim.SetBool("Grounded",grounded);
			if(grounded){
				transform.rotation=Quaternion.RotateTowards(transform.rotation,startingRot,15);
			}
            if (deadTImer > 2&&deadTImer<3)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 3 - deadTImer);
            }
            if (deadTImer > 3)
            {
                Destroy(gameObject);
            }
		} else {
			if(!anim.GetBool("isAttacking")||(anim.GetBool("isAttacking")&&atkTimer>0.5f)){
				if(rBody.velocity.x>0&&!facingRight){
					//Flip ();
				}else if(rBody.velocity.x<0&&facingRight){
					//Flip ();
				}
			}
			WallCheckFront();
            if (enterancePoint == null && enter)
            {
                AttackCheck();
            }
			movement ();
		}
	}

	public void AttackCheck(){
		if(isAttacking){
			atkTimer+=Time.deltaTime;
			if(atkTimer<0.5f){
				rBody.velocity=new Vector2((xSpeed/50)*-1,0);
                BS.StopFlap();
                anim.SetBool("isAttacking",true);
				anim.speed=1f;
			}else if(atkTimer>0.5f&&atkTimer<1){
				rBody.velocity=new Vector2(xSpeed/5,0);
			}else if(atkTimer>1&&atkTimer<1+atkCooldown){
				anim.SetBool("isAttacking",false);
				xSpeed=0;
			}else if(atkTimer>1+atkCooldown){
				isAttacking=false;
				atkTimer=0;
			}
		}
	}
	public void movement(){
	if (playerSpotted) {
            if (enter)
            {
                if (PC.PDS.playerDead)
                {
                    targetPlayer = null;
                    playerSpotted = false;
                    BV.EnableCol();
                    return;
                }
            }
			hover=false;
		if(targetPlayer.position.y>transform.position.y||grounded){
				ascending=true;
			}else{
				ascending=false;
			}

			float distToPlayerX=Mathf.Abs(targetPlayer.position.x-transform.position.x);
            float distToPlayerY=targetPlayer.position.y-transform.position.y;
			if((distToPlayerX>60||distToPlayerY>60)&&playerSpotted){
				playerSpotted=false;
				BV.EnableCol();
			}
			if(distToPlayerY<0&&distToPlayerY>-3&&distToPlayerX<25&&!isAttacking&&PC!=null){
				isAttacking=true;
			}

			if(wallFound){
				xSpeed=0;
			}else if(targetPlayer.position.x>transform.position.x){
                if (!anim.GetBool("isAttacking") && !facingRight && enter)
                {
                    Debug.Log("Flip1");
                    Flip();
                }
				if(atkTimer==0||atkTimer>1){
					xSpeed=attackingSpeed;
				}
			}else if(targetPlayer.position.x<transform.position.x){
				if(!anim.GetBool("isAttacking")&&facingRight && enter)
                {
                    Debug.Log("Flip2");
                    Flip();
				}
				if(atkTimer==0||atkTimer>1){
					xSpeed=-attackingSpeed;
				}
			}

		} else if(!anim.GetBool("isAttacking")){
			xSpeed=0;
			hover=true;
		}
	}
    public void switchLayers()
    {
        GetComponent<SpriteRenderer>().sortingLayerName="player";
        bodyCollider.enabled = true;
        transform.position=new Vector3(transform.position.x, transform.position.y, 0);
    }
    public IEnumerator EnterCastle()
    {
        if (!enter)
        {
            BV.searchEnabled = false;
            targetPlayer = enterancePoint;
            playerSpotted = true;
        }
        yield return new WaitForSeconds(2);
        anim.SetTrigger("Enter");
        targetPlayer = null;
        playerSpotted = false;
        enter = true;
        enterancePoint = null;
        BV.searchEnabled = true;
    }

    public void Flap(){
		if (dead) {
			return;
		}
		rBody.velocity = new Vector2 (0, 0);
		if (hover) {
			flapStrength=idlePower;
			anim.speed=1f;
		} else if (ascending||grounded) {
			anim.speed=1.1f;
		} else {
			anim.speed=0.9f;
		}
		rBody.AddForce (new Vector2 (xSpeed, flapStrength));
	}


    public void FlipOvveride()
    {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
    }
      
}
