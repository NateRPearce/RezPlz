using UnityEngine;
using System.Collections;

public class SkullGuideHideInWalls : GameStateFunctions {

    [FMODUnity.EventRef]
    string switchSound = "event:/ROB_WORKFOLDER/General_PassCharacter";
    FMOD.Studio.EventInstance switchEV;

    SpriteRenderer sr;
	public bool eidolonActivated;
	public bool inAWall;
	float alpha;
	public LayerMask whatIsWall;
	public ParticleSystem[] PS=new ParticleSystem[2];
    ParticleSystem.EmissionModule[] PEs;
	Light L;
	FollowScript FS;
	int currentSelectedPlayer;
	public Animator anim;
	public bool transforming;
    public Transform tempTarget;
    bool soundReady;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        L = GetComponentInChildren<Light>();
        anim = GetComponentInChildren<Animator>();
        FS = GetComponent<FollowScript>();
    }

	void Start () {
        switchEV = FMODUnity.RuntimeManager.CreateInstance(switchSound);
        alpha = 1;
		currentSelectedPlayer = 0;
        PEs = new ParticleSystem.EmissionModule[PS.Length];
        for (int i = 0; i < PS.Length; i++)
        {
            PEs[i] = PS[i].emission;
        }
        FindGM();
        GM.skullGuide = transform;
    }
	void Update(){
		if (FS.target == null||transforming) {
			transform.position=new Vector3(transform.position.x,transform.position.y,-35);
			return;
		}

			inAWall = Physics2D.OverlapCircle (transform.position, 0.1f, whatIsWall);

		if (FS.target.name == "Player1") {
			currentSelectedPlayer = 0;
			PEs[1].rate=new ParticleSystem.MinMaxCurve(0);
		} else if(FS.target.name == "Player2"){
			currentSelectedPlayer=1;
            PEs[0].rate = new ParticleSystem.MinMaxCurve(0);
        }
	}


	void FixedUpdate(){
        if (FS.target == GM.PM.Player1 || FS.target == GM.PM.Player2){
            if (!GM.PM.PDS1.playerDead || !GM.PM.PDS2.playerDead)
            {
                if (tempTarget != FS.target&&soundReady)
                {
                    switchEV.start();
                }
                if (!soundReady)
                {
                    soundReady = true;
                }
            }
        }
        tempTarget = FS.target;
		if(transforming){
			sr.color = new Color (1, 1, 1, 1);
			return;
		}
		if (inAWall||eidolonActivated) {
			if (alpha > 0) {
				alpha -= Time.deltaTime * 3;
				sr.color = new Color (1, 1, 1, alpha);
                if (eidolonActivated)
                {
                    if (PEs[currentSelectedPlayer].rate.constantMax > 0)
                    {
                        PEs[currentSelectedPlayer].rate = new ParticleSystem.MinMaxCurve(0);
                    }
                }
                else {
                    if (PEs[currentSelectedPlayer].rate.constantMax > 0)
                    {
                        PEs[currentSelectedPlayer].rate = new ParticleSystem.MinMaxCurve(PEs[currentSelectedPlayer].rate.constantMax - 2);
                    }
                }
				L.intensity = alpha;
			}

		} else {
			if (alpha < 0.8f) {
				alpha += Time.deltaTime * 3;
				sr.color = new Color (1, 1, 1, alpha);
				L.intensity = alpha;
				if (PEs[currentSelectedPlayer].rate.constantMax < 30) {
                    PEs[currentSelectedPlayer].rate = new ParticleSystem.MinMaxCurve(PEs[currentSelectedPlayer].rate.constantMax + 2);

                }
            } else if (!transforming) {
				alpha = 0.8f;
				PEs [currentSelectedPlayer].rate = new ParticleSystem.MinMaxCurve(30);
			} 
		}
	}
}
