using UnityEngine;
using System.Collections;

public class FollowScript : GameStateFunctions {

    [FMODUnity.EventRef]
    public string flapSound = "event:/MetalLevel/MetalLevel_Batling_Idle_Flap_2";
    public FMOD.Studio.EventInstance flapEV;

    [FMODUnity.EventRef]
    public string puffSound = "event:/Player/Player_Glide_Open";
    public FMOD.Studio.EventInstance puffEV;

    public Transform target;
    public bool disabled;
	public bool flutter;
	public float flapSpeed;
	public float flapVarianceRange;
	public bool followCameraFocus;
	public float followSpeed;
	public bool dontLerp;
    public bool floatAway;
	float flapTime;
	float flapVarianceY;
	float flapVarianceX;
	Animator anim;

	void Start(){
        if(flapSound!=" ")
        {
            flapEV = FMODUnity.RuntimeManager.CreateInstance(flapSound);
            puffEV = FMODUnity.RuntimeManager.CreateInstance(puffSound);
        }
        if (GetComponentInChildren<Animator> () != null) {
			anim = GetComponentInChildren<Animator> ();
		}
		FindGM ();
		if (target == null) {
			target = GM.selectedPlayerPos;
		}
	}


	void Update(){
        if (disabled)
        {
            return;
        }
	if (followCameraFocus) {
			target=GM.selectedPlayerPos;
			if(target.name=="Player1"){
				anim.SetBool("FollowingPlayer1",true);
			}else{
				anim.SetBool("FollowingPlayer1",false);
			}
		}
	
	}

	void FixedUpdate () {
        if (disabled)
        {
            return;
        }
        if (floatAway && target == transform)
        {
            transform.position = new Vector3(transform.position.x + (transform.localScale.x/100), transform.position.y + Random.Range(.01f, .1f), transform.position.z);
            return;
        }
		if (flutter) {
			flapTime += Time.deltaTime;
			if(target!=null){
				transform.position = Vector3.Lerp (transform.position, new Vector3(target.position.x+flapVarianceX,target.position.y+flapVarianceY,0), Time.deltaTime * 3);
			}
				if (flapTime > flapSpeed) {
				flapTime = 0;
				if (followCameraFocus) {
					flapVarianceY = Random.Range(-flapVarianceRange,flapVarianceRange)+3;
					flapVarianceX = Random.Range(-flapVarianceRange,flapVarianceRange)-3;
				}else{
					flapVarianceY = Random.Range(-flapVarianceRange,flapVarianceRange);
					flapVarianceX = Random.Range(-flapVarianceRange,flapVarianceRange);
				}
			}
		} else {
			if(dontLerp&&target!=null){
				transform.position = target.position;
			}else if(target!=null){
			transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * followSpeed);
			}
		}
	}

    public void PlayFlapSound()
    {
        flapEV.start();
    }

    public void PlayPuffSound()
    {
        puffEV.start();
    }
}
