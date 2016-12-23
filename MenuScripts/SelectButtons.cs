using UnityEngine;
using System.Collections;

public class SelectButtons : SetButton {

	public GameObject NewGameButton;
	public GameObject OnePlayerButton;

	public void SelectOnePlayerBTN(){
		SetCurrentButton (OnePlayerButton);
	}
	public void SelectNewGameButton(){
		SetCurrentButton (NewGameButton);
	}

}
