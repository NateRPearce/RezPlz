using UnityEngine;
using System.Collections;

public class ParticleSortLayerScript : MonoBehaviour {

	public string layerName;
	public int layerNumber;
	ParticleSystem PS;
	// Use this for initialization
	void Start () {
		PS = GetComponent<ParticleSystem> ();
		//Change Foreground to the layer you want it to display on 
		//You could prob. make a public variable for this
		PS.GetComponent<Renderer>().sortingLayerName = layerName;
		PS.GetComponent<Renderer>().sortingOrder = layerNumber;
	}
}
