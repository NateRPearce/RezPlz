using UnityEngine;
using System.Collections;

public class HeadCorpse : MonoBehaviour {

	
	public Vector3 startingPos;
	public Quaternion startingRot;
	public ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
	// Use this for initialization
	void Awake () {
		DissableCols ();
		getStartingPos ();
		getStartingRot ();
		PS = GetComponentInChildren<ParticleSystem> ();
        PE = PS.emission;
		PE.rate =new ParticleSystem.MinMaxCurve (0);
	}
	
	public void DissableCols(){
			GetComponent<Collider2D>().enabled=false;
	}

	public void Reset_Pos_n_Rot(){
			GetComponent<Rigidbody2D>().isKinematic=true;
		//return parts to their proper position & rotation
			transform.localPosition = startingPos;
			transform.localRotation = startingRot;
		//reactivate physics
			GetComponent<Rigidbody2D>().isKinematic=false;
		}

	
	void getStartingPos(){
			startingPos = transform.localPosition;
	}

	void getStartingRot(){
		startingRot = transform.localRotation;
	}

	public void LaunchHead(){
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range (-200, 200), 2000));
	}
}
