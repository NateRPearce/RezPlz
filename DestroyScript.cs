using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	public float delay;
	public bool DestroyOnStart;

	void Start(){
		if (DestroyOnStart) {
			Destroy();		
		}
	}

	public void Destroy(){
		Destroy(gameObject, delay);
	}

}
