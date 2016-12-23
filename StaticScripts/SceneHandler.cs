using UnityEngine;
using System.Collections;

public class SceneHandler : GameStateFunctions {

	public float resetTimer;
	public string scene;
	// Use this for initialization
	void Start () {
		FindGM ();
		GM.CurrentScene = scene;
	}
}
