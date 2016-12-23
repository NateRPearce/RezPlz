using UnityEngine;
using System.Collections;

public class RemoteLink : MonoBehaviour {

	public Transform[] target = new Transform[1];
	RemoteTriggerScript[] targetRTS;
	RemoteTriggerScript selfRTS;
	void Start () {
		selfRTS = GetComponent<RemoteTriggerScript> ();
		targetRTS = new RemoteTriggerScript[target.Length];
		for(int i=0;i<target.Length; i++){
			targetRTS[i]=target[i].GetComponent<RemoteTriggerScript>();
		}
		InvokeRepeating ("ActiveCheck", Time.deltaTime, 0.05f);
	}
	

	void ActiveCheck(){
		if (selfRTS.Activated != targetRTS [targetRTS.Length-1].Activated) {
			for (int i=1; i<target.Length; i++) {
				targetRTS [i].Activated = selfRTS.Activated;
			}
		}
	}
}
