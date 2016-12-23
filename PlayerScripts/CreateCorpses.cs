using UnityEngine;
using System.Collections;

public class CreateCorpses : PlayerInheritance {

	public float offsetX = 2.8f;
	public float offsetY = -2.8f;

	public Transform RGroundSpikeCorpse;
	public Transform LGroundSpikeCorpse;
	public Transform RDecapCorpse;
	public Transform LDecapCorpse;
	public Transform DecappedHead;
	public Transform lowerTorso;
	public Transform upperTorso;


	public Transform skeletonR;
	public Transform skeletonL;
    public SkeletonTFix STFR;
    public SkeletonTFix STFL;
    public GameObject acidSkeletonR;
	public GameObject acidSkeletonL;
	public Transform skeletonPoint;
	public Transform WallHangingDeadBody;
	public bool corpseCreated;
	public bool HeadCreated;
	public bool torsoCreated;
	public Transform stungCorpse;
	Dissolve stungCorpseDissolve;
	Dissolve[] FloorBodyDissolvesL;
	Dissolve[] FloorBodyDissolvesR;
	Dissolve[] DecapBodyDissolvesL;
	Dissolve[] DecapBodyDissolvesR;
	Dissolve[] WallBodyDissolves;
	Dissolve headDissolve;
	Dissolve upperTorsoDissolve;
	Dissolve lowerTorsoDissolve;
	
	public HeadCorpse HC;
	WallCorpse WC;
	//Bisected corpse upper and Lower
	public WallCorpse BCU;
	public WallCorpse BCL;
	//ground corpse L and R
	public WallCorpse GCL;
	public WallCorpse GCR;
	//Decap corpse L and R
	public WallCorpse DCL;
	public WallCorpse DCR;
    public bool localFaceright;

	void Start () {
		FindPC ();
		FindPDS ();
		FindLDS ();
		FindGM ();
		FindPM ();
        STFR = skeletonR.GetComponent<SkeletonTFix>();
        STFL= skeletonL.GetComponent<SkeletonTFix>();
        if (DecappedHead.GetComponent<Dissolve> () != null) {
			headDissolve = DecappedHead.GetComponent<Dissolve> ();
		}

		if (upperTorso.GetComponent<Dissolve> () != null) {
			upperTorsoDissolve = upperTorso.GetComponent<Dissolve> ();
		}


		if (lowerTorso.GetComponent<Dissolve> () != null) {
			lowerTorsoDissolve = lowerTorso.GetComponent<Dissolve> ();
		}

		if (WallHangingDeadBody.GetComponentsInChildren<Dissolve> () != null) {
			WallBodyDissolves=new Dissolve[WallHangingDeadBody.GetComponentsInChildren<Dissolve> ().Length];
			WallBodyDissolves = WallHangingDeadBody.GetComponentsInChildren<Dissolve> ();
			foreach (Dissolve D in WallBodyDissolves) {
				D.PCcontroller = PC;
			}
		}
		if (RGroundSpikeCorpse.GetComponentsInChildren<Dissolve> () != null) {
			FloorBodyDissolvesR=new Dissolve[RGroundSpikeCorpse.GetComponentsInChildren<Dissolve> ().Length];
			FloorBodyDissolvesR = RGroundSpikeCorpse.GetComponentsInChildren<Dissolve> ();
			foreach (Dissolve D in FloorBodyDissolvesR) {
				D.PCcontroller = PC;
			}
		}
		if (LGroundSpikeCorpse.GetComponentsInChildren<Dissolve> () != null) {
			FloorBodyDissolvesL=new Dissolve[LGroundSpikeCorpse.GetComponentsInChildren<Dissolve> ().Length];
			FloorBodyDissolvesL = LGroundSpikeCorpse.GetComponentsInChildren<Dissolve> ();
			foreach (Dissolve D in FloorBodyDissolvesL) {
				D.PCcontroller = PC;
			}
		}
		if (RDecapCorpse.GetComponentsInChildren<Dissolve> () != null) {
			DecapBodyDissolvesR=new Dissolve[RDecapCorpse.GetComponentsInChildren<Dissolve> ().Length];
			DecapBodyDissolvesR = RDecapCorpse.GetComponentsInChildren<Dissolve> ();
			foreach (Dissolve D in DecapBodyDissolvesR) {
				D.PCcontroller = PC;
			}
		}
		if (LDecapCorpse.GetComponentsInChildren<Dissolve> () != null) {
			DecapBodyDissolvesL=new Dissolve[LDecapCorpse.GetComponentsInChildren<Dissolve> ().Length];
			DecapBodyDissolvesL = LDecapCorpse.GetComponentsInChildren<Dissolve> ();
			foreach (Dissolve D in DecapBodyDissolvesL) {
				D.PCcontroller = PC;
			}
		}
		stungCorpseDissolve = stungCorpse.GetComponentInChildren<Dissolve> ();
		stungCorpseDissolve.PCcontroller = PC;
		HC = DecappedHead.GetComponent<HeadCorpse> ();
		headDissolve.PCcontroller = PC;
		upperTorsoDissolve.PCcontroller = PC;
		lowerTorsoDissolve.PCcontroller = PC;
		DCL = LDecapCorpse.GetComponent<WallCorpse> ();
		DCR = RDecapCorpse.GetComponent<WallCorpse> ();
		WC=WallHangingDeadBody.GetComponent<WallCorpse>();
		GCL=LGroundSpikeCorpse.GetComponent<WallCorpse>();
		GCR=RGroundSpikeCorpse.GetComponent<WallCorpse>();
		BCU=upperTorso.GetComponent<WallCorpse>();
		BCL=lowerTorso.GetComponent<WallCorpse>();
	}

