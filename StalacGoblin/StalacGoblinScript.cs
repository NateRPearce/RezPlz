using UnityEngine;
using System.Collections;

public class StalacGoblinScript : MonoBehaviour {

	public Transform Stalactite;

	public void CreateStalactite(){
		Transform newStalac;
		newStalac=Instantiate (Stalactite, transform.position, transform.rotation)as Transform;
		newStalac.GetComponent<Rigidbody2D>().AddForce (new Vector2(0, -200));
        newStalac.GetComponent<StalacScript>().targetPos = -22.2f;
	}
}
