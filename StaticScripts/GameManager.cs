using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/*public class KnownSpells{
	public String name;
	public int[] spell = new int[5];
	public bool unlocked;
}*/

public class GameManager : GameStateFunctions {



    [FMODUnity.EventRef]
    string titleMusicString = "event:/Music/titles 1";
    static FMOD.Studio.EventInstance titleMusicEV;

    //public KnownSpells FireSpell = new KnownSpells ();

    public static GameManager instance = null;

    //sound vars
    public float musicVol;
    public float SFXVol;

    //player controls
    public int numberOfEnemies;
    
	public Sprite[] p1MaskCollection = new Sprite[11];
	public Sprite[] p2MaskCollection = new Sprite[11];
	public bool tutorial;
	public int singlePlayerBonus;
	public CurrentLevelScoreTracker LST;
	public AbilitiesBar[] AB= new AbilitiesBar[2];
	public Crystal[] SavedCrystals1 = new Crystal[5];
	public Crystal[] SavedCrystals2 = new Crystal[5];
	public string[] L1 = new string[2];
	public string[] R1 = new string[2];
	public string[] jStickVertical = new string[2];
	public string[] jumpbtn = new string[2];
	public string[] movebtn = new string[2];
	public string[] attackbtn = new string[2];
	public string[] grabbtn = new string[2];
	public string[] Rezbtn = new string[2];
	public string[] switchPlayersbtn = new string[2];
	public string[] givebtn = new string[2];
	public string[] slide = new string[2];
	public string[] camControls = new string[2];
	public string[] ablControlsUp = new string[2];
	public string[] ablControlsDown = new string[2];
	public string[] pause = new string[2];
	public string[] spellCastY = new string[2];
	public string[] spellCastX = new string[2];
	public string p1Controller;
	public string p2Controller;
	public int nextLvl;
    public int mainMenuPageNumber;
	public int[] TotalKingdomPoints=new int[6];
	public int currentKingdomNum;
	public Kingdom[] Kingdoms = new Kingdom[7];
	int numOfTotalLvls=100;
	public Level[] GameLevels;
	public Transform selectedPlayerPos;
	public ArcanHUDBehavior AHB;
	public ZephHUDBehavior ZHB;
	public HUD_Dialog[] HD = new HUD_Dialog[2];
	public ManaBar[] MB = new ManaBar[2];
	public ManaBar EidolonLifeBar;
	public int maxMana;
    public float masksCollected;
    public bool demoMode;
	//ints
	public int currentLevel;
	public int difficulty=1;
	public int numOfPlayers=1;
	public int camNum;
	public Restart r;
	//vector3s
	public Vector3 CheckPointPos;
    public Vector3 BossCheckPointPos;
    public bool startAtBoss;
	//strings
	public string CurrentScene;
	public PlayersManager PM;
    public Transform skullGuide;
    public CheatController CC;

