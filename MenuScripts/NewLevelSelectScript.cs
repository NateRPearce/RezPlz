using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NewLevelSelectScript : SetButton {


    List<GameObject> listButtons = new List<GameObject>(); 
	public Button[] btn=new Button[0];
	Text[] txt;
	bool scrolling;
	float holdingScroll;
	int levelNumberModifier;
	int selectedLevel;
	public int kingdomNumber = 0;
	public Text highScoreTXT;
	public Text compTime;
	public Text NOD;
	public Text[] Header=new Text[2];
	public Text[] lockedText = new Text[2];
	public string[] KingdomName=new string[6];
	public int currentKingdom;
	Color originalColor;
	public Color disabledColor;
	// Use this for initialization
	void Start () {
        //FindMC ();
        lockedText[1].color=new Color(0,0,0,0);
		lockedText[0].color=new Color(0,0,0,0);
		setKingdomNames ();
		FindGM ();
		txt = new Text[btn.Length];
		SetCurrentButton (btn[0].gameObject);
		for (int i=0; i<btn.Length; i++) {
			txt[i]=btn[i].GetComponentInChildren<Text>();
			listButtons.Add(btn[i].gameObject);
			btn[i].interactable=!GM.GameLevels[i].locked;
            if (i > 0)
            {
                txt[i].text = "LvL " + (i + 1);
            }
		}
		selectedLevel = listButtons.IndexOf (ES.currentSelectedGameObject);
		Header [0].text = KingdomName [currentKingdom];
		Header [1].text= KingdomName [currentKingdom];
		highScoreTXT.text = "HighScore: " + GM.GameLevels [selectedLevel + levelNumberModifier + 1].highScore;
		compTime.text = "Time: " + GM.GameLevels [selectedLevel + levelNumberModifier + 1].recordCompletionTime;
		NOD.text = "Number of Deaths: " + GM.GameLevels [selectedLevel + levelNumberModifier + 1].recordnumOfDeaths;
		originalColor = Header [1].color;
		UnlockNextKingdomCheck (kingdomNumber);
		for (int i =1; i<GM.GameLevels.Length; i++) {
			GM.GameLevels [i].locked = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
			if (ES.currentSelectedGameObject == null) {
				SetCurrentButton (btn[0].gameObject);
				return;
			}
		// " Kingdom points:" + GM.TotalKingdomPoints [currentKingdom]
		highScoreTXT.text = "HighScore: " + GM.GameLevels [selectedLevel + 1].highScore;
		compTime.text = "Time: " + GM.GameLevels [selectedLevel + 1].recordCompletionTime;
		NOD.text = "Number of Deaths: " + GM.GameLevels [selectedLevel + 1].recordnumOfDeaths;
		selectedLevel = listButtons.IndexOf (ES.currentSelectedGameObject)+levelNumberModifier;
		if ((Input.GetAxis (GM.spellCastX[0]) <0.1&&Input.GetAxis (GM.spellCastX[0]) > -0.1)) {
			scrolling=false;
			holdingScroll=0;
		}
		if (!scrolling) {
			if (Input.GetAxis (GM.spellCastX [0]) > 0.15) {
				if (levelNumberModifier < 79) {
					levelNumberModifier += 20;
					currentKingdom+=1;
					Header [0].text = KingdomName [currentKingdom];
					Header [1].text= KingdomName [currentKingdom];
					for (int i=0; i<btn.Length; i++) {
						txt [i].text = "LvL " + (i + levelNumberModifier + 1);
						btn[i].interactable=!GM.GameLevels[i + levelNumberModifier].locked;
					}
					if(btn[0].interactable){
						Header[1].color=originalColor;
						lockedText[1].color=new Color(0,0,0,0);
						lockedText[0].color=new Color(0,0,0,0);
					}else{
						lockedText[1].color=originalColor;
						lockedText[0].color=new Color(0,0,0,1);
						Header[1].color=disabledColor;
					}
				}
				scrolling = true;
			} else if (Input.GetAxis (GM.spellCastX [0]) < -0.15) {
				if (levelNumberModifier > 0) {
					levelNumberModifier -= 20;
					currentKingdom-=1;
					Header [0].text = KingdomName [currentKingdom];
					Header [1].text= KingdomName [currentKingdom];
					for (int i=0; i<btn.Length; i++) {
						txt [i].text = "LvL " + (i + levelNumberModifier + 1);
						btn[i].interactable=!GM.GameLevels[i + levelNumberModifier].locked;
					}
					if(btn[0].interactable){
						Header[1].color=originalColor;
						lockedText[1].color=new Color(0,0,0,0);
						lockedText[0].color=new Color(0,0,0,0);
					}else{
						lockedText[1].color=originalColor;
						lockedText[0].color=new Color(0,0,0,1);
						Header[1].color=disabledColor;
					}
				}
				scrolling = true;
			}
		}
	}
	void FixedUpdate(){
	if (scrolling) {
			holdingScroll+=Time.deltaTime;
			if(holdingScroll>0.5f){
				scrolling=false;
				holdingScroll=0;
			}
		}
	}

    public void UnlockNextKingdomCheck(int kingNum){
		//Crashes the game
		    bool kingdomComplete = true;
			int first=(kingNum*20)+1;
			int last=(kingNum*20)+21;
			int nextFirst=first+19;
			int nextLast=last+19;
		//check if all levels in kingdom have highScores
			for (int lvlNum=first; lvlNum<last; lvlNum++) {
				if(GM.GameLevels[lvlNum].highScore<50){
					kingdomComplete=false;
					break;
				}
			}
		//unlock the next set of levels if previous kingdom is completed
			if (kingdomComplete) {
				for (int k =nextFirst; k<nextLast; k++) {
					GM.GameLevels [k].locked = false;
				}
			kingdomNumber+=1;
			UnlockNextKingdomCheck(kingdomNumber);
			}
	}

	public void ReturnToMenu(){
		SceneManager.LoadScene(0);
	}


	public void goToLevel(int lvlbtn){
		int lvlNum = lvlbtn + levelNumberModifier + 2;
        SceneManager.LoadScene (lvlNum);
	}
	void setKingdomNames(){
		KingdomName [0] = "The Caves";
		KingdomName [1] = "Kingdom 2";
		KingdomName [2] = "Kingdom 3";
		KingdomName [3] = "Kingdom 4";
		KingdomName [4] = "Kingdom 5";
	}
}
