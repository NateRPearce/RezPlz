using UnityEngine;
using System.Collections;

public class BringOtherPlayer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
	if (other.GetComponent<PlayerControls> () != null) {
			PlayerControls PC=other.GetComponent<PlayerControls>();
			PC.otherPlayer.position=new Vector3(other.transform.position.x-5,other.transform.position.y,other.transform.position.z);
		}
	}

}
