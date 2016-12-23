using UnityEngine;
using System.Collections;

public class EidolonSounds : MonoBehaviour {

    AudioSource source;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip fusionSound;
    float jumpVol=0.2f;
    float deathVol=1;
    float fusionVol=0.5f;


	void Awake () {
        source = GetComponent<AudioSource>();
	}
	
	public void PlayJump()
    {
        source.PlayOneShot(jumpSound, jumpVol);
    }

    public void PlayDeath()
    {
        source.PlayOneShot(deathSound, deathVol);
    }

    public void PlayFusion()
    {
        source.PlayOneShot(fusionSound, fusionVol);
    }
}
