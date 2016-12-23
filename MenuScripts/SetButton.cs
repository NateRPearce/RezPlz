using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SetButton : GameStateFunctions {


	public EventSystem ES;

	public void SetCurrentButton(GameObject G){
		ES.SetSelectedGameObject(G);
	}
}
