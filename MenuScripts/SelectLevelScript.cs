using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SelectLevelScript : GameStateFunctions {

	public Transform LevelEnterControl;
	public AnimatedTextureExtendedUV[] AnimTexture= new AnimatedTextureExtendedUV[3];
	LevelEnteranceControllerScript LECS;


	void Start () {
		FindGM ();
		LECS = LevelEnterControl.GetComponent<LevelEnteranceControllerScript> ();
	}
	

	void Update () {
		if(Input.GetButtonDown(GM.jumpbtn[0])||Input.GetButtonDown(GM.jumpbtn[1])){
			GM.difficulty=1;
			//GM.difficulty=LECS.difficulty;
			SceneManager.LoadScene(LECS.doorNumber+1);
		}
	}

}
