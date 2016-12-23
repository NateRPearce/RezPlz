using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelEnteranceControllerScript : GameStateFunctions {

	public Transform KingdomController;
	public int kingdomModifier;
	public float direction;
	public int doorNumber;
	public int difficulty;
	public Text[] LevelNumber = new Text[2];
	public Text[] DiffText = new Text[2];
	public GameObject StatPanel;
	public Text[] HighScore = new Text[2];
	public Text[] compTime = new Text[2];
	public Text[] Difficulty = new Text[2];
	public Text[] NOD = new Text[2];
	public float HoldTimer;
	public bool coolDown;
	bool DifCooldown;
	float t;
	void Start(){
		FindGM ();
		CheckModifier (GM.currentKingdomNum);
		doorNumber = 1+kingdomModifier;
		difficulty = 1;
		LevelNumber[0].text="Level " + doorNumber;
		LevelNumber[1].text="Level " + doorNumber;
		FillLevelStats ();
	}
	void Update(){
		if (coolDown) {
			return;
		}
		if (!coolDown && (Input.GetAxis (GM.movebtn [0]) > 0.5 || Input.GetAxis (GM.movebtn [1]) > 0.5)) {
			HoldTimer += 0.5f;
			if(HoldTimer>5){
				HoldTimer=5;
			}
			if (GM.currentKingdomNum == 0) {
				if (doorNumber < 45) {
						direction =(int)(-1*HoldTimer);
				}
			} else {
				if (doorNumber < kingdomModifier + 30) {
					direction = (int)(-1*HoldTimer);
				}
			}
		} else if (!coolDown && (Input.GetAxis (GM.movebtn [0]) < -0.5 || Input.GetAxis (GM.movebtn [1]) < -0.5)) {
			HoldTimer += 0.5f;
			if(HoldTimer>5){
				HoldTimer=5;
			}
			if (doorNumber > 1 + kingdomModifier) {
				direction = (int)HoldTimer;				
			}
		} else {
			HoldTimer=1.5f;
		}
		if (direction > 0) {
			doorNumber -= 1;
			coolDown = true;
		} else if (direction < 0) {
			doorNumber += 1;
			coolDown = true;
		}
		if (DifCooldown) {
			return;
		}
		if(Input.GetAxis(GM.spellCastX[0])>0.5||Input.GetAxis(GM.spellCastX[1])>0.5){
			if(difficulty<3){
				difficulty+=1;
				DisplayDifficulty(difficulty);
				DifCooldown=true;
			}
		}else if(Input.GetAxis(GM.spellCastX[0])<-0.5||Input.GetAxis(GM.spellCastX[1])<-0.5){
			if(difficulty>1){
				difficulty-=1;	
				DisplayDifficulty(difficulty);
				DifCooldown=true;
			}
		}
	}
	void FixedUpdate(){
		StatPanel.SetActive (!coolDown);
		if (DifCooldown) {
			t+=Time.deltaTime;
			if(t>0.2f){
				t=0;
				DifCooldown=false;
			}
		}
	}

	public void FillLevelStats(){
		HighScore [0].text = "HighScore: " +GM.GameLevels [doorNumber].highScore;
		HighScore [1].text = "HighScore: " +GM.GameLevels [doorNumber].highScore;
		compTime [0].text = "Time: " + GM.GameLevels [doorNumber].recordCompletionTime;
		compTime [1].text = "Time: " + GM.GameLevels [doorNumber].recordCompletionTime;
		NOD [0].text = "Number of Deaths: " + GM.GameLevels [doorNumber].recordnumOfDeaths;
		NOD [1].text = "Number of Deaths: " + GM.GameLevels [doorNumber].recordnumOfDeaths;
		string dif;
		if (GM.GameLevels [doorNumber].recordCompletionDifficulty == 3) {
			dif = "Very Hard";
		} else if (GM.GameLevels [doorNumber].recordCompletionDifficulty == 2) {
			dif = "Hard";
		} else if (GM.GameLevels [doorNumber].recordCompletionDifficulty == 1) {
			dif = "Normal";
		} else {
			dif="No Record";
		}
		Difficulty [0].text = "Difficulty: " + dif;
		Difficulty [1].text = "Difficulty: " + dif;
	}



	public void DisplayDifficulty(int dif){
		if (dif == 1) {
			DiffText[0].text="Normal";
			DiffText[1].text="Normal";
		} else if (dif == 2) {
			DiffText[0].text="Hard";
			DiffText[1].text="Hard";
		} else if (dif == 3) {
			DiffText[0].text="Very Hard";
			DiffText[1].text="Very Hard";
		}
	}

	public void CheckModifier(int num){
	switch (num) {
		case 0:
			kingdomModifier=0;
			break;
		case 1:
			//sets the first level of the select level screen to the correct number
			kingdomModifier=20;
			break;
		case 2:
			kingdomModifier=50;
			break;
		case 3:
			kingdomModifier=80;
			break;
		case 4:
			kingdomModifier=110;
			break;
		case 5:
			kingdomModifier=140;
			break;
		case 6:
			kingdomModifier=170;
			break;
		default:
			break;
		}
	}
}
