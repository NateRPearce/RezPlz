using UnityEngine;
using System.Collections;

public class SplatterBlood : GameStateFunctions {


	public Transform bloodPool;
	public Transform[] bloodSplatterL = new Transform[1];
	public Transform[] bloodSplatterR = new Transform[1];
	public Transform[] groundBloodSplatter = new Transform[1];
	public LayerMask whatIsBlood;
	public bool BloodFound;

	void Start(){
		FindGM ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "LWall") {
			Transform newBlood;
				int rando =Random.Range(0,bloodSplatterL.Length);
			newBlood =Instantiate(bloodSplatterL[rando],new Vector3( other.transform.position.x,transform.position.y,-16f),bloodSplatterL[rando].transform.rotation)as Transform;		
			GM.MBC.bloodSplatterCollection.Add (newBlood.gameObject);
		}
		if (other.tag == "RWall") {
			Transform newBlood;
			int rando =Random.Range(0,bloodSplatterR.Length);
			newBlood =Instantiate(bloodSplatterR[rando],new Vector3( other.transform.position.x,transform.position.y,-16f),bloodSplatterR[rando].transform.rotation)as Transform;		
			GM.MBC.bloodSplatterCollection.Add (newBlood.gameObject);	
		}

		if (other.tag == "GroundBloodCollider") {
			Transform newBlood;
			int rando =Random.Range(0,groundBloodSplatter.Length);
			newBlood = Instantiate(groundBloodSplatter[rando],new Vector3( transform.position.x,other.transform.position.y,-16f),groundBloodSplatter[rando].transform.rotation)as Transform;		
			GM.MBC.bloodSplatterCollection.Add (newBlood.gameObject);
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "GroundBloodCollider") {
			Transform newBlood;
			BloodFound=	Physics2D.OverlapCircle (transform.position, 1, whatIsBlood);
			if(!BloodFound){
				newBlood = Instantiate(bloodPool,new Vector3( transform.position.x,other.transform.position.y,-12.6f),bloodPool.transform.rotation)as Transform;		
				GM.MBC.bloodTrailCollection.Add (newBlood.gameObject);
			}
		}
	}
}
