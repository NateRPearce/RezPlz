using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : GameStateFunctions {

	public Image fader;
	public bool restartLvl;
	public bool fadeOut;
	void Start()
	{
		FindGM ();
		fader=GetComponent<Image> ();
		GM.r = this;
		fader.color = Color.clear;
	}

	void FixedUpdate(){
        if (fadeOut)
        {
            fadeToBlack();
        }
        if (!restartLvl) {
			return;
		}
		if (fader.color == Color.clear) {
			StartCoroutine ("RestartLevel");
		}
	}

	public void fadeToBlack(){
        if (fader.color != Color.black)
        {
            fader.color = Color.Lerp(fader.color, Color.black, Time.deltaTime * 2);
        }
	}

	IEnumerator RestartLevel(){
		yield return new WaitForSeconds (2);
		fadeOut = true;
		yield return new WaitForSeconds (3);
		if ((GM.MB [0].mana <=0 && GM.MB [1].mana <= 0)||GM.EidolonLifeBar.mana==0) {
			SceneManager.LoadScene(GM.currentLevel+2);
		}
	}

}
