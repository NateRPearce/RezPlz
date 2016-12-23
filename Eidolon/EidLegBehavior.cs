using UnityEngine;
using System.Collections;

public class EidLegBehavior : MonoBehaviour {

	public float moveRef;
	public bool groundedRef;
	public Transform[] LegSections= new Transform[0];


	void FixedUpdate () {
	if (moveRef == 0) {
			for (int i=0; i<LegSections.Length; i++) {
				LegSections [i].localPosition = Vector3.MoveTowards (LegSections [i].localPosition, new Vector3(0,0,0), Time.deltaTime * 4);
			}
		} else {
			if(groundedRef){
			for (int i=0; i<LegSections.Length; i++) {
				float f=i;
				LegSections [i].localPosition = Vector3.MoveTowards (LegSections [i].localPosition, new Vector3(-f/75,0,0), Time.deltaTime * 4);
			}
			}else{
				for (int i=0; i<LegSections.Length; i++) {
					float f=i;
					LegSections [i].localPosition = Vector3.MoveTowards (LegSections [i].localPosition, new Vector3(f/75,0,0), Time.deltaTime * 4);
				}
			}
		}
	}
}
