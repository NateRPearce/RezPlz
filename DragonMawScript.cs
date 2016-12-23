using UnityEngine;
using System.Collections;

public class DragonMawScript : RemoteControllable {

	public Transform Projectile;
	public Transform[] ProjectileList = new Transform[5];
	public bool hasNoRigidBody;
	//public Transform launchSmoke;
	public float StartTime;
	public float xForce;
	public float yForce;
	public bool StartOn;
	public bool StayOn;
	public bool singleFire;
	int currentProjectile=0;
	public float blastFrequency;
	Animator anim;
	public Transform spawnPoint;

	
	void Awake () {
		if(RemoteControl){
			RTS = GetComponent<RemoteTriggerScript> ();
		}
		for(int i=0;i<ProjectileList.Length;i++){
			Transform P;
			P=Instantiate(Projectile,transform.position,transform.rotation)as Transform;
			ProjectileList[i]=P;
		}
	}


	public void Start () {
		if (singleFire) {
			blastFrequency=0;
		}
		CheckDifficulty ();
		anim = GetComponent<Animator> ();
		if (RemoteControl) {
			RTS=GetComponent<RemoteTriggerScript>();		
		}
	}
	
	void Update(){
		CheckDifficulty();
		if (disabled) {
			return;
		}
		if(RemoteControl){
			CheckActiveStatus ();
		}
		if (RemoteControl) {
			activated=RTS.Activated;
			if(activated&&StayOn){	
				anim.SetBool("Active",true);
			}else{
				anim.SetBool("Active",false);
			}
		}
	}

	void FixedUpdate () {
		if (disabled) {
			return;
		}
		if (activated) {
			if (singleFire) {
				if (StartTime == blastFrequency) {
					anim.SetBool ("Blast", true);
				}
			} else {
				if (StartTime > blastFrequency) {
					anim.SetBool ("Blast", true);
					StartTime = 0;
				}
			}
			StartTime += Time.deltaTime;
		} else {
			StartTime = 0;
		}
	}
	
	void Fire() 
	{
		//Instantiate (launchSmoke, transform.position, transform.rotation);
		ProjectileList[currentProjectile].position = spawnPoint.position;
		ProjectileList [currentProjectile].GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		if (ProjectileList [currentProjectile].GetComponent<Animator> () != null) {
			Animator ProjectileAnim = ProjectileList [currentProjectile].GetComponent<Animator> ();
			ProjectileAnim.SetBool ("Activated",true);
		}
		if(!hasNoRigidBody){
			ProjectileList[currentProjectile].GetComponent<Rigidbody2D>().AddForce (new Vector2(xForce, yForce));
		}
		currentProjectile += 1;
		if (currentProjectile == ProjectileList.Length) {
			currentProjectile=0;
		}
	}
	public void DoneBlasting (){
			anim.SetBool("Blast",false);
	}
}
