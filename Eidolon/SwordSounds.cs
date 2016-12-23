using UnityEngine;
using System.Collections;

public class SwordSounds : MonoBehaviour {

    AudioSource source;
    public AudioClip flingSound;
    public AudioClip breakSound;
    public AudioClip hitSound;
    float flingVol=0.1f;
    float breakVol=0.5f;
    float hitVol=1;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayFling()
    {
        source.PlayOneShot(flingSound,flingVol);
    }

    public void PlayBreak()
    {
        source.PlayOneShot(breakSound, breakVol);
    }

    public void PlayHit()
    {
        source.PlayOneShot(hitSound,hitVol);
    }

}
