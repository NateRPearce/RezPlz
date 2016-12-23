using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelTimeScript : GameStateFunctions {


	Text levelTimer;
	float lvlTime;

	void Start(){
		FindGM ();
		levelTimer = GetComponent<Text> ();
	}

	void FixedUpdate(){
		lvlTime += Time.deltaTime;
		levelTimer.text = ""+Mathf.Round(lvlTime*100)/100;
	}
}
