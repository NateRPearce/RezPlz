using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneBlockage : MonoBehaviour {

	public List<Rigidbody2D> stones;
	float crumbleTime=0;
	float crumbleRate=0.05f;
	public int currentStone = 0;
	public ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
	public bool broken;
	// Use this for initialization
	void Start () {
		stones= new List<Rigidbody2D>();
		stones.AddRange(GetComponentsInChildren<Rigidbody2D>());
		PS = GetComponentInChildren<ParticleSystem> ();
        PE = PS.emission;
	}

	void FixedUpdate(){
		if (broken) {
			Crumble();
		}
	}
	public void Crumble(){
		if (currentStone > (stones.Count/2)) {
            PE.rate = new ParticleSystem.MinMaxCurve(0);
        }

		if (currentStone == stones.Count) {
			return;
		}
        PE.rate = new ParticleSystem.MinMaxCurve(35);
        crumbleTime += Time.deltaTime;
		if (crumbleTime > crumbleRate) {
			stones[currentStone].isKinematic=false;
			crumbleRate+=Random.Range(0,0.05f);
			currentStone+=1;
		}
		
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerControls>()) {
			PlayerControls PC=other.GetComponent<PlayerControls>();
			if(PC.stoneSkinActivated&&other.GetComponent<Rigidbody2D>().velocity.y<-30){
				broken=true;
			}
		}
	}
}
