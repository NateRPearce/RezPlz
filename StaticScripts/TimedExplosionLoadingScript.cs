using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TimedExplosionLoadingScript : MonoBehaviour {

	public bool lockControls;
	public Transform explosion1;
	public Transform explosion2;
	public float time;
	public float explosionTime1;
	public float explosionTime2;
	public float loadTime;
	public string sceneToLoad;

	void Awake(){
	}

	void FixedUpdate(){
		time += Time.deltaTime;
		if (time > explosionTime1) {
		explosion1.GetComponent<Collider2D>().enabled=true;	
		}
		if (time > explosionTime2) {
		explosion2.GetComponent<Collider2D>().enabled=true;	
		}
		if (time > loadTime) {
			SceneManager.LoadScene(sceneToLoad);
		}
	}
}
