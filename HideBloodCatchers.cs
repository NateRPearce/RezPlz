using UnityEngine;
using System.Collections;

public class HideBloodCatchers : MonoBehaviour {


	private MeshRenderer mesh;
	// Use this for initialization
	void Awake () {
		mesh = GetComponent<MeshRenderer> ();
	if (mesh.enabled) 
		{
			mesh.enabled=false;
		}
	}
}
