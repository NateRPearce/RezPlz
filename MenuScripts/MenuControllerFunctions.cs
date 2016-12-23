using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuControllerFunctions : GameStateFunctions {


	public GameObject IntroPanel;
	public GameObject StartPanel;
	public GameObject StartScreen;
	public GameObject PlayersSelPanel;
	public GameObject KingdomSelPanel;
	public GameObject KingdomSelScreen;
	public GameObject LVLselPanel;
	public GameObject LVLselScreen;
	public Image BlackBackDrop;
	public bool HideBD;
	public Transform titleImage;
	public Animator titleAnim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ShowStartPanel(){
		titleAnim.SetTrigger("Rez");
		if(!HideBD){
			HideBD=true;
		}
		IntroPanel.SetActive(false);
	}
}
