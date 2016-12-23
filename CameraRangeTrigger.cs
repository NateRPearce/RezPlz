using UnityEngine;
using System.Collections;

public class CameraRangeTrigger : GameStateFunctions {

	public float newXMin;
	public float newYMin;
	public float newXMax;
	public float newYMax;
	// Use this for initialization
	void Start () {
		FindCBS ();
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "SkullGuide") {
			if(newXMax!=0){
				CBS.xMax=newXMax;
			}
			if(newXMin!=0){
				CBS.xMin=newXMin;
			}
			if(newYMax!=0){
				CBS.yMax=newYMax;
			}
			if(newYMin!=0){
				CBS.yMin=newYMin;
			}
		}
	}

}
