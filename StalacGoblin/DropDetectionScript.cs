using UnityEngine;
using System.Collections;

public class DropDetectionScript : MonoBehaviour {

    public float delay;
	StalacGoblinScript SGS;
	void Start () {
		SGS = GetComponentInParent<StalacGoblinScript> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player"){
            StartCoroutine("DropIt");
            GetComponent<Collider2D>().enabled = false;
        }
	}

    IEnumerator DropIt()
    {
        yield return new WaitForSeconds(delay);
        SGS.CreateStalactite();
    }
}
