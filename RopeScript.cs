using UnityEngine;
using System.Collections;

public class RopeScript : GameStateFunctions {

    [FMODUnity.EventRef]
    public string swingSound = "event:/Environment/General_Chain_Swing";
    FMOD.Studio.EventInstance swingEV;

    public Transform[] RopeSegments = new Transform[3];
	public int playersOnRope;
	public Transform[] Player;
	public int[] currentSegment;
	public PlayerControls[] PC;
	public float hangTime;
	bool[] climbCoolingDown;
	float[] climbTime;
	float swingForceMod = 200;
    float swingVol;
	public bool Kinematic;
	// Use this for initialization
	void Start(){
		FindGM();
		Player=new Transform[2];
		currentSegment = new int[2];
		PC=new PlayerControls[2];
		climbCoolingDown=new bool[2];
		climbTime= new float[2];
        if (swingSound != "")
        {
            swingEV = FMODUnity.RuntimeManager.CreateInstance(swingSound);
            swingEV.setVolume(0);
            swingEV.start();
        }
    }
	void Update(){
	if (playersOnRope > 0) {
			hangTime+=0.1f;
			RopeSegments[RopeSegments.Length-1].GetComponent<Rigidbody2D>().drag=0;
			for (int i=0; i<playersOnRope; i++) {
				if (((Input.GetAxis (PC [i].jStickVertical) < -0.49f||Input.GetAxis (PC [i].jStickVertical) > 0.49f)||(Input.GetButton("KB_Up")||Input.GetButton("KB_Down")))&&currentSegment[i]!=RopeSegments.Length-1&&(GM.numOfPlayers==1||PC[i].name=="Player1")){
					if (currentSegment [0] < currentSegment [1] - 5 || currentSegment [0] > currentSegment [1]) {
						PC[i].climbing=true;
					}else{
						PC[i].climbing=false;
					}
					if (currentSegment [1] < currentSegment [0] - 5 || currentSegment [1] > currentSegment [0]) {
						PC[i].climbing=true;
					}else{
						PC[i].climbing=false;
					}
					if (currentSegment [0] > currentSegment [1] + 5 || currentSegment [0] < currentSegment [1]) {
						PC[i].climbing=true;
					}else{
						PC[i].climbing=false;
					}
					if (currentSegment [1] > currentSegment [0] + 5 || currentSegment [1] < currentSegment [0]) {
						PC[i].climbing=true;
					}else{
						PC[i].climbing=false;
					}
				}else{
					PC[i].climbing=false;
				}

				if ((Input.GetAxis (PC [i].jStickVertical) > 0.5f|| (Input.GetButton("KB_Down")&&(GM.numOfPlayers==1||PC[i].name=="Player1")))&& currentSegment [i] != RopeSegments.Length - 1 && !climbCoolingDown [i]) {
					if (playersOnRope > 1) {
						if (i == 0) {
							if (currentSegment [0] < currentSegment [1] - 5 || currentSegment [0] > currentSegment [1]) {
								currentSegment [i] += 1;
								climbCoolingDown [i] = true;
							}
						}
						if (i == 1) {
							if (currentSegment [1] < currentSegment [0] - 5 || currentSegment [1] > currentSegment [0]) {
								currentSegment [i] += 1;
								climbCoolingDown [i] = true;
							}
						}
					} else {
						currentSegment [i] += 1;
						climbCoolingDown [i] = true;
					}
				}
				if ((Input.GetAxis (PC [i].jStickVertical) < -0.5f || (Input.GetButton("KB_Up")&&(GM.numOfPlayers==1||PC[i].name=="Player1")))&& currentSegment [i] != 0 && !climbCoolingDown [i]) {
					if (playersOnRope == 2) {
						if (i == 0) {
							if (currentSegment [0] > currentSegment [1] + 5 || currentSegment [0] < currentSegment [1]) {
								currentSegment [i] -= 1;
								climbCoolingDown [i] = true;
							}
						}
						if (i == 1) {
							if (currentSegment [1] > currentSegment [0] + 5 || currentSegment [1] < currentSegment [0]) {
								currentSegment [i] -= 1;
								climbCoolingDown [i] = true;
							}
						}
					} else if (playersOnRope == 1) {
						currentSegment [i] -= 1;
						climbCoolingDown [i] = true;
					}
				}

			}
			if (Input.GetButtonUp (PC [0].grabbtn) || PC [0].onFire||PC [0].bisected) {
				PC [0] = PC [1];
				Player [0] = Player [1];
				currentSegment [0] = currentSegment [1];
				climbCoolingDown [0] = climbCoolingDown [1];
				climbTime [0] = climbTime [1];
				PC [0].StopGrabbing ();
				if (playersOnRope != 0) {
					playersOnRope -= 1;
				}
			}
			if (playersOnRope > 1) {
				if (Input.GetButtonUp (PC [1].grabbtn) || PC [1].onFire|| PC [1].bisected) {
					PC [1].StopGrabbing ();
					if (playersOnRope != 0) {
						playersOnRope -= 1;
					}
				}
			}
		} else {
			hangTime=0;
			RopeSegments[RopeSegments.Length-1].GetComponent<Rigidbody2D>().drag=1;
		}
	}

