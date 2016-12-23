using UnityEngine;
using System.Collections;

public class RaceCheckPoint : MonoBehaviour {

    RaceController RC;
	void Start () {
        RC = GetComponentInParent<RaceController>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player1")
        {
            RC.p1CheckPoint = this.transform;
        }
        if (other.name == "Player2")
        {
            RC.p2CheckPoint = this.transform;
        }
    }
}
