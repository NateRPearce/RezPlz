using UnityEngine;
using System.Collections;

public class RisingChainScript : SoundFunctions {

    [FMODUnity.EventRef]
    string chainSound = "event:/Environment/General_Chain_Platform";
    public FMOD.Studio.EventInstance chainSoundEv;

    public Transform[] gears=new Transform[2];
	public Animator[] gearAnims;
	public Transform[] targets = new Transform[2];
    Vector3 midPOS;

    public float speed;
	public Transform squishCollider;
	bool activated;
    bool halfActivated;
	public bool Invert;
	bool hold;
	RemoteTriggerScript RTS;
	public float squishTimer;
	float t;
	bool soundReady;

	void Start () {
        midPOS = new Vector3(transform.position.x, (targets[0].position.y + targets[1].position.y) / 1.8f, transform.position.z);
        RTS = GetComponent<RemoteTriggerScript> ();
		gearAnims = new Animator[gears.Length];
		for (int i=0; i<gears.Length; i++) {
			gearAnims[i]=gears[i].GetComponent<Animator>();
		}
        soundStartFunctions();
        chainSoundEv = FMODUnity.RuntimeManager.CreateInstance(chainSound);
        chainSoundEv.setVolume(0);
        chainSoundEv.start();
    }
    void Update(){
		hold = RTS.hold;
	if (Invert) {
			activated = !RTS.Activated;
		} else {
			activated=RTS.Activated;
            halfActivated = RTS.halfActivated;
		}
	}

 void FixedUpdate(){
		t += Time.deltaTime;
		if (t > 4)
        {
			soundReady=true;
		}
		if (gearAnims [0].GetBool ("Idle"))
        {
                chainSoundEv.setVolume(0);
        }
        else
        {
            if (soundReady)
            {
                chainSoundEv.setVolume(checkRange());
            }		
		}
		if (transform.position.y < targets [1].position.y + 0.2f && transform.position.y > targets [1].position.y - 0.2f)
        {
			if(squishTimer<0.2f)
            {
				squishCollider.GetComponent<Collider2D>().enabled = true;
			}
            else
            {
				squishCollider.GetComponent<Collider2D>().enabled = false;
			}
			squishTimer+=Time.deltaTime;
		}
        else
        {
			squishTimer=0;
			squishCollider.GetComponent<Collider2D>().enabled = false;
		}
		if ((transform.position.y < targets [0].position.y + 0.1f && transform.position.y > targets [0].position.y - 0.1f) || (transform.position.y < targets [1].position.y + 0.1f && transform.position.y > targets [1].position.y - 0.1f) || (transform.position.y < midPOS.y + 0.1f && transform.position.y > midPOS.y - 0.1f))
        {
			gearAnims [0].SetBool ("Idle", true);
			gearAnims [1].SetBool ("Idle", true);
		}
        else
        {
			gearAnims [0].SetBool ("Idle", false);
			gearAnims [1].SetBool ("Idle", false);
		}
			if (hold)
        {
				gearAnims [0].SetBool ("Idle", true);
				gearAnims [1].SetBool ("Idle", true);
				transform.position = Vector3.MoveTowards (transform.position, transform.position, speed);
			}
        else if (activated)
        {
				gearAnims [0].SetBool ("GearUp", true);
				gearAnims [0].SetBool ("GearDown", false);
				gearAnims [1].SetBool ("GearUp", true);
				gearAnims [1].SetBool ("GearDown", false);
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, targets [0].position.y, transform.position.z), speed);
		}
        else if (halfActivated)
        {
            if (midPOS.y < transform.position.x)
            {
                gearAnims[0].SetBool("GearDown", true);
                gearAnims[0].SetBool("GearUp", false);
                gearAnims[1].SetBool("GearDown", true);
                gearAnims[1].SetBool("GearUp", false);
            }
            else if (midPOS.y > transform.position.x)
            {
                gearAnims[0].SetBool("GearUp", true);
                gearAnims[0].SetBool("GearDown", false);
                gearAnims[1].SetBool("GearUp", true);
                gearAnims[1].SetBool("GearDown", false);
            }
            else
            {
                gearAnims[0].SetBool("Idle", true);
                gearAnims[1].SetBool("Idle", true);
            }
                transform.position = Vector3.MoveTowards(transform.position, midPOS, speed);
        }
        else
        {
				gearAnims [0].SetBool ("GearDown", true);
				gearAnims [0].SetBool ("GearUp", false);
				gearAnims [1].SetBool ("GearDown", true);
				gearAnims [1].SetBool ("GearUp", false);
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, targets [1].position.y, transform.position.z), speed);
		}
	}
}
