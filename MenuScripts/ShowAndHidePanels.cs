using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShowAndHidePanels : GameStateFunctions {

	MenuControllerScript MCS;
	SelectButtons SB;
	public FadeScreenScript FSS;
	// Use this for initialization
	void Start () {
		FindGM ();
		MCS = GetComponentInParent<MenuControllerScript> ();
		SB = GetComponentInParent<SelectButtons> ();
		FSS=MCS.BlackBackDrop.GetComponent<FadeScreenScript> ();
	}

	public void ShowStartPanel(){
			MCS.StartPanel.SetActive(true);
		SB.SelectNewGameButton ();
	}	

	public void ShowPlayersPanel(){
		MCS.PlayersSelPanel.SetActive(true);
	}

	public void ShowKingdomPanel(){
		MCS.KingdomSelPanel.SetActive(true);
	}
	public void ShowLVLSelPanel(){
		MCS.LVLselPanel.SetActive(true);
	}

	public void HideStartPanel(){
		MCS.StartPanel.SetActive(false);
	}
	public void HidePlayersPanel(){
		MCS.PlayersSelPanel.SetActive(false);
		FSS.fadeIn = true;
		FSS.CurrentScreen [1] = MCS.KingdomSelScreen;
		FSS.CurrentScreen [0] = MCS.KingdomSelPanel;
		FSS.LastScreen [0] = MCS.titleImage.gameObject;
		FSS.LastScreen [1] = MCS.StartScreen;
	}
	public void HideKingdomPanel(){
		MCS.KingdomSelPanel.SetActive(false);
		FSS.fadeIn = true;
		FSS.LastScreen [0] = MCS.KingdomSelScreen;
		FSS.LastScreen [1] = MCS.KingdomSelPanel;
		FSS.CurrentScreen [1] = MCS.LVLselScreen;
		FSS.CurrentScreen [0] = MCS.LVLselPanel;
	}
	public void HideLVLSelPanel(){
		MCS.LVLselPanel.SetActive(false);
	}
	public void SetNumberOfPlayers(int num){
		GM.numOfPlayers = num;
		GM.setControls ();
	}
}
