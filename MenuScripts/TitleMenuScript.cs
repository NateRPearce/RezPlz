using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleMenuScript : GameStateFunctions {


	public bool startScreenEnabled;
	float alpha=0;
	public Image graveyard;
	public GameObject BlackPanel2;
	public GameObject IntroPanel;
	public GameObject StartPanel;
	public GameObject LvlSelectPanel;
	public GameObject KNGDMPanel;

	void Awake () {
		IntroPanel.SetActive (true);
		KNGDMPanel.SetActive (false);
		LvlSelectPanel.SetActive (false);
		StartPanel.SetActive (false);
		graveyard.color=new Color(1,1,1,0);
	}
	void Start(){
		FindGM ();
	}

	void FixedUpdate () {
		if(startScreenEnabled&&alpha<1){
			graveyard.color=new Color(0.75f,0.75f,0.75f,alpha);
			alpha+=0.01f;
		}
		if(!startScreenEnabled&&alpha>0){
			graveyard.color=new Color(0.75f,0.75f,0.75f,alpha);
			alpha-=0.01f;
		}
	}

	public void GoToKingdomSelect(){
		startScreenEnabled = false;
		IntroPanel.SetActive (false);
		BlackPanel2.SetActive (false);
	}
}
