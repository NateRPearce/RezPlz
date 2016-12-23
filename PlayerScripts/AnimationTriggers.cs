using UnityEngine;
using System.Collections;

public class AnimationTriggers : PlayerInheritance {

	int alleyOops=1;
	public int oopCounter;
    public Rigidbody2D rbody;
    PlayerSounds PS;
    SpeechScript SS;
	void Start () {
		FindPC ();
		FindPDS ();
		FindLDS ();
		FindGM ();
		FindPM ();
        SS = GetComponent<SpeechScript>();
		PC.capeAnim = PC.cape.GetComponent<Animator> ();
        rbody = GetComponent<Rigidbody2D>();
        PS = GetComponent<PlayerSounds>();
	}

	void Update()
	{
		if (PC.eidolonMode) {
			return;
		}
		animCheck(PC.move);
	}

	//animation handler
	public void animCheck(float move){
		PC.anim.SetBool("FacingRight", PC.facingRight);
		PC.capeAnim.SetBool ("FacingRight",PC.facingRight);
		PC.anim.SetFloat ("ClimbTimer", PC.climbTime);
		if(!PC.grounded&&PC.isTouchingWall&&PC.wallCheck2&&PC.facingRight&&move>0.1f && !PC.cronened)
        {
			PC.wallDragTimer+=Time.deltaTime;
			PC.timeAirborne=0;
		}else if(!PC.grounded&&PC.isTouchingWall&&PC.wallCheck2&&!PC.facingRight&&move<-0.1f && !PC.cronened)
        {
			PC.wallDragTimer+=Time.deltaTime;
			PC.timeAirborne=0;
		}else{
			PC.wallDragTimer=0;
		}
		//anim.SetFloat ("BlinkTimer", idleTimer);
		PC.anim.SetFloat ("WallDrag", PC.wallDragTimer);
		PC.anim.SetBool ("Ground", PC.grounded);
		PC.anim.SetFloat ("ySpeed", rbody.velocity.y);
		if (PM.controlsLocked||PC.moveLocked) {
			PC.anim.SetFloat ("Speed",Mathf.Abs (rbody.velocity.x));
		}else{
			PC.anim.SetFloat ("Speed", Mathf.Abs (move));
		}
	}


	public void UseMagic(){
	if (name == "Player1") {
			if(GM.MB[0].mana<=0&&GM.MB[1].mana>0){
				GM.MB[1].UpdateManaBar(10);
			}else{
				GM.MB[0].UpdateManaBar(10);
			}
		} else{
			if(GM.MB[1].mana<=0&&GM.MB[0].mana>0){
				GM.MB[0].UpdateManaBar(10);
			}else{
				GM.MB[1].UpdateManaBar(10);
			}
		}
	}

	//stop at the end of the attack animation
	public void endOfTeleport(){
		PC.anim.SetBool ("Teleporting", false);
		PC.teleporting=false;
	}


	public void GiveReady(){
		GetComponent<Renderer>().sortingOrder = 1;
		PC.anim.SetBool ("HoldingCrystal", true);
		PC.anim.SetBool ("GivingCrystal", false);
	}

	public void OopReady(){
		GetComponent<Renderer>().sortingOrder = 1;
        rbody.isKinematic = true;
		PC.anim.SetBool ("AlleyOopReady", true);
		PC.anim.SetBool ("AlleyOoping", false);
		PC.SpiritS.OopPhase (1);
	}

    public void Unlock()
    {
        PC.rbody.constraints = RigidbodyConstraints2D.None|RigidbodyConstraints2D.FreezeRotation;
    }

	public void DoneOoping(){
		GetComponent<Renderer>().sortingOrder = 3;
        PC.stopAlleyOoping ();
    }

	//stop at the end of the attack animation
	public void endOfAttack(){
		PC.isAttacking = false;
		PC.anim.SetBool ("isAttacking", false);
		PC.anim.SetBool ("isAttackingUp", false);
		PC.anim.SetBool ("isAttackingDown", false);
	}
	
	public void DoneBurning(){
		LDS.onFire=false;
		PC.onFire=false;
		PDS.playerDead=true;
		PC.anim.SetBool ("Dying", false);
	}

	public void FullSplit(){
		PC.moveLocked = false;
		PC.fullyBisected = true;
	}

	//stop resurrecting at the end of the animation
	public void DoneResurrecting(){
		if (GM.numOfPlayers == 2) {
			PC.ControlsEnabled=true;		
		}
		PC.launching = false;
		GetComponent<Renderer>().sortingOrder = 3;
		PC.anim.SetBool ("Resurrecting", false);
		PC.anim.SetBool ("Dead", false);
		PC.rezDelay=0;
		PC.rezing=false;
		if(PC.isPlayer1){
			PM.PC1.rezing=false;
		}else{
			PM.PC2.rezing=false;
		}
	}

	public void resetLaunch(){
		PC.launching = false;
	}

	public void launch(){
        rbody.AddForce(new Vector2(0,6000));
	}
	public void Dead(){
		PDS.playerDead = true;
		PC.anim.SetBool ("Dead", true);
	}
	public void slowTime(){
		Time.timeScale = 0.2f;
	}
	
	public void regularTime(){
		Time.timeScale = 1;
	}

    public void FreezeYPos()
    {
        rbody.constraints = RigidbodyConstraints2D.FreezePositionY;
    }
    public void UnFreeze()
    {
        rbody.constraints = RigidbodyConstraints2D.None| RigidbodyConstraints2D.FreezeRotation;
    }
public void BlinkToTarget(){
		transform.position = new Vector3 (transform.position.x+(PC.blinkDir*19),transform.position.y,transform.position.z);
    }
    // stop at the end of the summon animation
    public void DoneSummoning(){
		PC.summoning = false;
		PC.anim.SetBool ("Summoning", false);
		PC.anim.SetBool ("FailedRez", false);
		PC.rezDelay=0;
		PC.rezing=false;
	}
	public void summoningLight(int on){
		if(on==1){
			LDS.resurrecting=true;
		}else if(on==0){
			LDS.resurrecting=false;
		}
	}

	public void HoldingAbsorb(){
		PC.anim.SetBool ("HoldingAbsorb", true);
	}
	public void DoneRezFailing(){
		PC.anim.SetBool ("FailedRez", false);
	}

	public void AnnounceOop(int playerNum){
		oopCounter += 1;
		if (oopCounter == alleyOops) {
            //SS.Speak(-0.76f, 0.76f, "Come here,  I'll boost you up", 1);
            if (playerNum==1){
                GM.HD [playerNum].Talk ("Come here,  I'll boost you up", 100, 50, 110, 20, 2);
			}else{
				GM.HD [playerNum].Talk ("Come here,  I'll boost you up", 100, 50, -110, 20, 2);
            }
			alleyOops*=2;
		}
	}
    public void HangIdle()
    {
        PC.anim.SetBool("HangeIdle", true);
    }
    public void StopWiggling()
    {
        PC.anim.SetBool("Wiggle",false);
    }

    public void lockPlayer(int locked)
    {
        if (locked==1)
        {
            PC.moveLocked = true;
            PC.jumpLocked = true;
            PC.BodyCollider.gameObject.layer = 9;
            PC.FeetCollider.gameObject.layer = 9;
        }else
        {
            PC.BodyCollider.gameObject.layer = 10;
            PC.FeetCollider.gameObject.layer = 10;
            PDS.COD = "Alive";
            PDS.tempOBJ = "Bannanna";
            PC.anim.SetBool("KO", false);
            PC.flipDisabled = false;
            PC.moveLocked = false;
            PC.jumpLocked = false;
        }
    }

}
