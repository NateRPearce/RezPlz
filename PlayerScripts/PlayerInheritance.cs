using UnityEngine;
using System.Collections;

public class PlayerInheritance : GameStateFunctions {

	public PlayersManager PM;
	public PlayerControls PC;
	public PlayerDeathScript PDS;
	public LightDetectionScript LDS;


	public void FindLDS(){
		LDS = GetComponentInChildren<LightDetectionScript> ();
	}

	public void FindPM(){
		PM = GetComponentInParent<PlayersManager> ();
	}

	public void FindPC(){
		PC = GetComponent<PlayerControls> ();
	}

	public void FindPDS(){
		PDS = GetComponent<PlayerDeathScript> ();
	}
}
