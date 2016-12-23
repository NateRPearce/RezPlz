using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LoadLevelScript : GameStateFunctions {

	void Start(){
		FindGM ();
		FindCBS ();
		GM.currentLevel = SceneManager.GetActiveScene().buildIndex - 2;
            GM.nextLvl = SceneManager.GetActiveScene().buildIndex + 1;
        if (GM.demoMode)
        { 
            if (GM.nextLvl == 4 || GM.nextLvl == 6)
            {
                GM.nextLvl += 1;
            }
        }
}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "CameraCollider") {
			//CBS.cameraEvent=false;
			GM.SaveCrystalsInfo();
			if(GM.GameLevels[GM.currentLevel].locked){
				SceneManager.LoadScene(1);
			}else{
				GM.loadNextLevel();
			}
			//GM.SaveGame();
			KingdomCheck();
		}
	}

	public void KingdomCheck(){
		if (GM.currentLevel>20&&GM.currentLevel<41) {
			GM.currentKingdomNum=1;
		}else if (GM.currentLevel>40&&GM.currentLevel<61) {
			GM.currentKingdomNum=2;
		}else if (GM.currentLevel>60&&GM.currentLevel<81) {
			GM.currentKingdomNum=3;
		}else if (GM.currentLevel>80&&GM.currentLevel<101) {
			GM.currentKingdomNum=4;
		}else {
			GM.currentKingdomNum=0;
		}
	}
}
