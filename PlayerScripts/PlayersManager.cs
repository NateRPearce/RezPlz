using UnityEngine;
using System.Collections;

public class PlayersManager : GameStateFunctions {

    
	//transforms
	public Transform Player1;
	public Transform Player2;

	//bools
	public bool eventHappening;
	public bool controlsLocked;
	private bool bothEnabled;
	private bool bothDisabled;
    public bool raceOver;
	//scripts
	public PlayerControls PC1;
	public PlayerControls PC2;
	public PlayerDeathScript PDS1;
	public PlayerDeathScript PDS2;

	//floats
	public float startDelay;
	public float gameTime;
	private float deadTimer;
	private float doubleEorDTimer;

    public bool player1FoundDead;
    public bool player2FoundDead;
    public RaceController RC;
    float p1TimeOffScreen;
    float p2TimeOffScreen;

    void Start () {
		FindGM ();
        FindCBS();
        GM.player1Deaths = 0;
        GM.player2Deaths = 0;
		GM.PM = this;
		FindScripts ();
		GM.selectedPlayerPos = Player1;
        if (GM.startAtBoss)
        {
            transform.position = GM.BossCheckPointPos;
        }
        StartCoroutine("CheckForRace");
        InvokeRepeating("CamFixer", Time.deltaTime, 0.1f);
        StartCoroutine("OffScreenDelay");
    }
	IEnumerator CheckForRace()
    {
        yield return new WaitForEndOfFrame();
        if (GM.raceMode)
        {
            GM.numOfPlayers = 2;
            GM.setControls();
            GM.PM.PC1.CheckControls();
            GM.PM.PC2.CheckControls();
        }
    }
    void FixedUpdate(){
        if (PC1.rbody.velocity.y < -150)
        {
            PC1.rbody.velocity = new Vector2(PC1.rbody.velocity.x, -150);
        }
        if (PC2.rbody.velocity.y < -150)
        {
            PC2.rbody.velocity = new Vector2(PC2.rbody.velocity.x, -150);
        }
        if (startDelay > gameTime) 
		{
			SDelay();
		} else if(startDelay!=0) 
		{
			eventHappening = false;
			controlsLocked = false;
		}

		if (GM.numOfPlayers == 1) 
		{
			controlsEnabledCheck();

			if(bothEnabled||bothDisabled&&!controlsLocked)
			{
				DoubleEorDFix();
			}
		}
		WhoIsDead ();

	}


	private void FindScripts()
	{
		PC1 = Player1.GetComponent<PlayerControls> ();
		PC2 = Player2.GetComponent<PlayerControls> ();
		PDS1 = Player1.GetComponent<PlayerDeathScript> ();
		PDS2 = Player2.GetComponent<PlayerDeathScript> ();
		PC1.isPlayer1 = true;
		PC2.isPlayer1 = false;
		PC1.otherPlayer = Player2;
		PC2.otherPlayer = Player1;
		PC1.otherPC = PC2.GetComponent<PlayerControls> ();
		PC2.otherPC = PC1.GetComponent<PlayerControls> ();
	}


	//ensures that in 1 player mode only 1 player is enabled at a time
	void controlsEnabledCheck()
	{
		if(GM.numOfPlayers==1){
			if(!PDS1.playerDead&&!PDS2.playerDead){
				if(PC1.ControlsEnabled&&PC2.ControlsEnabled){
					bothEnabled=true;
				}
				if(!PC1.ControlsEnabled&&!PC2.ControlsEnabled){
					bothDisabled=true;
				}
			}
			if(PDS2.playerDead&&!PDS1.playerDead&&!PC1.ControlsEnabled&&!eventHappening){
				PC1.ControlsEnabled=true;
				StartCoroutine("TempLockControls");
			}else if(!PDS2.playerDead&&PDS1.playerDead&&!PC2.ControlsEnabled&&!eventHappening){
				PC2.ControlsEnabled=true;
				StartCoroutine("TempLockControls");
			}
		}
	}


	//If both players dead, then rez both players after delay
	private void WhoIsDead()
	{
        if (GM.raceMode)
        {
            if (GM.PM.PDS1.playerDead)
            {
                RaceRezCheck(1);
            }
            if (GM.PM.PDS2.playerDead)
            {
                RaceRezCheck(2);
            }
            return;
        }


        if (PDS1.playerDead && PDS2.playerDead) {
				deadTimer+=Time.deltaTime;		
			if (GM.MB [0].mana <=0 && GM.MB [1].mana <= 0) {
				GM.r.restartLvl=true;
				return;
			}
			if (deadTimer > 1.5f) {
				BothPlayersRez();
				if(GM.MB [0].mana >=20 && GM.MB [1].mana <= 0){
					GM.MB[0].UpdateManaBar(20);
					deadTimer=0;
					return;
				}
				if(GM.MB [1].mana >=20 && GM.MB [0].mana <= 0){
					GM.MB[1].UpdateManaBar(20);
					deadTimer=0;
					return;
				}
				GM.MB[0].UpdateManaBar(10);
				GM.MB[1].UpdateManaBar(10);
				deadTimer=0;
			}
		}
	}

