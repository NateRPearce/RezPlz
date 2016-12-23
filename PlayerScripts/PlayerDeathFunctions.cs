using UnityEngine;
using System.Collections;

public class PlayerDeathFunctions : DeathScript {

	CreateCorpses CC;
	public Collider2D hitBox;
	
	public Transform bloodSplurt;
	public Transform bloodSplurt2;
	public Transform HangingDeadBody;
	public Transform LeftSideDeadBody;
	public Transform RightSideDeadBody;
	public Transform GibCorpse;
	public Transform Explosion;
    public Transform Explosion_NoFire;
    public Transform Parent;
    public PlayerSounds PS;
	public GameObject shadow;

	public Animator anim;
	public Animator leftSideAnim;
	public Animator rightSideAnim;

	public bool playerDead=false;
	public bool playerKilled;
	public bool attackDeflected;
	public string[] weakness = new string[1];
	public string COD;
	public float TOD;//time of dying
	public float finalTOD;// time when officially dead
	public float deflectionTimer;
	public float DeadTimer;
    public string tempOBJ;


	public void RunStart()
	{
		FindGM ();
		FindCBS ();
		FindPM ();
		Parent = transform.parent;
		anim = GetComponent<Animator> ();
		FindPC ();
        PS = GetComponent<PlayerSounds>();
		FindLDS ();
		hitBox = GetComponent<Collider2D> ();
		rightSideAnim = RightSideDeadBody.GetComponent<Animator> ();
		leftSideAnim = LeftSideDeadBody.GetComponent<Animator> ();
		if (!PM.controlsLocked) {
			resetLiveAnims();
			anim.SetBool ("Dying", false);
			ResetAnimCODs();
		}
		CC = GetComponent<CreateCorpses> ();
	}



	//resurrecting
	public void RezMe(){
		if(PC.isPlayer1&&PM.PC1.rezing){
			PC.rezing=true;
			PlayerAliveReset();
		}else if(!PC.isPlayer1&&PM.PC2.rezing){
			PC.rezing=true;
			PlayerAliveReset();
		}
	}



	public void PlayerAliveReset(){
		if(!PM.eventHappening){
			PM.controlsLocked=false;
        }
        anim.SetBool("Cronen", false);
        PC.cronened = false;
        tempOBJ = "Bannanna";
        DisableKO();
        if (playerDead) {
            //reset the spike corpses so they can squish onto the spikes
            PC.flipDisabled = false;
			CC.GCR.ResetTriggered();
			CC.GCL.ResetTriggered();
			CC.corpseCreated = false;
			CC.HeadCreated=false;
			CC.torsoCreated=false;
			PC.AB.HideAllAbilities ();
			LDS.energyAbsorbed = false;
			Vector3 theScale = transform.localScale;
			theScale.y = Mathf.Abs (theScale.y);
			transform.localScale = theScale;
			PC.DoneRolling ();
			finalTOD = 0;
			transform.parent = Parent;
			PC.onFire = false;
			PC.bisected=false;
			PC.fullyBisected=false;
			PC.exploding = false;
			PC.swinging = false;
			LDS.onFire = false;
            PC.hanging = false;
            PC.moveLocked = false;
			anim.SetBool ("Swinging", PC.swinging);
			transform.rotation = PC.startingRotation;
			shadow.SetActive (true);
			HangingDeadBody.gameObject.SetActive (false);
            PC.isBlinking = false;
            TOD = 0;
            PC.rbody.gravityScale = 1;
            PC.rbody.mass = 1;
			PC.BodyCollider.GetComponent<Collider2D>().enabled = true;
			PC.FeetCollider.GetComponent<Collider2D>().enabled = true;
			PC.rbody.isKinematic = false;
            PC.rbody.constraints = RigidbodyConstraints2D.None;
            PC.rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            COD = "Alive";
			PC.dying = false;
			playerKilled = false;
			anim.SetBool ("Resurrecting", true);
			PC.RezFacingCheck ();
			anim.SetBool ("Dying", false);
			anim.SetBool ("isAttacking", false);
            PC.anim.SetBool("HangeIdle", false);
            anim.SetBool ("Hanging", false);
			ResetAnimCODs ();
			if(name=="Player1"){
				GM.RevivePlayer(1);
			}else{
				GM.RevivePlayer(2);
			}
		}
		playerDead = false;
	}


