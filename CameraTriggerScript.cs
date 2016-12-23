using UnityEngine;
using System.Collections;

public class CameraTriggerScript : GameStateFunctions {

	public Transform Target;
	public float transitionSpeed;
	public bool triggered;
	public bool maxPositionOverride;
	public bool moveHorizontal;
	public bool moveVertical;
    public bool fadeOut;
	public AudioClip CompletionSound;
	private AudioSource	source;

	void Start(){
	FindGM ();
	FindCBS ();
        if (!GM.raceMode)
        {
            CBS.maxPosOverride = maxPositionOverride;
        }
	source = GetComponent<AudioSource> ();
	}

	void FixedUpdate(){
	if (triggered) {
            if (moveHorizontal)
            {
                CBS.EventLocation = new Vector3(Target.position.x, Camera.main.transform.position.y, Target.position.z);
            }
            else if (moveVertical)
            {
                CBS.EventLocation = new Vector3(Camera.main.transform.position.x, Target.position.y, Target.position.z);
            }
            else if (fadeOut)
            {
                CBS.EventLocation = CBS.transform.position;
            }else {
				CBS.EventLocation = Target.position;
			}
			CBS.CamSpeed=transitionSpeed;
			CBS.cameraEvent = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player1"|| other.name == "Player2" && !triggered) {
            //source.PlayOneShot(CompletionSound,1);
            if (other.name == "Player1")
            {
                Debug.Log("Player 1 Finish Line Bonus + 100 points");
                GM.p1Win = 125;
                GM.p2Win = 0;
                GM.portalPlayer = 0;
            }
            if (other.name == "Player2")
            {
                Debug.Log("Player 2 Finish Line Bonus + 100 points");
                GM.p2Win = 125;
                GM.p1Win = 0;
                GM.portalPlayer = 1;
            }
            GM.PM.raceOver = true;
            GM.LST.HighScoreCheck();
			triggered=true;
		}
	}

}
