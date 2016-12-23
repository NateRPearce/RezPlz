using UnityEngine;
using System.Collections;

public class TimeControlScript : MonoBehaviour {

	public void setTimeSpeed(float speed){
		Time.timeScale = speed;
	}
	public void Destroy(){
		Destroy (gameObject);
	}
}
