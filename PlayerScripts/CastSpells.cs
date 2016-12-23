using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CastSpells : GameStateFunctions {

	public List<int> Spell = new List<int>();
	public int lastInput;
	public Text Spell_Name;
	public SpellCastMenu SCM;
	public Animator anim;

	void Start () {
		FindGM ();
		SCM = GetComponentInParent<SpellCastMenu> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (!SCM.SCMCanvas.enabled) {
			return;
		}
		GetJoystickInput ();
	}



	void InputSpell(int input){
		if (Spell.Count == 6) {
			Spell.RemoveRange(0,5);
		}
		if (Spell.Count == 0) {
			Spell.Add (input);
		}else if(input!=lastInput){
			Spell.Add (input);
			if (Spell.Count == 5) {
				//CheckSpell();
			}
		}
		lastInput=input;
	}




	void CheckSpell(){
		//int[] attemptCast = new int[5];
		//attemptCast=Spell.ToArray ();
		/*if (attemptCast.SequenceEqual (GM.FireSpell.spell)&&GM.FireSpell.unlocked) {
			Spell_Name.text = GM.FireSpell.name;
			anim.SetTrigger(GM.FireSpell.name);
			if(SCM.Player1Casting){
				GM.PM.PC1.FireAbilityEnabled=true;
			}else if(SCM.Player2Casting){
				GM.PM.PC2.FireAbilityEnabled=true;
			}

			Spell.RemoveRange(0,5);
		} else {
			Spell.RemoveRange(0,5);
			Spell_Name.text = "Spell Failed";
			SCM.ScreenToggle();
		}*/
	}





	void GetJoystickInput(){
		/*if(Input.GetAxis (GM.spellCastX [0])>0.9f){
			InputSpell(2);
		}
		if(Input.GetAxis (GM.spellCastX [0])<-0.9f){
			InputSpell(4);
		}
		if (Input.GetAxis (GM.spellCastY [0]) >0.9f) {
			InputSpell(3);
		}
		if (Input.GetAxis (GM.spellCastY [0]) <-0.9f) {
			InputSpell(1);
		}*/
	}
	public void DisableScreen(){
		SCM.ScreenToggle ();
	}
}