	public void CreateStungCorpse(){
        stungCorpse.GetComponent<Rigidbody2D>().velocity = new Vector2(PC.rbody.velocity.x, PC.rbody.velocity.y);
        stungCorpse.position = new Vector3 (PC.transform.position.x, PC.transform.position.y, PC.transform.position.z);
		SpriteRenderer sr = stungCorpse.GetComponentInChildren<SpriteRenderer> ();
		BoxCollider2D col1 = stungCorpse.GetComponentInChildren<BoxCollider2D> ();
		Collider2D col2 = stungCorpse.GetComponent<Collider2D> ();
		stungCorpseDissolve.setBool ("Dissolve", false);
		stungCorpseDissolve.setBool ("Enabled", true);
		sr.enabled = true;
		col1.enabled = true;
		col2.enabled = true;
	}

	public void CreateUpperTorso(){
		if (!torsoCreated) {
			//establish facing dir
			if (transform.localScale.x > 0) {	
				upperTorso.transform.localScale=new Vector3(10,10,10);
			} else {
				upperTorso.transform.localScale=new Vector3(-10,10,10);
			}
			//set head position
			upperTorso.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
			upperTorso.position = new Vector3 (transform.position.x, transform.position.y, -2);
			upperTorsoDissolve.setBool ("Dissolve", false);
			upperTorsoDissolve.setBool ("Enabled", true);
			upperTorso.GetComponentInChildren<Collider2D>().enabled=true;
            ParticleSystem.EmissionModule PE = BCU.PS.emission;
			PE.rate= new ParticleSystem.MinMaxCurve(10);
			torsoCreated = true;
		}
	}

	public void CreateLowerTorso(){
		if (!corpseCreated) {
			//establish facing dir
			if (transform.localScale.x > 0) {	
				lowerTorso.transform.localScale=new Vector3(10,10,10);
			} else {
				lowerTorso.transform.localScale=new Vector3(-10,10,10);
			}
			//set head position
			lowerTorso.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
			lowerTorso.position = new Vector3 (transform.position.x, transform.position.y, -1);
			lowerTorsoDissolve.setBool ("Dissolve", false);
			lowerTorsoDissolve.setBool ("Enabled", true);
			lowerTorso.GetComponentInChildren<Collider2D>().enabled=true;
            ParticleSystem.EmissionModule PE = BCL.PS.emission;
			PE.rate=new ParticleSystem.MinMaxCurve(10);
			corpseCreated = true;
		}
	}
	
	public void CreateDecapHead(){
		if (!HeadCreated) {
			//establish facing dir
			if (transform.localScale.x > 0) {	
				HC.transform.localScale=new Vector3(10,10,10);
			} else {
				HC.transform.localScale=new Vector3(10,-10,10);
			}
			//reset local position and rotation
			HC.Reset_Pos_n_Rot();
			//set head position
			DecappedHead.position = new Vector3 (transform.position.x, transform.position.y, -1);
			headDissolve.setBool ("Dissolve", false);
			headDissolve.setBool ("Enabled", true);
			HC.GetComponent<Collider2D>().enabled=true;
            ParticleSystem.EmissionModule PE = HC.PS.emission;
			PE.rate=new ParticleSystem.MinMaxCurve(10);
			HC.LaunchHead();
			HeadCreated = true;
		}
	}

	public void CreateDecapCorpse(){
		if (!corpseCreated) {
			if (transform.localScale.x > 0) {	
				offsetX = Mathf.Abs(offsetX);
				DCR.resetVelocity();
				RDecapCorpse.position = new Vector3 (transform.position.x + 1, transform.position.y+offsetY, -1);
				foreach (Dissolve d in DecapBodyDissolvesR) {
					d.setBool ("Dissolve", false);
					d.setBool ("Enabled", true);
				}
				DCR.EnableCols ();
                ParticleSystem.EmissionModule PE = DCR.PS.emission;
				PE.rate=new ParticleSystem.MinMaxCurve(10);
			} else {
				offsetX =Mathf.Abs(offsetX)*-1;
				DCL.resetVelocity();
				LDecapCorpse.position = new Vector3 (transform.position.x -2, transform.position.y+offsetY, -1);
				foreach (Dissolve d in DecapBodyDissolvesL) {
					d.setBool ("Dissolve", false);
					d.setBool ("Enabled", true);
				}
				DCL.EnableCols ();
                ParticleSystem.EmissionModule PE = DCL.PS.emission;
				PE.rate=new ParticleSystem.MinMaxCurve(10);
			}
			PC.anim.SetBool ("Dead", true);
			corpseCreated = true;
		}
	}

