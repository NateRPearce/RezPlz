using UnityEngine;
using System.Collections;

public class RisingLava : GameStateFunctions {

	public float risingRate;
	public Transform[] ObjectsAffected = new Transform[1];
	bool startRising;

	void Start () {
		FindCBS ();
		InvokeRepeating ("Rise", Time.deltaTime, 0.2f);
	}
public void Rise(){
		if (!startRising) {
			return;
		}
		CBS.yMin += risingRate;
		for(int i = 0;i<ObjectsAffected.Length;i++){
			if(ObjectsAffected[i]!=null){
				ObjectsAffected[i].localPosition=new Vector3(ObjectsAffected[i].localPosition.x,ObjectsAffected[i].localPosition.y+risingRate,ObjectsAffected[i].localPosition.z);
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			startRising=true;
		}
	}
}
