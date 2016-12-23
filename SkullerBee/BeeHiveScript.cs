using UnityEngine;
using System.Collections;

public class BeeHiveScript : GameStateFunctions {

	public Transform beePrefab;
	public Transform patrolPoint1;
	public Transform patrolPoint2;
	public Transform spawnPoint;
	public Transform currentBee;
	SkullerBeeBehavior SBB;

	void Start () {
		FindGM ();
		SpawnABee();
	}
	
	void FixedUpdate () {
		if (SBB == null) {
			return;
		}
		if (SBB.dead && !GM.PM.PDS1.playerDead && !GM.PM.PDS2.playerDead) {
			SpawnABee();
		}
	}

	void SpawnABee(){
		currentBee=Instantiate(beePrefab,spawnPoint.position,beePrefab.rotation)as Transform;
		SBB =currentBee.GetComponentInChildren<SkullerBeeBehavior>();
		SBB.patrolPoint1.position=patrolPoint1.position;
		SBB.patrolPoint2.position=patrolPoint2.position;
	}
}
