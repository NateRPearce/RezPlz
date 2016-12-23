using UnityEngine;
using System.Collections;

public class ParralaxScript : MonoBehaviour {
	public Transform mainCam;
	//public Transform camL;
	//public Transform camR;


	void Update () {
		transform.position = Vector3.Lerp (transform.position,new Vector3(mainCam.position.x,mainCam.position.y,transform.position.z),Time.deltaTime/2);
	}
}
