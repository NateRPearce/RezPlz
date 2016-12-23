using UnityEngine;
using System.Collections;

public class PlayerLauncher : GameStateFunctions {
	public float xForce;
	public float yForce;
	public float launchDuration;
	void Start () {
		FindGM ();

	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.GetComponent<PlayerControls>()!=null&&other.name=="Player1"){
			StartCoroutine("LaunchP1");
		}

		if(other.GetComponent<PlayerControls>()!=null&&other.name=="Player2"){
			StartCoroutine("LaunchP2");
		}
	}
	IEnumerator LaunchP1(){
		GM.PM.PC1.moveLocked = true;
		GM.PM.PC1.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		GM.PM.PC1.GetComponent<Rigidbody2D>().AddForce (new Vector2 (xForce, yForce),ForceMode2D.Impulse);
		yield return new WaitForSeconds (launchDuration);
		GM.PM.PC1.moveLocked = false;
	}
	IEnumerator LaunchP2(){
		GM.PM.PC2.moveLocked = true;
		GM.PM.PC2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		GM.PM.PC2.GetComponent<Rigidbody2D>().AddForce (new Vector2 (xForce, yForce), ForceMode2D.Impulse);
		yield return new WaitForSeconds (launchDuration);
		GM.PM.PC2.moveLocked = false;
	}
}
