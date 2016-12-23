using UnityEngine;
using System.Collections;

public class FloorTriggerScript : SoundFunctions {

    [FMODUnity.EventRef]
    string downSound = "event:/Pressure Button Step on";
    FMOD.Studio.EventInstance downEV;

    [FMODUnity.EventRef]
    string upSound = "event:/Pressure Button Step off";
    FMOD.Studio.EventInstance upEV;


    public Transform[] Target= new Transform[0];
	public RemoteTriggerScript[] RTS;
	public bool Activated;
	public bool stayOnOnceTriggered;
	public LayerMask whatIsAlive;
	public Animator anim;
	public float TOT;
	public bool invert;
    public bool toggle;

    void Awake()
    {
        downEV = FMODUnity.RuntimeManager.CreateInstance(downSound);
        upEV = FMODUnity.RuntimeManager.CreateInstance(upSound);
    }

    void Start () {
		anim = GetComponent<Animator> ();
        FindGM();
		RTS = new RemoteTriggerScript[Target.Length];
		for(int i = 0; i<Target.Length;i++) {
		RTS[i] = Target[i].GetComponent<RemoteTriggerScript> ();
		}
	}
	void Update(){
		Activated = Physics2D.OverlapCircle (transform.position, 1.5f, whatIsAlive);
        if (toggle&&Activated)
        {
            anim.SetBool("PlayerOn", true);
        }
        else if(!toggle)
        {
            anim.SetBool("PlayerOn", Activated);
        }
		for(int i = 0; i<Target.Length;i++) {
			if(invert){
				RTS[i].Activated = !Activated;
			}else{
                if (toggle && anim.GetBool("PlayerOn"))
                {
                    RTS[i].Activated = true;
                }
                else if (!toggle)
                {
                    RTS[i].Activated = Activated;
                }
			}
		}
	}
	void FixedUpdate(){
	if (Activated) {
			TOT += 2;
		} else {
			TOT-=2;
		}
		if (TOT > 60) {
			TOT=60;
		}
		if (TOT < 0) {
			TOT=0;
		}
		anim.SetFloat("TimeOnTrigger",TOT);
	}

    public void PlaySound()
    {
        if (Activated)
        {
            downEV.setVolume(checkRange());
            downEV.start();
        }else
        {
            upEV.setVolume(checkRange());
            upEV.start();
        }
    }
}
