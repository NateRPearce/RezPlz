using UnityEngine;
using System.Collections;

public class ZephHUDBehavior : GameStateFunctions {

	public Animator anim;
	HUD_Dialog HD;

	void Start () {
		anim = GetComponent<Animator> ();
		FindGM ();
		GM.ZHB = this;
		HD=GetComponent<HUD_Dialog>();
	}
	
	public void ShowAbility(int abl){
		if (GM.PM.PC1.AB.CrystalList [abl].enabled && GM.PM.PC1.AB.CrystalList [abl].unlocked) {
			if (abl == 1) {
				//anim.SetBool ("Stoned", true);
				//anim.SetBool ("Horny", false);
			} else if (abl == 3) {
				//anim.SetBool ("Horny", true);
				//anim.SetBool ("Stoned", false);
			} else {
				//anim.SetBool ("Horny", false);
				//anim.SetBool ("Stoned", false);
			}
		} else {
			//anim.SetBool ("Horny", false);
			//anim.SetBool ("Stoned", false);
		}
	}
	
	public void KillHUD(){	
		anim.SetBool("Dead",true);
	}
	
	public void ReviveHUD(){
		anim.SetBool("Dead",false);
	}
}