	public void resetLiveAnims(){
        anim.SetBool("Panicing",false);
		anim.SetBool ("AlleyOopReady", false);
		anim.SetBool ("AlleyOopToss", false);
		anim.SetBool ("AlleyOoping", false);
		anim.SetBool ("isSliding", false);
		anim.SetBool ("Tossed", false);
		anim.SetBool ("isAttacking", false);
		anim.SetBool ("isAttackingUp", false);
		anim.SetBool ("isAttackingDown", false);
		anim.SetBool ("Swinging", false);
		anim.SetBool ("Summoning", false);
		anim.SetBool ("Teleporting", false);
		anim.SetBool ("Resurrecting", false);
		anim.SetBool ("WallDragging", false);
    }



    public void ResetAnimCODs(){
        PS.PlayInLavaSound(false);
        anim.SetBool("CODSting",false);
		anim.SetBool("CODDecap",false);
		anim.SetBool("CODDeathSplit",false);
		anim.SetBool ("CODGroundSpikes", false);
		anim.SetBool ("CODWallSpikes", false);
		anim.SetBool ("CODCeilingSpikes", false);
		anim.SetBool ("CODFire", false);
		anim.SetBool ("CODLava", false);
		anim.SetBool("CODAcid",false);
		anim.SetBool ("CODExplode", false);
		anim.SetBool ("CODBisected", false);
	}
	
