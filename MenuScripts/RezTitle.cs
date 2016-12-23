using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RezTitle : GameStateFunctions{
	
	public Image title;
	Animator titleAnim;
	public GameObject BackDrop;
	public GameObject NewGameButton;
	public GameObject IntroPanel;
	public GameObject StartPanel;
	public GameObject KindgomPanel;
	public GameObject LvlSelPanel;
	void Start () {
		FindGM ();
		titleAnim = title.GetComponent<Animator> ();
			EnableStartPanel();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown (GM.Rezbtn[0]) || Input.GetButtonDown (GM.Rezbtn[1])) {
			titleAnim.SetTrigger("Rez");
		}
	}
	public void EnableStartPanel(){
		StartPanel.SetActive (true);
		KindgomPanel.SetActive (false);
		LvlSelPanel.SetActive (false);
		IntroPanel.SetActive (false);
		BackDrop.SetActive (false);
	}


	public void EnableKingdomPanel(){
		KindgomPanel.SetActive (true);
		StartPanel.SetActive (false);
		LvlSelPanel.SetActive (false);
		IntroPanel.SetActive (false);
	}

	public void EnableLvlSelPanel(){
		LvlSelPanel.SetActive (true);
		KindgomPanel.SetActive (false);
		StartPanel.SetActive (false);
		IntroPanel.SetActive (false);
	}

	public void EnableIntroPanel(){
		IntroPanel.SetActive (true);
		LvlSelPanel.SetActive (false);
		KindgomPanel.SetActive (false);
		StartPanel.SetActive (false);
	}
}
