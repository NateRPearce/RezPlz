using UnityEngine;
using System.Collections;

public class ScrollingKingdomScript : GameStateFunctions {

	public Transform KingdomImage;
	SpriteRenderer KingdomS;
	KingdomEnteranceController KEC;
	public Vector3 startingPos;
	public float offsetL;
	public float offsetR;
	public bool pos1;
	// Use this for initialization
	void Start () {
		FindGM ();
		startingPos = transform.localPosition;
		KEC = GetComponentInParent<KingdomEnteranceController> ();
		KingdomS = KingdomImage.GetComponent<SpriteRenderer>();
		if (pos1 && !GM.Kingdoms[1].unLocked) {
			KingdomS.color=new Color(0.2f,0.2f,0.2f,1);
		}
	}
	void Update(){
		if (KEC.direction ==-1) {
			if (transform.localPosition.x < startingPos.x - offsetL) {
				transform.localPosition = new Vector3 (transform.localPosition.x + 165, transform.localPosition.y, transform.localPosition.z);
				if(GM.currentKingdomNum<6){
					KingdomS.sprite=KEC.KingdomSprite[GM.currentKingdomNum+1];
					if(!GM.Kingdoms[GM.currentKingdomNum+1].unLocked){
						KingdomS.color=new Color(0.2f,0.2f,0.2f,1);
					}else{
						KingdomS.color=new Color(1,1,1,1);
					}
				}
				KEC.direction = 0;
				KEC.SetKingdomName(GM.currentKingdomNum);
				setPanelText();
				return;
			}
		} else if (KEC.direction ==1) {
			if (transform.localPosition.x > startingPos.x + offsetR) {
				transform.localPosition = new Vector3 (transform.localPosition.x - 165, transform.localPosition.y, transform.localPosition.z);
				if(GM.currentKingdomNum>0){
					KingdomS.sprite=KEC.KingdomSprite[GM.currentKingdomNum-1];
					if(!GM.Kingdoms[GM.currentKingdomNum-1].unLocked){
						KingdomS.color=new Color(0.2f,0.2f,0.2f,1);
					}else{
						KingdomS.color=new Color(1,1,1,1);
					}
				}
				KEC.direction = 0;
				KEC.SetKingdomName(GM.currentKingdomNum);
				setPanelText();
				return;
			}
		}
		if (KEC.direction == 0) {
			KEC.coolDown=false;
		}
	}
	void FixedUpdate () {
		scroll (KEC.direction);
	}
	
	void scroll(float dir){
		transform.position = new Vector3 (transform.position.x+dir, transform.position.y, transform.position.z);
	}
	void setPanelText(){
		if(!GM.Kingdoms[GM.currentKingdomNum].unLocked){
			KEC.pressB[0].color=new Color(0,0,0,0);
			KEC.pressB[1].color=new Color(0,0,0,0);
			KEC.bImage[0].color=new Color(0,0,0,0);
			KEC.bImage[1].color=new Color(0,0,0,0);
		}else{
			KEC.pressB[0].color=new Color(1,1,1,1);
			KEC.pressB[1].color=new Color(0,0,0,0.8f);
			KEC.bImage[0].color=new Color(1,1,1,1);
			KEC.bImage[1].color=new Color(0,0,0,0.8f);
		}
		KEC.lvlsDone = 0;
		KEC.CompletedLvlCheck();
		KEC.lvlsCompleted[0].text="Levels Completed: " + KEC.lvlsDone + " of " + KEC.kingdomLvls;
		KEC.lvlsCompleted[1].text="Levels Completed: " + KEC.lvlsDone + " of " + KEC.kingdomLvls;
		KEC.TotalPoints[0].text="Total Points: "+KEC.AllPoints;
		KEC.TotalPoints[1].text="Total Points: "+KEC.AllPoints;
		KEC.TotalPoints[0].text="Total Points: " + GM.TotalKingdomPoints[GM.currentKingdomNum];
		KEC.TotalPoints[1].text="Total Points: " + GM.TotalKingdomPoints[GM.currentKingdomNum];
	}
}
