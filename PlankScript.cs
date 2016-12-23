using UnityEngine;
using System.Collections;

public class PlankScript : MonoBehaviour {

	BridgeScript BS;
	// Use this for initialization
	void Start () {
		BS = GetComponentInParent<BridgeScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	if (BS.BridgeDestroyed) {
			gameObject.layer=17;
		}
	}
}
