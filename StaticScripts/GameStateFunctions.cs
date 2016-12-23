using UnityEngine;
using System.Collections;

public class GameStateFunctions : MonoBehaviour {

	public MusicController MC;
	public GameManager GM;
	public CameraBehaviorScript CBS;
	public MaxBloodController MBC;

	public void FindGM(){
		if (GameManager.instance.GetComponent<GameManager> ()) {
			GM = GameManager.instance.GetComponent<GameManager> ();
		}
	}
	public void FindCBS(){
		if (Camera.main.GetComponent<CameraBehaviorScript> ()) {
			CBS = Camera.main.GetComponent<CameraBehaviorScript> ();
		}
	}
	public void FindMBC(){
		if (GetComponent<MaxBloodController> ()) {
			MBC = GetComponent<MaxBloodController> ();
		}
	}
	public void FindMC(){
		if (MusicController.musicInstance.GetComponent<MusicController> ()) {
			MC = MusicController.musicInstance.GetComponent<MusicController> ();
		}
	}
}
