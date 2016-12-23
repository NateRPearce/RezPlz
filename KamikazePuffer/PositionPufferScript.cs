using UnityEngine;
using System.Collections;

public class PositionPufferScript : MonoBehaviour {

	public PufferScript PS;
	public float flapTimer;
	public float flapFrequency;
	public bool Done;
	public float timer;
	private float flapDuration;
	public string direction;

	void Start () {
		PS = GetComponent<PufferScript> ();
		PS.stopFlapping = true;
		timer = 0;
		flapTimer = 0;
		flapDuration = PS.enterTime;
		Done = false;
	}
	

	void FixedUpdate () {
		if (Done) {
			return;		
		}
		flapTimer += Time.deltaTime;
		if (flapTimer > flapFrequency) {
			if(direction=="NW"){
			PS.FlapNW(20);
			PS.anim.SetBool("PuffNW",true);
			}else if(direction=="SW"){
				PS.FlapSW(20);
				PS.anim.SetBool("PuffSW",true);
			}else if(direction=="NE"){
				PS.FlapNE(20);
				PS.anim.SetBool("PuffNE",true);
			} else if(direction=="SE"){
				PS.FlapSE(20);
				PS.anim.SetBool("PuffSE",true);
			}
		}
		timer += Time.deltaTime;
		if (timer > flapDuration) {
			PS.stopFlapping=false;
			Done=true;
		}
	}
}