	void FixedUpdate(){
		AdjustSwingVolume ();
		for (int i=0; i<playersOnRope; i++) {
			if (climbCoolingDown[i]) {
				climbTime[i] += Time.deltaTime;
				if (climbTime[i] > 0.1f) {
					climbCoolingDown[i] = false;
					climbTime[i] = 0;
				}
			}
		}
	if (playersOnRope > 0) {
			for (int i=0; i<playersOnRope; i++) {
				PC [i].swinging = true;
				PC [i].runningMomentum = 0;
				PC [i].wallJumpL = true;
				PC [i].wallJumpR = true;
				PC [i].anim.SetBool ("Swinging", PC [i].swinging);
				PC [i].capeAnim.SetFloat ("SwingSpeed", RopeSegments [currentSegment [i]].GetComponent<Rigidbody2D>().velocity.x);
				Player [i].GetComponent<Rigidbody2D>().velocity = RopeSegments [currentSegment [i]].GetComponent<Rigidbody2D>().velocity;
				Player [i].position = Vector3.MoveTowards (Player [i].position, new Vector3 (Player [i].position.x, RopeSegments [currentSegment [i]].position.y, Player [i].position.z), 0.35f);
				Player [i].position = Vector3.MoveTowards (Player [i].position, new Vector3 (RopeSegments [currentSegment [i]].position.x, Player [i].position.y, Player [i].position.z), 4);
				Player [i].rotation = RopeSegments [currentSegment [i]].rotation;

				if (!Kinematic&&(RopeSegments [RopeSegments.Length - 1].GetComponent<Rigidbody2D>().velocity.x < 25 && RopeSegments [RopeSegments.Length - 1].GetComponent<Rigidbody2D>().velocity.x > -25)) {
                    if (hangTime < 0.3f) {
                        RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().AddForce(new Vector2((PC[i].runningMomentum * 20) + (Input.GetAxis(PC[i].movebtn) * (7 * RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().mass) * (swingForceMod / playersOnRope)), 0));
                        if (GM.numOfPlayers == 1 || PC[i].name == "Player1")
                        {
                            if (Input.GetButton("KB_MoveL"))
                            {
                                RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().AddForce(new Vector2((PC[i].runningMomentum * 20) + (-1 * (7 * RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().mass) * (swingForceMod / playersOnRope)), 0));
                            }
                            if (Input.GetButton("KB_MoveR"))
                            {
                                RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().AddForce(new Vector2((PC[i].runningMomentum * 20) + (1 * (7 * RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().mass) * (swingForceMod / playersOnRope)), 0));
                            }
                        }
                    } else {
                        RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().AddForce(new Vector2(PC[i].move * (swingForceMod / playersOnRope), 0));
                        if (GM.numOfPlayers == 1 || PC[i].name == "Player1")
                        {
                            if (Input.GetButton("KB_MoveL"))
                            {
                                RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * (swingForceMod / playersOnRope), 0));
                            }
                            if (Input.GetButton("KB_MoveR"))
                            {
                                RopeSegments[currentSegment[i]].GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * (swingForceMod / playersOnRope), 0));
                            }
                        }
                    }
                }

			}
        }else
        {
            RopeSegments[RopeSegments.Length - 1].GetComponent<Rigidbody2D>().velocity = new Vector2(RopeSegments[RopeSegments.Length - 1].GetComponent<Rigidbody2D>().velocity.x * 0.9f, RopeSegments[RopeSegments.Length - 1].GetComponent<Rigidbody2D>().velocity.y);
        }
	}

    public float checkRange()
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

	void AdjustSwingVolume(){
			float vol=Mathf.Abs (RopeSegments [RopeSegments.Length - 1].GetComponent<Rigidbody2D>().velocity.x) / 20;
			if (vol > 1) {
			vol = 1;
			}
			if (vol < 0.1f) {
				vol = 0;
			}
        swingVol = checkRange() * vol;
        if (swingEV != null)
        {
            swingEV.setVolume(swingVol*GM.SFXVol);
        }
    }
}