	IEnumerator TempLockControls(){
		GM.PM.controlsLocked = true;
		yield return new WaitForSeconds (0.5f);
		GM.PM.controlsLocked = false;
	}

    public void RaceRezCheck(int playerNum)
    {
        if (playerNum==1)
        {
            if (player1FoundDead)
            {
                return;
            }
            RC.CheckPointCheck(1);
            StartCoroutine("RezP1");                
        }
        if (playerNum==2)
        {
            if (player2FoundDead)
            {

                return;
            }
            RC.CheckPointCheck(2);
            StartCoroutine("RezP2");
        }
    }

	public void BothPlayersRez()
	{
        if (GM.numOfPlayers == 1)
        {
            GM.camNum = 1;
        }
		if (GM.MB [0].mana > 0||GM.MB [1].mana >= 20) {
			PC1.anim.SetBool ("Resurrecting", true);
			PC1.transform.position = new Vector3 (GM.GetCheckPoint ().x - 4, GM.GetCheckPoint ().y, PC1.transform.position.z);
			PC1.rezing = true;
			PDS1.PlayerAliveReset ();
		}
		if (GM.MB [1].mana > 0||GM.MB [0].mana >= 20) {
			PC2.anim.SetBool ("Resurrecting", true);
			PC2.transform.position = new Vector3 (GM.GetCheckPoint ().x + 4, GM.GetCheckPoint ().y, PC2.transform.position.z);
			PC2.rezing = true;
			PDS2.PlayerAliveReset ();
		}
	}


	//Start Delay
	private void SDelay()
	{
		gameTime += Time.deltaTime;
		Player1.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, Player1.GetComponent<Rigidbody2D>().velocity.y);
		Player2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, Player2.GetComponent<Rigidbody2D>().velocity.y);
		eventHappening = true;
		controlsLocked = true;
	}


	//if both players are enabled or both disabled. fix it
	private void DoubleEorDFix()
	{
		if (bothEnabled || bothDisabled && !controlsLocked) {
			doubleEorDTimer += Time.deltaTime;
			if (doubleEorDTimer > 0.1f) {
				PC1.ControlsEnabled = true;	
				PC2.ControlsEnabled = false;
				bothEnabled = false;
				bothDisabled = false;
			}
		} else {
			doubleEorDTimer = 0;
		}
	}

    void CamFixer()
    {
        if (GM.selectedPlayerPos == PC1.transform && !PC1.ControlsEnabled && !eventHappening && !PDS1.playerDead)
        {
            GM.camNum = 2;
        }
        if (GM.selectedPlayerPos == PC2.transform && !PC2.ControlsEnabled && !eventHappening&&!PDS2.playerDead)
        {
            GM.camNum = 1;
        }
    }
    IEnumerator RezP1()
    {
        player1FoundDead = true;
        yield return new WaitForSeconds(0.5f);
        GM.player1Deaths += 1;
        PC1.anim.SetBool("Resurrecting", true);
        PC1.transform.position = new Vector3(RC.p1CheckPoint.position.x + 1, RC.p1CheckPoint.position.y, PC1.transform.position.z);
        PC1.rezing = true;
        PDS1.PlayerAliveReset();
        player1FoundDead = false;
    }

    IEnumerator RezP2()
    {
        player2FoundDead = true;
        yield return new WaitForSeconds(0.5f);
        GM.player2Deaths += 1;
        PC2.anim.SetBool("Resurrecting", true);
        PC2.transform.position = new Vector3(RC.p2CheckPoint.position.x - 1, RC.p2CheckPoint.position.y, PC2.transform.position.z);
        PC2.rezing = true;
        PDS2.PlayerAliveReset();
        player2FoundDead = false;
    }

    IEnumerator OffScreenDelay()
    {
        yield return new WaitForSeconds(2);
        InvokeRepeating("OffScreenCheck", Time.deltaTime, 0.1f);
    }

    public void OffScreenCheck()
    {
        if (GM != null && CBS != null&&!raceOver&&GM.raceMode)
        {
            if (GM.PM.PC1.transform.position.x < CBS.cameraPositionX - 70)
            {
                p1TimeOffScreen += 0.1f;
                p2TimeOffScreen = 0;
            }else if(GM.PM.PC2.transform.position.x < CBS.cameraPositionX - 70)
            {
                p2TimeOffScreen += 0.1f;
                p1TimeOffScreen = 0;
            }else
            {
                p1TimeOffScreen = 0;
                p2TimeOffScreen = 0;
            }
            if (p1TimeOffScreen > 1)
            {
                GM.PM.PDS1.playerDead = true;
                p1TimeOffScreen = 0;
            }
            if (p2TimeOffScreen > 1)
            {
                GM.PM.PDS2.playerDead = true;
                p2TimeOffScreen = 0;
            }
        }
    }
    }
