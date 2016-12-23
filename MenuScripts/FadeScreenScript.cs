using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FadeScreenScript : MonoBehaviour {

	public bool fadeIn;
	float alpha;
	Image self;
	public GameObject[] CurrentScreen=new GameObject[2];
	public GameObject[] LastScreen = new GameObject[2];
	void Start(){
		self = GetComponent<Image> ();
	}
	void FixedUpdate () {
		if (fadeIn && alpha < 1) {
			self.color=new Color(0,0,0,alpha);
			alpha+=0.1f;
		}
		if (alpha >= 1) {	
			if(fadeIn){
			CurrentScreen[0].SetActive(true);
			CurrentScreen[1].SetActive(true);
			LastScreen[0].SetActive(false);
			LastScreen[1].SetActive(false);
			}
			fadeIn=false;
		}
		if(!fadeIn&&alpha>0){
			self.color=new Color(0,0,0,alpha);
			alpha-=0.1f;
		}
	}
}
