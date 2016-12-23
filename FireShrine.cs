using UnityEngine;
using System.Collections;

public class FireShrine : BreakableScript {

	Animator anim;
	public Light[] fireLight = new Light[0];
	public ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
	public SpriteRenderer[] LightGlow = new SpriteRenderer[0];
	public Collider2D lightCollider;
	public Transform[] Target= new Transform[0];
	RemoteTriggerScript[] targetRTS;


	void Start(){
		anim = GetComponent<Animator> ();
		targetRTS = new RemoteTriggerScript[Target.Length];
		for (int i = 0; i<Target.Length; i++) {
			targetRTS[i]=Target[i].GetComponent<RemoteTriggerScript>();
		}
        PE = PS.emission;
	}
	public override void Break(string deadlyTag){
		if (deadlyTag == "Fireball"|| deadlyTag == Tags.fire) {
			anim.SetBool("Activated",true);
			for (int i = 0; i<Target.Length; i++) {
				targetRTS[i].Activated=true;
			}
		}
	}
	public void LitUp(){
		LightDetectionScript LDS = GetComponent<LightDetectionScript>();
		LDS.onFire=true;
		lightCollider.enabled=true;
		fireLight[0].enabled=true;
		fireLight[1].enabled=true;
		PE.rate=new ParticleSystem.MinMaxCurve(150);
		foreach(SpriteRenderer S in LightGlow){
			S.enabled=true;
		}
	}
}