    public int portalPlayer;
    //race stuff
    public bool raceMode;
    public int player1Deaths;
    public int player2Deaths;
    public int p1Win;
    public int p2Win;
    void Awake()
    {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        GameLevels = new Level[numOfTotalLvls];
        DontDestroyOnLoad(gameObject);
        setControls();
        CreateKingdomArray();
        CreateLevelArray();
        currentLevel = 1;
        if (titleMusicEV == null)
        {
            titleMusicEV = FMODUnity.RuntimeManager.CreateInstance(titleMusicString);
        }
        if (SceneManager.GetActiveScene().buildIndex < 2) {
            FMOD.Studio.PLAYBACK_STATE musicPlaystate;
            titleMusicEV.getPlaybackState(out musicPlaystate);
            if (musicPlaystate == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                titleMusicEV.start();
            }
        }else
        {
            titleMusicEV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void PlayerKilled(int playerNum){
		if (playerNum == 1) {
			ZHB.KillHUD();
		} else {
			AHB.KillHUD();
		}
	}

	public void UpdateHUD(int ablNum,int playerNum){
		if (playerNum == 1) {
			ZHB.ShowAbility (ablNum);
		} else if(playerNum==2) {    
			AHB.ShowAbility (ablNum);
		}
	}

	public void GainMana(int playerNum){
		if (playerNum == 1) {
			MB[0].UpdateManaBar(-10);
		} else if(playerNum==2) {
			MB[1].UpdateManaBar(-10);
		}
	}

	public void EidolonHit(){
		EidolonLifeBar.UpdateManaBar (10);
	}

	public void UseMana(int playerNum){
	if (playerNum == 1) {
			if(MB[0].mana<=0&&MB[1].mana>0){
				MB[1].UpdateManaBar(10);
			}else{
				MB[0].UpdateManaBar(10);
			}
		} else if(playerNum==2) {
			if(MB[1].mana<=0&&MB[0].mana>0){
				MB[0].UpdateManaBar(10);
			}else{
				MB[1].UpdateManaBar(10);
			}
		}
	}

	public void RevivePlayer(int playerNum){
		if (playerNum == 1) {
			ZHB.ReviveHUD();
		} else {
			AHB.ReviveHUD();
		}
	}

	public Vector3 GetCheckPoint(){
		Vector3 CP = CheckPointPos;
		return CP;
	}
	
	
	
	public void setControls()
	{
		if (p1Controller == "") {
			p1Controller="Xbox";
		}
		if (p2Controller == "") {
			p2Controller="Xbox";
		}
		if (numOfPlayers == 1) {
			for (int i=0; i<2; i++) {
				jStickVertical [i] = p1Controller + "_P1_Vertical";
				spellCastY [i] = p1Controller + "_" + "P1_SpellCastingY";
				spellCastX [i] = p1Controller + "_" + "P1_SpellCastingX";
				jumpbtn [i] = p1Controller + "_P1_Jump";
				movebtn [i] = p1Controller + "_P1_Move";
				attackbtn [i] = p1Controller + "_P1_Attack";
				grabbtn [i] = p1Controller + "_P1_Grab";
				Rezbtn [i] = p1Controller + "_P1_Resurrect";
				givebtn[i]=p1Controller+"_P1_Give";
				switchPlayersbtn [i] = p1Controller + "_P1_Switch";
				slide [i] = p1Controller + "_P1_Slide";
				L1 [i] = p1Controller + "_P1_L1";
				R1 [i] = p1Controller + "_P1_R1";
				ablControlsUp[i] = p1Controller+"_P1_AbilityControlsUp";
				ablControlsDown[i] = p1Controller+"_P1_AbilityControlsDown";
				camControls[i] = p1Controller+"_P1_Resurrect";
				pause[i] = p1Controller+"_P1_Pause";
			}
		} else {
			//set player 1 controls
				jStickVertical [0] = p1Controller + "_P1_Vertical";
				spellCastY [0] = p1Controller + "_" + "P1_SpellCastingY";
				spellCastX [0] = p1Controller + "_" + "P1_SpellCastingX";
				jumpbtn [0] = p1Controller + "_P1_Jump";
				movebtn [0] = p1Controller + "_P1_Move";
				attackbtn [0] = p1Controller + "_P1_Attack";
				grabbtn [0] = p1Controller + "_P1_Grab";
				Rezbtn [0] = p1Controller + "_P1_Resurrect";
				givebtn[0]=p1Controller+"_P1_Give";
				switchPlayersbtn [0] = p1Controller + "_P1_Switch";
				slide [0] = p1Controller + "_P1_Slide";
				L1 [0] = p1Controller + "_P1_L1";
				R1 [0] = p1Controller + "_P1_R1";
				ablControlsUp[0] = p1Controller+"_P1_AbilityControlsUp";
				ablControlsDown[0] = p1Controller+"_P1_AbilityControlsDown";
				camControls[0] = p1Controller+"_P1_Resurrect";
				pause[0] = p1Controller+"_P1_Pause";
			//set player 2 controls
				jStickVertical [1] = p2Controller + "_P2_Vertical";
				spellCastY [1] = p2Controller + "_" + "P2_SpellCastingY";
				spellCastX [1] = p2Controller + "_" + "P2_SpellCastingX";
				jumpbtn [1] = p2Controller + "_P2_Jump";
				movebtn [1] = p2Controller + "_P2_Move";
				attackbtn [1] = p2Controller + "_P2_Attack";
				grabbtn [1] = p2Controller + "_P2_Grab";
				Rezbtn [1] = p2Controller + "_P2_Resurrect";
				givebtn[1]=p1Controller+"_P2_Give";
				switchPlayersbtn [1] = p1Controller + "_P1_Switch";
				slide [1] = p2Controller + "_P2_Slide";
				L1 [1] = p2Controller + "_P2_L1";
				R1 [1] = p2Controller + "_P2_R1";
				ablControlsUp[1] = p2Controller+"_P1_AbilityControlsUp";
				ablControlsDown[1] = p2Controller+"_P1_AbilityControlsDown";
				camControls[1] = p2Controller+"_P2_Resurrect";
				pause[1] = p2Controller+"_P2_Pause";
		}
	}
	public void loadNextLevel(){
        if (startAtBoss)
        {
            startAtBoss = false;
        }
        //SceneManager.LoadScene(nextLvl);
        if (raceMode)
        {
            SceneManager.LoadScene("Race_Score_Screen");
        }
        else
        {
            SceneManager.LoadScene("LevelCompletedScreen");
        }
	}

	public void SaveCrystalsInfo(){
		for(int i= 0;i<5;i++){
			SavedCrystals1[i]=AB[0].CrystalList[i];
			SavedCrystals2[i]=AB[1].CrystalList[i];
		}
	}

	public void CreateKingdomArray(){
	if (Kingdoms [0] == null) {
			for(int i =0;i<Kingdoms.Length;i++){
				Kingdom NewK=new Kingdom();
				Kingdoms[i]=NewK;
				if(i==0){
					Kingdoms[i].firstLevel=1;
					Kingdoms[i].lastLevel=20;
					Kingdoms[i].unLocked=true;
				}else if(i==1){
					Kingdoms[i].firstLevel=21;
					Kingdoms[i].lastLevel=40;
				}else{
					Kingdoms[i].firstLevel=(20*(i-1))+20+i;
					Kingdoms[i].lastLevel=(20*i)+20;
				}
			}
		}
	}
	public void CreateLevelArray(){
		if (GameLevels [0] == null) {
			for(int i =0;i<GameLevels.Length;i++){
				Level NewL=new Level();
				GameLevels[i]=NewL;
				if(i<20){
					GameLevels[i].locked=false;
				}else{
					GameLevels[i].locked=true;
				}
			}
		}
	}


	/*public void CreateKnownSpells(){
		FireSpell.name = "Dragon Wings";
		FireSpell.spell[0]=1;
		FireSpell.spell[1]=2;
		FireSpell.spell[2]=3;
		FireSpell.spell[3]=4;
		FireSpell.spell[4]=5;
		FireSpell.unlocked=true;
	}*/
	public void SaveGame()
	{
		BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
			PlayerData data = new PlayerData ();
		for (int i=0; i<numOfTotalLvls; i++) {
			data.score [i] = GameLevels [i].highScore;
			data.time [i] = GameLevels [i].recordCompletionTime;
			data.numOfDeaths [i] = GameLevels [i].recordnumOfDeaths;
			data.locked [i] = GameLevels [i].locked;
			if(i<SavedCrystals1.Length){
			data.p1Crystalunlocked[i]=SavedCrystals1[i].unlocked;
			data.p2Crystalunlocked[i]=SavedCrystals2[i].unlocked;
			}
		}

		bf.Serialize (file, data);
		file.Close ();
	}

	public void LoadGame()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData loadData = (PlayerData)bf.Deserialize (file);
			file.Close ();
			for (int i=0; i<numOfTotalLvls; i++) {
				GameLevels [i].highScore = loadData.score [i];
				GameLevels [i].recordCompletionTime = loadData.time [i];
				GameLevels [i].recordnumOfDeaths = loadData.numOfDeaths [i];
				GameLevels [i].locked = loadData.locked [i];
			}
				LoadCrystalData();
			for(int i = 0;i<5;i++){
				if(i<5){
					SavedCrystals1[i].unlocked=loadData.p1Crystalunlocked[i];
					SavedCrystals2[i].unlocked=loadData.p2Crystalunlocked[i];
				}
			}
			Debug.Log ("File loaded");
		} else {
			Debug.Log ("Load file not found");
		}
	}
	// mask data
	public void addNewMask(int playerNumber, Sprite newMask){
		if (playerNumber == 1) {
			for (int i=0; i<p1MaskCollection.Length; i++) {
				if (p1MaskCollection [i] == null) {
					p1MaskCollection [i] = newMask;
					break;
				}
			}
		} else if(playerNumber==2){
			for (int i=0; i<p2MaskCollection.Length; i++) {
				if (p2MaskCollection [i] == null) {
					p2MaskCollection [i] = newMask;
					break;
				}
			}
		}
	}
	public void LoadMaskData()
	{
	
	}

