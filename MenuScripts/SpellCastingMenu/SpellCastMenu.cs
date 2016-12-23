using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpellCastMenu : GameStateFunctions {

	public Canvas SCMCanvas;
	public bool Player1Casting;
	public bool Player2Casting;
	public Text CastingPlayer;
	CastSpells CS;
	void Start(){
		FindGM ();
		SCMCanvas = GetComponent<Canvas> ();
		CS = GetComponentInChildren<CastSpells> ();
		SCMCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (SCMCanvas.enabled) {
			//Time.timeScale = 0.1f;
		} else {
			Player1Casting=false;
			Player2Casting=false;
			//Time.timeScale = 1;
		}
		if (Input.GetButtonDown (GM.Rezbtn[0])){
			CS.anim.SetTrigger("StartSequence");
			CS.Spell_Name.text = "Spell Name";
			if(GM.PM.PC1.ControlsEnabled){
				Player1Casting=true;
				CastingPlayer.text="Player 1 Casting";
			}else if(GM.PM.PC2.ControlsEnabled){
				Player2Casting=true;
				CastingPlayer.text="Player 2 Casting";
			}
			ScreenToggle();
		}else if(Input.GetButtonDown (GM.Rezbtn[1])) {
			CS.anim.SetTrigger("StartSequence");
			CS.Spell_Name.text = "Spell Name";
			ScreenToggle();
			Player2Casting=true;
			CastingPlayer.text="Player 2 Casting";
		}
	}


	public void ScreenToggle(){
		SCMCanvas.enabled=!SCMCanvas.enabled;	
	}
}
