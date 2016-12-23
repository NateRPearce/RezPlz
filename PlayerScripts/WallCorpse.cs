using UnityEngine;
using System.Collections;

public class WallCorpse : MonoBehaviour {

	Collider2D[] cols = new Collider2D[0];
	public Vector3[] partsPos;
	public Quaternion[] partsRot;
	Rigidbody2D[] rBodies = new Rigidbody2D[0];
	SpikeCorpseCollisionScript SCCS;
	public ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
    // Use this for initialization
    void Awake () {
		if (GetComponentInChildren<SpikeCorpseCollisionScript> () != null) {
			SCCS=GetComponentInChildren<SpikeCorpseCollisionScript>();
		}
		if (GetComponentInChildren<ParticleSystem> () != null) {
			PS = GetComponentInChildren<ParticleSystem> ();
            PE = PS.emission;
			PE.rate = new ParticleSystem.MinMaxCurve(0);
		}
		cols = GetComponentsInChildren<Collider2D> ();
		partsPos = new Vector3[cols.Length];
		partsRot = new Quaternion[cols.Length];
		rBodies = GetComponentsInChildren<Rigidbody2D> ();
		DissableCols ();
		getStartingPos ();
		getStartingRot ();
	}

	public void DissableCols(){
	foreach (Collider2D col in cols) {
			col.enabled=false;
		}
	}

	public void EnableCols(){
		StartCoroutine("Enable");
	}

	IEnumerator Enable(){
		yield return new WaitForSeconds (0.02f);
		foreach (Collider2D col in cols) {
			col.enabled=true;
		}
	}

	public void resetVelocity(){
		foreach (Rigidbody2D r in rBodies) {
			r.velocity=new Vector3(0,0,0);
		}
		//return parts to their proper position & rotation
		for (int i=0; i<cols.Length; i++) {
			cols [i].transform.localPosition = partsPos[i];
			cols [i].transform.localRotation = partsRot[i];
		}
	}

	void getStartingPos(){
		for (int i=0; i<cols.Length; i++) {
			partsPos [i] = cols [i].transform.localPosition;
		}
	}
	void getStartingRot(){
		for (int i=0; i<cols.Length; i++) {
			partsRot [i] = cols [i].transform.localRotation;
		}
	}
	public void ResetTriggered(){
		SCCS.triggered = false;
	}
}
