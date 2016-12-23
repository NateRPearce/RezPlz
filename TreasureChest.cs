using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {


    [FMODUnity.EventRef]
    string openSound = "event:/Chest Opening";
    FMOD.Studio.EventInstance openEV;

    [FMODUnity.EventRef]
    string chainSound = "event:/Chains Evaporating";
    FMOD.Studio.EventInstance chainEV;

    [FMODUnity.EventRef]
    string lockSound = "event:/Lock Break";
    FMOD.Studio.EventInstance lockEV;

    public Animator[] anim=new Animator[3];
    AudioSource source;
    public AudioClip hitSound;
    float hitVol = 0.5f;

    void Awake()
    {
        openEV = FMODUnity.RuntimeManager.CreateInstance(openSound);
        chainEV = FMODUnity.RuntimeManager.CreateInstance(chainSound);
        lockEV = FMODUnity.RuntimeManager.CreateInstance(lockSound);
    }


    void Start () {
		anim = GetComponentsInChildren<Animator> ();
        source = GetComponent<AudioSource>();
    }
	
    public void PlayChainSound()
    {
        chainEV.start();
    }

    public void PlayOpenSound()
    {
        openEV.start();
    }

    public void PlayLockSound()
    {
       lockEV.start();
    }

    void OnTriggerEnter2D(Collider2D other){
	if (other.tag == Tags.attackCollider) {
            source.PlayOneShot(hitSound, hitVol);
			anim[0].SetTrigger("Activated");
			anim[2].SetTrigger("Activated");
			StartCoroutine("DisableCollider");
		}
	}
	IEnumerator DisableCollider(){
		yield return new WaitForSeconds (2);
		GetComponent<Collider2D>().enabled=false;
	}
}
