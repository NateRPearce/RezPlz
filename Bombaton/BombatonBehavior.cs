using UnityEngine;
using System.Collections;

public class BombatonBehavior : MinionScript {

    [FMODUnity.EventRef]
    string explosionSound = "event:/Puffer/PUFFER_Explosion";
    public FMOD.Studio.EventInstance explosionEv;

    AudioSource source;
	void Awake(){
		anim = GetComponent<Animator> ();
		source = GetComponentInChildren<AudioSource> ();
        explosionEv = FMODUnity.RuntimeManager.CreateInstance(explosionSound);
    }
    void Start()
    {
        FindGM();
        StartCoroutine("RegiserEnemy");
    }
    void Update () {
		GroundedCheck ();
		if (!anim.GetBool ("Exploding")) {
			EdgeCheck ();
		    Walk ();
        }
    }
	void FixedUpdate(){
		DeadEndCheck ();
		if (wallFound) {
			direction*=-1;
			Flip();
		}
	}

	public void Explode(){
        explosionEv.setVolume(checkRange());
        explosionEv.start();
    }
    public float checkRange()
    {
        if (GM.skullGuide == null)
        {
            return 0;
        }
        else
        {
            float distanceToPlayer = (((GM.skullGuide.position.x - transform.position.x) + (GM.skullGuide.position.y - transform.position.y)));
            float howFar = Mathf.Abs(distanceToPlayer);
            if (howFar > 60)
            {
                howFar = 0;
            }
            else
            {
                howFar = (60 - howFar) / 60;
            }
            return howFar;
        }
    }
}
