using UnityEngine;
using System.Collections;

public class WeightTrigger : MonoBehaviour {


	public Transform Target;
	public Transform otherPlat;
	RemoteTriggerScript RTS;
	public bool[] HeavyPlayerOnPlat=new bool[2];

	void Start(){
			RTS = Target.GetComponent<RemoteTriggerScript> ();
	}

	void Update(){
	if (HeavyPlayerOnPlat [0] || HeavyPlayerOnPlat [1]) {
				RTS.Activated = true;

			if(otherPlat!=null){
				otherPlat.GetComponent<RemoteTriggerScript>().Activated=true;
			}
		} else {
				RTS.Activated = false;

			if(otherPlat!=null){
				otherPlat.GetComponent<RemoteTriggerScript>().Activated=false;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<PlayerControls> () != null) {
			PlayerControls PC=other.GetComponent<PlayerControls>();
			if(other.name=="Player1"){
			Rigidbody2D rBody=other.GetComponent<Rigidbody2D>();
			if(rBody.mass>10){
					if(PC.anim.GetBool("FullStone")){
					HeavyPlayerOnPlat[0]=true;
							RTS.hold = false;
					}else if(!HeavyPlayerOnPlat[1]){
							RTS.hold = true;
					}
			}else{
					HeavyPlayerOnPlat[0]=false;
			}
			}
			if(other.name=="Player2"){
				Rigidbody2D rBody=other.GetComponent<Rigidbody2D>();
				if(rBody.mass>10){
					if(PC.anim.GetBool("FullStone")){
						HeavyPlayerOnPlat[1]=true;
							RTS.hold = false;
					}else if(!HeavyPlayerOnPlat[0]){
							RTS.hold = true;
					}
				}else{
					HeavyPlayerOnPlat[1]=false;
				}
			}
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<PlayerControls> () != null) {
			if(other.name=="Player1"){
					HeavyPlayerOnPlat[0]=false;
			}
			if(other.name=="Player2"){
				HeavyPlayerOnPlat[1]=false;
			}
		}
	}
}
