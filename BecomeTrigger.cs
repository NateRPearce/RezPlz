using UnityEngine;
using System.Collections;

public class BecomeTrigger : MonoBehaviour {

	public Collider2D C;

	public void BeTrigger(int tf){
		if (tf == 0) {
			C.isTrigger = false;
		} else {
			C.isTrigger = true;
		}
	}
}
