using UnityEngine;
using System.Collections;

public class BatlingVision : MonoBehaviour {

	BatlingBehavior BB;
    public bool searchEnabled;

	void Awake(){
		BB = GetComponentInParent<BatlingBehavior> ();
        StartCoroutine("TurnOnSearch");
	}



void OnTriggerStay2D(Collider2D other){
        if (!searchEnabled)
        {
            return;
        }
	if (other.tag == "Player"&&other.GetComponent<PlayerControls>()!=null) {
			BB.targetPlayer=other.transform;
			BB.PC=other.GetComponent<PlayerControls>();
			BB.playerSpotted=true;
			GetComponent<Collider2D>().enabled=false;
		}
	}

	public void EnableCol(){
		GetComponent<Collider2D>().enabled=true;
	}
    IEnumerator TurnOnSearch()
    {
        yield return new WaitForSeconds(0.5f);
        searchEnabled = true;
    }
}