	public override void HitBy(string OBJ){
        if (OBJ == tempOBJ)
        {
            return;
        }
		if ((LDS.energyAbsorbed&&!PC.stoneSkinActivated)||PC.cronened) {
			GetComponent<Renderer>().enabled=false;
			shadow.SetActive(false);
			anim.SetBool("Dying",true);
			anim.SetBool("CODExplode",true);
			PC.rbody.velocity=new Vector2(0,0);
			if(!playerDead){
				Instantiate(GibCorpse,transform.position,transform.rotation);
                if (PC.cronened)
                {
                    Instantiate(Explosion_NoFire, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(Explosion, transform.position, transform.rotation);
                }
            }
            playerDead =true;
			return;
		}
		for(int i = 0; i<weakness.Length;i++){
			if (COD == weakness [i]&&!attackDeflected) {
				if (COD != OBJ){
					playerDead = false;
					ResetAnimCODs ();
				}
			}
		}
		
		COD = OBJ;
		switch(OBJ){
		case"Decap":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);
                    PC.FeetCollider.gameObject.layer = 11;
                    PC.onFire = false;
                    PS.stopWallDragSound();
                    shadow.SetActive(false);
				    anim.SetBool("Dying",true);
				    anim.SetBool("CODDecap",true);
				}
                PC.rbody.velocity = new Vector2(0, 0);
                playerDead =true;
			break;
		case"Explosion":
			GetComponent<Renderer>().enabled=false;
			shadow.SetActive(false);
			anim.SetBool("Dead",true);
			anim.SetBool("CODExplode",true);
			GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
			if(!playerDead){
                    Transform newGib;
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                    PS.stopWallDragSound();
                    PS.PlayExploded();
                    newGib = Instantiate(GibCorpse,new Vector3(transform.position.x,transform.position.y,-1),transform.rotation)as Transform;
                    Rigidbody2D[] rbodies;
                    rbodies = newGib.GetComponentsInChildren<Rigidbody2D>();
                    foreach (Rigidbody2D r in rbodies)
                    {
                        int x = Random.Range(-100, 100);
                        int y = Random.Range(-100, 100);
                        r.AddForce(new Vector2(x, y));
                    }
                }

                PC.rbody.velocity = new Vector2(0, 0);
                playerDead =true;
			break;
		case"DeathSplit":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                GetComponent<Renderer>().enabled=false;
			anim.SetBool("Dying",true);
			anim.SetBool("CODDeathSplit",true);
			GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
			if(!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                    PS.stopWallDragSound();
                    PS.PlayDeathSplitSound();
                    PS.playSquish();
                    PS.playBoneBreak();
                Instantiate(bloodSplurt,transform.position,bloodSplurt.rotation);
				Instantiate(bloodSplurt2,new Vector3(transform.position.x,transform.position.y,-24),bloodSplurt2.rotation);
				if(PC.facingRight){
					LeftSideDeadBody.transform.localScale=new Vector3(10,10,10);
					RightSideDeadBody.transform.localScale=new Vector3(10,10,10);
				}else{
					LeftSideDeadBody.transform.localScale=new Vector3(-10,10,10);
					RightSideDeadBody.transform.localScale=new Vector3(-10,10,10);
				}
				LeftSideDeadBody.transform.position=transform.position;
				LeftSideDeadBody.gameObject.SetActive(true);
				RightSideDeadBody.transform.position=transform.position;
				RightSideDeadBody.gameObject.SetActive(true);
				rightSideAnim.SetBool("Active",true);
				leftSideAnim.SetBool("Active",true);
			}

                PC.rbody.velocity = new Vector2(0, 0);
                playerDead =true;
			break;
		case"Fire":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead){
                    DisableKO();
                    PS.stopWallDragSound();
                    LDS.onFire=true;
				PC.dying=true;
                    if (!PC.onFire)
                    {
                        PS.PlayOnFireSound();
                        PC.onFire = true;
                    }

				anim.SetBool("CODFire",true);
				anim.SetBool("Dying",true);
			}
			break;
		case"Bisect":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                    PS.stopWallDragSound();
                    PC.dying=true;
				PC.bisected=true;
				anim.SetBool("CODBisected",true);
				anim.SetBool("Dying",true);
			}
			break;
		case"Sting":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                    PS.stopWallDragSound();
                    PC.dying=true;
				anim.SetBool("CODSting",true);
				anim.SetBool("Dying",true);
			}
			break;
		case"Fireball":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead){
                    DisableKO();
                    PS.stopWallDragSound();
                    LDS.onFire=true;
				PC.dying=true;
                    if (!PC.onFire)
                    {
                        PS.PlayOnFireSound();
                        PC.onFire = true;
                    }

				anim.SetBool("CODFire",true);
				anim.SetBool("Dying",true);
			}
			break;
		case "GroundSpikes":
                if (anim.GetBool("FullStone"))
                {
                    if (PC.rbody.constraints != (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation))
                    {
                        PC.rbody.isKinematic = true; 
                        PC.rbody.constraints = RigidbodyConstraints2D.None;
                        PC.rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    }
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                DisableKO();
                PC.onFire = false;
                PS.stopWallDragSound();
                PS.playSquish();
                GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                anim.SetBool("Dying",true);
			    anim.SetBool("CODGroundSpikes",true);
                PC.rbody.velocity = new Vector2(0, 0);
                PC.dying=true;
			break;
		case "Lava":
			if(!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    if (!PC.dying)
                    {
                        PS.PlayInLavaSound(true);
                    }
                    PC.onFire = false;
                    PS.stopWallDragSound();
                anim.SetBool("Dying",true);
				anim.SetBool("CODLava",true);
				GetComponent<Rigidbody2D>().mass=3;
                PC.rbody.velocity = new Vector2(0, 0);
                PC.dying=true;
				anim.SetBool("Dead",true);
			}
			playerDead=true;
			break;
		case"InstaKillLava":
			if(!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                LDS.onFire=true;
				PC.dying=true;
				PC.rbody.velocity=new Vector2(PC.rbody.velocity.x/2, PC.rbody.velocity.y);
				anim.SetBool("CODFire",true);
				anim.SetBool("Dying",true);
				anim.SetBool("Dead",true);
			}
			playerDead=true;
			break;
		case"Acid":
			if(!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                PS.stopWallDragSound();
                PC.dying=true;
				GetComponent<Rigidbody2D>().isKinematic=true;
				anim.SetBool("CODAcid",true);
				anim.SetBool("Dying",true);
				anim.SetBool("Dead",true);
			}
                PC.rbody.velocity = new Vector2(0, 0);
                playerDead =true;
			break;
		case "WallSpikes":
			if(anim.GetBool("FullStone")){
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
			}
			if(!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                    PS.stopWallDragSound();
                PS.playSquish();
				anim.SetBool("Dying",true);
				anim.SetBool("CODWallSpikes",true);
			}
                PC.rbody.velocity = new Vector2(0, 0);
                playerDead =true;
			break;
		case "CeilingSpikes":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead){
                    DisableKO();
                    GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
                    Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

                    PC.onFire = false;
                    PS.stopWallDragSound();
                anim.SetBool("Dying",true);
				anim.SetBool("CODCeilingSpikes",true);
				HangingDeadBody.gameObject.SetActive(true);
				Transform newHangingBody =Instantiate(HangingDeadBody,transform.position,Quaternion.AngleAxis(90,Vector3.forward)) as Transform;
			if(newHangingBody.GetComponentsInChildren<Dissolve>()!=null){
				Dissolve[] NBD= new Dissolve[4];
				NBD = newHangingBody.GetComponentsInChildren<Dissolve>();
				foreach(Dissolve D in NBD){
					D.PCcontroller = PC;
				}
			}
			}
                PC.rbody.velocity = new Vector2(0, 0);
                playerDead =true;
			break;
            case "AttackCollider":
                if (anim.GetBool("FullStone"))
                {
                    COD = "Alive";
                    OBJ = "Alive";
                    return;
                }
                if (!playerDead)
                {
                    Debug.Log("KO");
                    anim.SetBool("KO", true);
                }
                break;
            default:
			break;
		}
        tempOBJ = OBJ;
	}


	public void deflectionCooldown(){
		deflectionTimer += Time.deltaTime;
		if (deflectionTimer > 0.2f) {
			deflectionTimer = 0;
			attackDeflected = false;
			hitBox.enabled=true;
		} else {
			hitBox.enabled=false;
		}
	}

    void DisableKO()
    {
        PC.BodyCollider.gameObject.layer = 10;
        PC.FeetCollider.gameObject.layer = 10;
        PC.anim.SetBool("KO", false);
        PC.flipDisabled = false;
        PC.moveLocked = false;
        PC.jumpLocked = false;
    }

	public void contactCheck(){
		if (playerDead || PC.dying) {
			if(anim.GetBool("FailedRez")){
				anim.SetBool("FailedRez",false);
			}
			anim.SetBool ("AlleyOoping",false);
			anim.SetBool ("AlleyOopReady",false);
			anim.SetBool ("AlleyOopToss",false);
			return;		
		}
	}
	
	
	
	public void DeadCheck()
	{
		if (((PC.onFire||PC.bisected)&&TOD>2)&&!playerDead) {
            GM.GameLevels[GM.currentLevel].lastnumOfDeaths += 1;
            Debug.Log(GM.GameLevels[GM.currentLevel].lastnumOfDeaths);

            anim.SetBool("Dead",true);
			anim.SetBool ("Dying", false);
			playerDead=true;
		}
			anim.SetFloat ("TOD", TOD);
		if (playerDead || PC.dying) {
            TOD += Time.deltaTime;
		} else {
			TOD=0;
		}
        if (TOD > 0 && TOD < 1)
        {
            //Debug.Log("Vibrate");
           //XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 5, 5);
        }
        else
        {
           // XInputDotNetPure.GamePad.SetVibration(XInputDotNetPure.PlayerIndex.One, 0, 0);
        }
        if (playerDead) {
            if (GM.raceMode)
            {
                return;
            }
			if (finalTOD < 0.2f && (transform.position.x > CBS.transform.position.x + 20 || transform.position.x < CBS.transform.position.x - 20)) {
				if (GM.numOfPlayers == 1&&!GM.PM.eventHappening) {
                   // CBS.deadIndicator.GetComponent<Renderer>().enabled = true;
                }
            } else {
				CBS.deadIndicator.GetComponent<Renderer>().enabled = false;
			    }
			if(GM.selectedPlayerPos.position.x==transform.position.x&&finalTOD<0.6f){
				CBS.cameraEvent=true;
				CBS.EventLocation=new Vector3 (CBS.cameraPositionX, CBS.cameraPositionY, CBS.transform.position.z);
			}else if(finalTOD>0.6f&&finalTOD<1){
				CBS.cameraEvent=false;
				PM.controlsLocked = false;	
			}
			finalTOD += Time.deltaTime;
		}

		if (playerKilled) {
			return;
		}

		if (transform.position.y < -35&&!PC.rezing) {
            if (!playerDead)
            {
                PS.playFallSound();
                anim.SetBool("Dead", true);
                anim.SetBool("CODExplode", true);
                playerDead = true;
                PC.dying = true;
            }
		}

		if (PC.dying||playerDead) {
			anim.SetBool("Stoned",false);
			anim.SetBool("FullStone",false);
			if(PC.stoneSkinActivated){
				Instantiate(PC.StoneSkinFlakes,new Vector3(transform.position.x+PC.StoneSkinOffset.x,transform.position.y+PC.StoneSkinOffset.y,transform.position.z),Quaternion.identity);
			}
			PC.stoneSkinActivated=false;
			GetComponent<Rigidbody2D>().mass=1;
			PC.AB.HideAllAbilities ();
			PC.cape.GetComponent<Renderer>().enabled=false;
			GetComponent<Renderer>().sortingOrder = 0;
			PC.summoning=false;
			PC.rezing=false;
			resetLiveAnims();
			PC.dragSmoke.GetComponent<Renderer>().enabled=false;
		}else{
			if(!anim.GetBool("AlleyOopReady")||!anim.GetBool("GivingCrystal")){
				GetComponent<Renderer>().sortingOrder = 3;
			}
		}
		if (playerDead) {
			if(name=="Player1"){
				GM.PlayerKilled(1);
			}else{
				GM.PlayerKilled(2);
			}
			PC.suicideTimer=0;
			shadow.SetActive(false);
			PC.ControlsEnabled=false;	
			playerKilled=true;
		}
	}
}
