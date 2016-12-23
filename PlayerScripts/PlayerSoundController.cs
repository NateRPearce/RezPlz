using UnityEngine;
using System.Collections;

public class PlayerSoundController : MonoBehaviour {

	public AudioClip squishSound;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}


	public void playSquish(){
		source.PlayOneShot (squishSound, 1);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
