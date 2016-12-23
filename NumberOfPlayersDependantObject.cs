using UnityEngine;
using System.Collections;

public class NumberOfPlayersDependantObject : GameStateFunctions {

	public bool singlePlayerObject;
	// Use this for initialization
	void Start () {
		FindGM ();
		if (singlePlayerObject) {
			if (GM.numOfPlayers == 1) {
				gameObject.SetActive (true);
			} else {
				gameObject.SetActive (false);
			}
		} else {
			if(GM.numOfPlayers==2) {
				gameObject.SetActive(true);
			}else{
				gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
