using UnityEngine;
using System.Collections;

public class TriggerCrystal : BreakableScript {

	public Transform CrystalLight;
	public Transform[] Target = new Transform[1];
	RemoteTriggerScript[] RTS;
	public bool Activated;
	public GameObject graphic;
	public GameObject brokenCrystal;
	public GameObject shadow;
	public Collider2D explosion;
	public GameObject playerSensor;
	public string[] weakness = new string[0];
	float timeSinceDestroyed;

	void Awake(){
		RTS =  new RemoteTriggerScript[Target.Length];
		for(int i=0;i<RTS.Length;i++){
		RTS[i] = Target[i].GetComponent<RemoteTriggerScript> ();
		}
		CrystalLight.GetComponent<Light>().intensity = 0;
	}

	void Update(){
		for(int i=0;i<RTS.Length;i++){
		RTS[i].Activated = Activated;
		}
		if (timeSinceDestroyed > 1) {
			explosion.enabled=false;
		}
	}

	void FixedUpdate(){
		if (Activated) {
			timeSinceDestroyed+=Time.deltaTime;
		}
	}

	public override void Break(string OBJ){
		foreach (string W in weakness) {
			if (OBJ == W) {
				explosion.enabled = true;
				GetComponent<Collider2D>().enabled = false;
				graphic.SetActive (false);
				shadow.SetActive (false);
				brokenCrystal.SetActive (true);
				playerSensor.SetActive (false);
				CrystalLight.GetComponent<Light>().enabled = false;
				Activated = true;
			}
		}
	}
}
