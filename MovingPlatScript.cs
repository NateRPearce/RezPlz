using UnityEngine;
using System.Collections;

public class MovingPlatScript : SoundFunctions {

    [FMODUnity.EventRef]
    string smashSound = "event:/ULMOK/LavaLevel_Ulmok_Flop";
    FMOD.Studio.EventInstance smashEV;


    public bool goingToTarget;
	public Transform target1;
	public Transform target2;
    public Transform secondaryTarget;
    public Transform puffOfSmoke;
	public float speedToTarget;
	public float returnSpeed;
	public bool oneWayTrip;
	public bool Activated;
	public bool toggle;
	public RemoteTriggerScript RTS;
	public bool DontLerpToTarget;
	public bool DontLerpBack;
	public float startDelay;
	public Collider2D squishCol;
	public bool flipEnabled;
	public bool facingRight;
	float currentTime;
    public float squishRangePos;
    public float squishRangeNeg;
    public bool playSmashSound;


    void Start(){
        soundStartFunctions();
	if (GetComponent<RemoteTriggerScript> () != null) {
			RTS=GetComponent<RemoteTriggerScript>();
		}
        if (playSmashSound)
        {
            smashEV = FMODUnity.RuntimeManager.CreateInstance(smashSound);
        }
	}

	void Update(){
	if (RTS != null) {
			Activated=RTS.Activated;
		}
		if (squishCol != null) {
			squishCheck();
		}
	}
	void FixedUpdate () {
		switchCheck ();
		if (startDelay > 0) {
			currentTime+=Time.deltaTime;
			if(currentTime<startDelay){
				return;
			}
		}
		if (Activated||toggle) {
			GetComponent<Rigidbody2D>().isKinematic=true;
			if (goingToTarget||(toggle&&Activated)) {
				if(DontLerpToTarget){
					transform.position = Vector3.MoveTowards(transform.position, target2.position, speedToTarget);
				}else{
					transform.position = Vector3.Lerp (transform.position, target2.position, Time.deltaTime * speedToTarget);
				}
				} else if(!goingToTarget||(toggle&&!Activated)){
				if(DontLerpBack){
					transform.position = Vector3.MoveTowards(transform.position, target1.position, returnSpeed);
				}else{
					transform.position = Vector3.Lerp (transform.position, target1.position, Time.deltaTime * returnSpeed);
				}
				}
		}
	}

	void switchCheck(){
		if(transform.position==target1.position||transform.position==target2.position){
			if(!oneWayTrip){
                if (goingToTarget&&playSmashSound)
                {
                    PlaySmashSound();
                    if(puffOfSmoke!=null)
                    puffOfSmoke.GetComponent<Animator>().SetTrigger("Puff");
                }
				goingToTarget=!goingToTarget;
				if(flipEnabled){
					Flip();
				}
			}
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void squishCheck(){
        if(transform.position.y<target2.position.y+squishRangePos&& transform.position.y > target2.position.y + squishRangeNeg&&goingToTarget)
        {
            squishCol.enabled = true;
        }else
        {
            squishCol.enabled = false;
        }
	}

    public void PlaySmashSound()
    {
        smashEV.setVolume(checkRange());
        smashEV.start();
    }
}
