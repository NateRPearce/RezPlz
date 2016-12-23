using UnityEngine;
using System.Collections;

public class MakeManagerParent : GameStateFunctions {

	// Use this for initialization
	void OnEnable () {
	if (GameManager.instance != null) {
		transform.SetParent(GameManager.instance.transform);
		}
	}
}
