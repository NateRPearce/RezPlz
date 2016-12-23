using UnityEngine;
using System.Collections;

public class MainCamScript : GameStateFunctions {

	float camCooldown;
	bool camCoolingDown;


	void Start(){
		FindGM ();
		FindCBS ();
	}
	void Update () {
		if(!camCoolingDown){
			CamSwitch ();
		}
	}


	void FixedUpdate(){
	if (camCoolingDown) {
			camCooldown+=Time.deltaTime;
			if(camCooldown>0.2f){
				camCoolingDown=false;
				camCooldown=0;
			}
		}
	}


	void CamSwitch(){
		if(GM.PM.PDS1.playerDead||GM.PM.PDS2.playerDead){
			return;
		}
		if(Input.GetButtonUp(GM.Rezbtn[0])||Input.GetButtonUp(GM.Rezbtn[1])){
            if (GM.raceMode)
            {
                return;
            }
			if (GM.camNum == 1) {
				if (GM.PM.PC2.ControlsEnabled) {
					GM.camNum = 2;
				}
			}else if (GM.camNum == 2) {
				if (GM.PM.PC1.ControlsEnabled) {
					GM.camNum = 1;
				}
			}
			camCoolingDown = true;
		}
	}
}
