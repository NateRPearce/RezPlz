using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class KingdomEnteranceController : GameStateFunctions {

	public int direction;
	public string CurrentKingdomName;
	public Text[] KingdomName = new Text[2];
	public GameObject StatPanel;
	public Text[] lvlsCompleted = new Text[2];
	public Text[] TotalPoints = new Text[2];
	public bool coolDown;
	public Sprite[] KingdomSprite = new Sprite[7];
	public Text[] pressB = new Text[2];
	public Image[] bImage = new Image[2];
	public int lvlsDone;
	public int kingdomLvls;
	public int AllPoints;
	void Start(){
		FindGM ();
		GM.currentKingdomNum = 0;
		lvlsDone = 0;
		SetKingdomName (GM.currentKingdomNum);
		CompletedLvlCheck ();
		TotalPoints[0].text="Total Points: " + GM.TotalKingdomPoints[GM.currentKingdomNum];
		TotalPoints[1].text="Total Points: " + GM.TotalKingdomPoints[GM.currentKingdomNum];
		lvlsCompleted [0].text = "Levels Completed: " + lvlsDone + " of "+kingdomLvls;
		lvlsCompleted [1].text = "Levels Completed: " + lvlsDone + " of "+kingdomLvls;
	}
	void Update(){
		if (coolDown) {
			return;
		}
		if (!coolDown&&(Input.GetAxis (GM.movebtn [0]) > 0.5 || Input.GetAxis (GM.movebtn [1]) > 0.5)) {
			if (GM.currentKingdomNum < 6) {
				direction = -1;
			}
		} else if (!coolDown&&(Input.GetAxis (GM.movebtn [0]) < -0.5 || Input.GetAxis (GM.movebtn [1]) < -0.5)) {
			if (GM.currentKingdomNum > 0) {
				direction = 1;				
			}
		}
		if(direction!=0){
			GM.currentKingdomNum+=direction*-1;
			coolDown=true;
		}

	}
	void FixedUpdate(){
		StatPanel.SetActive (!coolDown);
	}

	public void SetKingdomName(int num){
		switch (GM.currentKingdomNum) {
		case 0:
			CurrentKingdomName="The Caves";
			kingdomLvls=45;
			break;
		case 1:
			if(GM.Kingdoms[1].unLocked){
				CurrentKingdomName="Fire Kingdom";
			}else{
				CurrentKingdomName="Locked";
			}
			kingdomLvls=30;
			break;
		case 2:
			if(GM.Kingdoms[2].unLocked){
				CurrentKingdomName="Ice Kingdom";
			}else{
				CurrentKingdomName="Locked";
			}
			kingdomLvls=30;
			break;
		case 3:
			if(GM.Kingdoms[3].unLocked){
				CurrentKingdomName="Metal Kingdom";
			}else{
				CurrentKingdomName="Locked";
			}
			kingdomLvls=30;
			break;
		case 4:
			if(GM.Kingdoms[4].unLocked){
				CurrentKingdomName="Undead Kingdom";
			}else{
				CurrentKingdomName="Locked";
			}
			kingdomLvls=30;
			break;
		case 5:
			if(GM.Kingdoms[5].unLocked){
				CurrentKingdomName="Blood Kingdom";
			}else{
				CurrentKingdomName="Locked";
			}
			kingdomLvls=30;
			break;
		case 6:
			if(GM.Kingdoms[6].unLocked){
				CurrentKingdomName="Time Kingdom";
			}else{
				CurrentKingdomName="Locked";
			}
			kingdomLvls=30;
			break;
		default:
			break;
		}
		KingdomName[0].text=CurrentKingdomName;
		KingdomName[1].text=CurrentKingdomName;
	}
	public void CompletedLvlCheck(){
		Debug.Log (GM.Kingdoms [GM.currentKingdomNum].firstLevel);
		Debug.Log (GM.Kingdoms [GM.currentKingdomNum].lastLevel);
		for (int i=GM.Kingdoms[GM.currentKingdomNum].firstLevel; i<GM.Kingdoms[GM.currentKingdomNum].lastLevel; i++) {
		if(GM.GameLevels[i].highScore>0){
				lvlsDone+=1;
			}
		}
	}
}
