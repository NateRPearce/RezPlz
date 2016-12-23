using UnityEngine;
using System.Collections;

public class DestroyTriggerScript : MonoBehaviour {

	BridgeScript BS;
	public string trigger;
	public GameObject[] DestructionList = new GameObject[1];
	public float destroyDelay;
	public bool collapseTriggered;
	public float realTime;
	public bool targetDestoryed;
	int currentListItem=0;
	public bool destroyBridge;
	public bool destroyRope;
	PlayerControls PC;
	RopeSegment Rseg;

	void Start(){
		if (GetComponentInParent<BridgeScript> () != null) {
			BS = GetComponentInParent<BridgeScript> ();
			destroyBridge=true;
		}
		if (GetComponent<RopeSegment> () != false) {
			Rseg=GetComponent<RopeSegment> ();
			destroyRope=true;
		}

	}


	void FixedUpdate(){
	if (collapseTriggered) {
			realTime+=Time.deltaTime;
			if(realTime>destroyDelay&&!targetDestoryed){
				if(currentListItem<DestructionList.Length){
					Destroy(DestructionList[currentListItem]);
					GetComponent<Collider2D>().enabled=false;
					if(destroyRope){
						Rseg.RS.playersOnRope=0;
						PC.StopGrabbing();
					}
					realTime=0;
					currentListItem+=1;
				}else{
					targetDestoryed=true;
					if(destroyBridge){
					BS.BridgeDestroyed=true;
					}
					//Destroy(gameObject);
				}
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == trigger) {
			collapseTriggered=true;
			if(trigger=="Player"){
				PC=other.GetComponentInParent<PlayerControls>();
			}
		}
	}
}
