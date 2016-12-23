using UnityEngine;
using System.Collections;

public class GoblinDetectionScript : MonoBehaviour {

	StalacGoblinScript SGS;
	void Start () {
		SGS = GetComponentInParent<StalacGoblinScript> ();
	}

}
