using UnityEngine;
using System.Collections;

public class EndlessPufferScript : MonoBehaviour {

	public PufferScript PS;
	public bool newPufferSpawned;
	public Transform pufferPrefab;
	public Vector3 Origin;
	// Use this for initialization
	void Awake(){
		Origin = transform.position;
	}
	void Start () {
		PS = GetComponentInChildren<PufferScript> ();
		newPufferSpawned = false;
	}
	
	// Update is called once per frame
	void Update () {
	if (PS.exploding&&!newPufferSpawned) {
			Instantiate(pufferPrefab,Origin,transform.rotation);
			newPufferSpawned=true;
		}
	}

}
