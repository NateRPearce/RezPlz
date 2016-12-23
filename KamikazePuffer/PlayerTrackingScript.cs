using UnityEngine;
using System.Collections;

public class PlayerTrackingScript : MonoBehaviour {

	public PufferScript PS;

	void Awake(){
		PS = GetComponentInParent<PufferScript> ();
	}

	public void OnTriggerStay2D(Collider2D other){
	if (other.GetComponentInParent<PlayerDeathScript>()!=null&&PS.Entered) {
			PlayerDeathScript pds=other.GetComponentInParent<PlayerDeathScript>();
			if(pds.playerDead){
				return;
			}
		PS.playerSpotted=true;
		PS.playerPosX=other.transform.position.x;
		PS.playerPosY=other.transform.position.y;
			if(other.transform.position.x>transform.position.x){
				PS.playerIsEast=true;
				PS.playerIsWest=false;
			}else{
				PS.playerIsWest=true;
				PS.playerIsEast=false;
			}
			if(other.transform.position.y>transform.position.y){
				PS.playerIsNorth=true;
				PS.playerIsSouth=false;
			}else{
				PS.playerIsSouth=true;
				PS.playerIsNorth=false;
			}
		}
	}
	public void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			PS.playerSpotted=false;
			PS.playerPosX=0;
			PS.playerPosY=0;
		}
	}
}