	public void CreateAcidSkeleton()
	{
		Dissolve[] D=new Dissolve[13];
		GameObject newSkeleton;
		Rigidbody2D[] boneRbodies = new Rigidbody2D[13];
		if(PC.facingRight){
			newSkeleton = Instantiate (acidSkeletonR, skeletonPoint.position, skeletonPoint.rotation)as GameObject;
			D=newSkeleton.GetComponentsInChildren<Dissolve>();
			foreach(Dissolve Dis in D){
				Dis.PCcontroller=PC;
			}
			boneRbodies=newSkeleton.GetComponentsInChildren<Rigidbody2D>();
			foreach(Rigidbody2D BRBs in boneRbodies){
				BRBs.isKinematic=true;
			}
		}else{
			newSkeleton = Instantiate (acidSkeletonL, skeletonPoint.position, skeletonPoint.rotation)as GameObject;
			D=newSkeleton.GetComponentsInChildren<Dissolve>();
			foreach(Dissolve Dis in D){
				Dis.PCcontroller=PC;
			}
			boneRbodies=newSkeleton.GetComponentsInChildren<Rigidbody2D>();
			foreach(Rigidbody2D BRBs in boneRbodies){
				BRBs.isKinematic=true;
			}
		}
	}
	public void CreateNewSkeleton()
	{
		Dissolve[] D=new Dissolve[9];
		if(localFaceright){
            STFR.ResetRbodies();
            skeletonR.position = skeletonPoint.position;
			D=skeletonR.GetComponentsInChildren<Dissolve>();
			foreach(Dissolve Dis in D){
				Dis.PCcontroller=PC;
                Dis.setBool("Dissolve", false);
                Dis.setBool("Enabled", true);
            }
			foreach(Rigidbody2D BRBs in STFR.rbodies){
               BRBs.velocity=GetComponent<Rigidbody2D>().velocity;
			}
		}else{
            STFL.ResetRbodies();
            skeletonL.position = skeletonPoint.position;
            D = skeletonL.GetComponentsInChildren<Dissolve>();
            foreach (Dissolve Dis in D)
            {
                Dis.PCcontroller = PC;
                Dis.setBool("Dissolve", false);
                Dis.setBool("Enabled", true);
            }
            foreach (Rigidbody2D BRBs in STFL.rbodies)
            {
                BRBs.velocity = GetComponent<Rigidbody2D>().velocity;
            }
        }
	}
	
	public void CreateGroundSpikeCorpse(){
		if (!corpseCreated) {
			if (PC.facingRight) {	
				offsetX = Mathf.Abs(offsetX);
				GCR.resetVelocity();
				RGroundSpikeCorpse.position = new Vector3 (transform.position.x + offsetX, transform.position.y+offsetY, -1);
				foreach (Dissolve d in FloorBodyDissolvesR) {
					d.setBool ("Dissolve", false);
					d.setBool ("Enabled", true);
				}
				GCR.EnableCols ();
			} else {
				offsetX =Mathf.Abs(offsetX)*-1;
				GCL.resetVelocity();
				LGroundSpikeCorpse.position = new Vector3 (transform.position.x + offsetX, transform.position.y+offsetY, -1);
				foreach (Dissolve d in FloorBodyDissolvesL) {
					d.setBool ("Dissolve", false);
					d.setBool ("Enabled", true);
				}
				GCL.EnableCols ();
			}
			PC.anim.SetBool ("Dead", true);
			corpseCreated = true;
		}
	}


	public void CreateWallCorpse(){
		if (!corpseCreated) {
			WallHangingDeadBody.position= new Vector3(transform.position.x,transform.position.y,-1);
			if (transform.localScale.x > 0) {
				WallHangingDeadBody.localScale=new Vector3(1,1,1);
			}else{
				WallHangingDeadBody.localScale=new Vector3(1,-1,1);
			}
			foreach(Dissolve d in WallBodyDissolves){
				d.setBool("Dissolve",false);
				d.setBool("Enabled",true);
			}
			WC.EnableCols();
            ParticleSystem.EmissionModule PE = WC.PS.emission;
			PE.rate=new ParticleSystem.MinMaxCurve(10);
			PC.anim.SetBool ("Dead", true);
			corpseCreated=true;
		}
	}
    public void GetFacingDiretion()
    {
        localFaceright = PC.facingRight;
        PC.flipDisabled = true;
    }
	public void ResetCorpses(){
	
	}
}
