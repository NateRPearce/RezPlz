using UnityEngine;
using System.Collections;

public class CamCheck : PlayerInheritance {

    public float raceCamSize;
	// Use this for initialization
	void Start () {
		FindGM ();
        FindCBS();
		GM.camNum = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (GM.raceMode)
        {
            RaceCheck();
            return;
        }
		if(GM.numOfPlayers==1){
			CameraCheck ();
        }else
        {
            if (GM.camNum == 2 && GM.PM.PDS2.playerDead)
            {
                GM.camNum = 1;
            }
            if (GM.camNum == 1 && GM.PM.PDS1.playerDead)
            {
                GM.camNum = 2;
            }
        }
	}

    public void RaceCheck()
    {
        if (CBS.camSize != raceCamSize)
        {
            CBS.camSize = raceCamSize;
        }
        if (GM.PM.PDS1.playerDead)
        {
            GM.camNum = 2;
            return;
        }
        if (GM.PM.PDS2.playerDead)
        {
            GM.camNum = 1;
            return;
        }
        if (GM.PM.PC1.transform.position.x> GM.PM.PC2.transform.position.x)
        {
            GM.camNum = 1;
        }else
        {
            GM.camNum = 2;
        }
    }

	public void CameraCheck(){
		if(GM.camNum==4&&(!GM.PM.PDS1.playerDead&&!GM.PM.PDS2.playerDead)){
			if (GM.PM.PC1.ControlsEnabled) {	
				GM.camNum = 1;
			} else {
				GM.camNum = 2;
			}
		}
		if(GM.PM.PDS1.playerDead&&GM.PM.PDS2.playerDead&&GM.camNum==4){
            GM.camNum = 1;
			return;
		}

        if ((GM.PM.PDS1.playerDead && GM.PM.PDS1.TOD < 1) || (GM.PM.PDS2.playerDead && GM.PM.PDS2.TOD < 1))
        {
            if (GM.PM.PC2.Ooping) {
                GM.PM.PC2.stopAlleyOoping();
            }
            if (GM.PM.PC1.Ooping) {
                GM.PM.PC1.stopAlleyOoping();
            }
                GM.camNum = 4;
            return;
        }
        if (GM.PM.PC1.onFire || GM.PM.PC2.onFire)
        {
            return;
        }

            if (GM.PM.PDS2.TOD > 1)
            {
                GM.camNum = 1;
            }
			if (GM.PM.PDS1.TOD > 1)
            {
                GM.camNum = 2;
            }
	}
}