	public void LoadCrystalData(){
		for(int i =0;i<2;i++){
		Crystal MetalCrystal = new Crystal ();
		Crystal NecroCrystal = new Crystal ();
		Crystal ZooCrystal = new Crystal ();
		Crystal CaveCrystal = new Crystal ();
		Crystal	TimeCrystal0 = new Crystal ();
			MetalCrystal.name = "Dragon Wings";
			ZooCrystal.name = "Spirit Animal";
			NecroCrystal.name = "Absorb";
			CaveCrystal.name = "Stone Skin";
			TimeCrystal0.name = "Blink";
			if(i==0){
				SavedCrystals1 [4]=NecroCrystal;
				SavedCrystals1 [3]=MetalCrystal;
				SavedCrystals1 [2]=ZooCrystal;
				SavedCrystals1 [1]=CaveCrystal;
				SavedCrystals1 [0]=TimeCrystal0;
				for(int j = 0;j<5;j++){
					SavedCrystals1[j].enabled=false;
				}
			}else{
				SavedCrystals2 [4]=NecroCrystal;
				SavedCrystals2 [3]=MetalCrystal;
				SavedCrystals2 [2]=ZooCrystal;
				SavedCrystals2 [1]=CaveCrystal;
				SavedCrystals2 [0]=TimeCrystal0;
				for(int j = 0;j<5;j++){
					SavedCrystals2[j].enabled=false;
				}
			}
		}
	}
}
public class Kingdom
	{
	public bool unLocked;
	public int firstLevel;
	public int lastLevel;
}
public class Level
{
	public string KNGDM;
	public string lvlName;
	public bool locked;
	public float lastCompletionTime;
	public int lastCompletionDifficulty;
	public int lastnumOfDeaths;
	public float recordCompletionTime;
	public int recordCompletionDifficulty;
	public int recordnumOfDeaths;
	public int score;
	public int highScore;
}
[Serializable]
class PlayerData
{
	public int[] score=new int[100];
	public int[] numOfDeaths = new int[100];
	public float[] time = new float[100];
	public bool[] locked = new bool[100];
	public bool[] p1Crystalunlocked = new bool[5];
	public bool[] p2Crystalunlocked = new bool[5];
}
