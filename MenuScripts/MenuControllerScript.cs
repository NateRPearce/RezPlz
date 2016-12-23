using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuControllerScript : MenuControllerFunctions {


	float BDalpha=1;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		IntroPanel.SetActive (true);
		StartPanel.SetActive (false);
		PlayersSelPanel.SetActive (false);
		KingdomSelPanel.SetActive (false);
		LVLselPanel.SetActive (false);
		titleAnim = titleImage.GetComponent<Animator> ();
		FindGM ();
	}

	void Update(){
	if(Input.GetButtonDown(GM.Rezbtn[0])||Input.GetButtonDown(GM.Rezbtn[1])){
			ShowStartPanel();
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (HideBD&&BDalpha>0) {
			BDalpha-=0.02f;
			BlackBackDrop.color=new Color(0,0,0,BDalpha);
		}
	}



}
