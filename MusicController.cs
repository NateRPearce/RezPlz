using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public static MusicController musicInstance;
	public AudioSource AS;
	public AudioClip[] Songs=new AudioClip[1];
	// Use this for initialization
	void Awake () {
		if (musicInstance == null)
			
			//if not, set instance to this
			musicInstance = this;
		
		//If instance already exists and it's not this:
		else if (musicInstance != this)
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject); 

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		AS=GetComponent<AudioSource>();
	}
	public void SwitchSongCheck(int lvl){
	}
}
