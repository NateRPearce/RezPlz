using UnityEngine;
using System.Collections;

public class CheckPointScript : GameStateFunctions {

	public Transform respawnPoint;
	//Animator anim;


	void Start(){
		//anim = GetComponent<Animator> ();
		FindGM ();
		if (name == "start checkpoint") {
			GM.CheckPointPos=respawnPoint.position;	
		}
	}


	void OnTriggerEnter2D(Collider2D other){
	 if(other.tag=="Player"){
            if (name == "Boss_Checkpoint")
            {
                GM.startAtBoss = true;
                GM.BossCheckPointPos = respawnPoint.position;
            }
			if(name=="checkpoint"){	
				GM.CheckPointPos=respawnPoint.position;
			}
		}
	}
}
