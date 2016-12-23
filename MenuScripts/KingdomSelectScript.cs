using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class KingdomSelectScript : GameStateFunctions {

	ShowAndHidePanels SandH;
	public Transform KingdomController;
	// Use this for initialization
	void Start () {
		FindGM ();
		SandH = GetComponent<ShowAndHidePanels> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(GM.jumpbtn[0])||Input.GetButtonDown(GM.jumpbtn[1])){
			if(GM.Kingdoms[GM.currentKingdomNum].unLocked){
				SandH.HideKingdomPanel();
			}
		}
	}
}
