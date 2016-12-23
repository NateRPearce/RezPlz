using UnityEngine;
using System.Collections;

public class BossWeakPoint : MonoBehaviour {

	CaveBossBehavior CBB;

	void Start () {
		CBB = GetComponentInParent<CaveBossBehavior> ();
	}
	

    public void WeakPointHit() {
        CBB.Hit();		
	}
}
