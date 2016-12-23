using UnityEngine;
using System.Collections;

public class BossLevelController : MonoBehaviour {


	public int phase;
	bool[] PhaseComplete = new bool[3];
	Collider2D col;
	public float phaseSwitchCooldown;
	public DestroySequenceScript DSS;
	public Transform[] movingPlats = new Transform[3];
	public Transform[] DragonMaw = new Transform[4];
	public Transform[] LavaBlast = new Transform[3];
	RemoteTriggerScript[] LBRTS;
	DragonMawScript[] DMS;
	RemoteTriggerScript[] RTS;
	MovingPlatScript[] MPS;
	// Use this for initialization
	void Start () {
		col = GetComponent<Collider2D> ();
		DSS = GetComponent<DestroySequenceScript> ();
		for(int i=0;i<3;i++){
			PhaseComplete[i]=false;
		}
		LBRTS = new RemoteTriggerScript[LavaBlast.Length];
		for(int i=0;i<LavaBlast.Length;i++){
			LBRTS[i]=LavaBlast[i].GetComponent<RemoteTriggerScript>();
		}
		MPS=new MovingPlatScript[movingPlats.Length];
		for (int i=0; i<movingPlats.Length; i++) {
			MPS[i] = movingPlats[i].GetComponent<MovingPlatScript>();
		}
		DMS=new DragonMawScript[DragonMaw.Length];
		RTS = new RemoteTriggerScript[DragonMaw.Length];
		for (int i=0; i<DragonMaw.Length; i++) {
			DMS[i] = DragonMaw[i].GetComponent<DragonMawScript>();
			RTS[i]= DragonMaw[i].GetComponent<RemoteTriggerScript>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	if (phase == 1&&!PhaseComplete[1]) {
			Phase1();
		}
		if (phase == 2&&!PhaseComplete[2]) {
			Phase2 ();
		}
	}

	void FixedUpdate(){
		if (!col.enabled) {
			phaseSwitchCooldown -= Time.deltaTime;
		}
		if (phaseSwitchCooldown < 0) {
			col.enabled=true;
		}
	}

	public void Phase1(){
		foreach(MovingPlatScript MS in MPS){
			MS.target2.position=new Vector3(MS.target2.position.x,MS.target2.position.y-10,MS.target2.position.z);
			MS.toggle=true;
			MS.goingToTarget=true;
		}
		foreach(RemoteTriggerScript RT in RTS){
			RT.Activated=true;
		}
		DMS[0].blastFrequency=4.8f;
		DMS[1].blastFrequency=3.9f;
		DMS [3].StartTime = 5;
		PhaseComplete[1]=true;
	}


	public void Phase2(){
		DMS[0].blastFrequency=3.8f;
		DMS[1].blastFrequency=2.9f;
		foreach(RemoteTriggerScript RT in LBRTS){
			RT.Activated=true;
		}
		DSS.DestroySequence();
		PhaseComplete[2]=true;
	}


	void OnTriggerEnter2D(Collider2D other){
	if (other.tag == "Player") {
			phase+=1;
			if (phase == 3) {
				Debug.Log ("Level COmplete");
			}
			phaseSwitchCooldown=5;
			col.enabled=false;
		}
	}
}