using UnityEngine;
using System.Collections;

public class TriggerCrystalPlayerSensor : MonoBehaviour {

	bool playerNear;
	public float GlowTime;
	public Transform CrystalLight;

	void FixedUpdate () {
		if (playerNear) {
			GlowTime+=Time.deltaTime;
			if(GlowTime<1){
				CrystalLight.GetComponent<Light>().intensity+=Time.deltaTime*2;	
			}else if(GlowTime>1&&GlowTime<2){
				CrystalLight.GetComponent<Light>().intensity-=Time.deltaTime*2;	
			}else{
				GlowTime=0;
			}
		}else{
			GlowTime=0;
			if(CrystalLight.GetComponent<Light>().intensity>0){
				CrystalLight.GetComponent<Light>().intensity-=Time.deltaTime*4;	
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			playerNear=true;	
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			playerNear=false;	
		}
	}
}
