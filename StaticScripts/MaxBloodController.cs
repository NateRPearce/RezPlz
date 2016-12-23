using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MaxBloodController : MonoBehaviour {

	public List<GameObject> bloodTrailCollection = new List<GameObject> ();
	public List<GameObject> bloodSplatterCollection = new List<GameObject> ();
	public List<GameObject> wallBloodCollection = new List<GameObject> ();

	void Update () {
		if (bloodTrailCollection.Count > 50) {
			Destroy(bloodTrailCollection[0]);
			bloodTrailCollection.RemoveAt(0);
		}
		if (bloodSplatterCollection.Count > 50) {
			Destroy(bloodSplatterCollection[0]);
			bloodSplatterCollection.RemoveAt(0);
		}
		if (wallBloodCollection.Count > 50) {
			Destroy(wallBloodCollection[0]);
			wallBloodCollection.RemoveAt(0);
		}
	}
}
