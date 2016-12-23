using UnityEngine;
using System.Collections;

public class DestroySequenceScript : MonoBehaviour {

	public GameObject[] triggers = new GameObject[0];
	public bool DestroyOnStart;
	public float Delay;


	void Start(){
		if (DestroyOnStart) {
			DestroySequence();
		}
	}

	public void DestroySequence(){
		for(int i =0;i<triggers.Length;i++){
				Destroy(triggers[i].gameObject,Delay);		
		}
	}
}
