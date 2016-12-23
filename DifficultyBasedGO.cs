using UnityEngine;
using System.Collections;

public class DifficultyBasedGO : GameStateFunctions {

	public GameObject[] LVL1GOs;
	public GameObject[] LVL2GOs;
	public GameObject[] LVL3GOs;
	// Use this for initialization
	void Start () {
		FindGM ();
		if (GM.difficulty == 1) {
		
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
