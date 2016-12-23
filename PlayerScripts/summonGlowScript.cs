using UnityEngine;
using System.Collections;

public class summonGlowScript : MonoBehaviour {

	bool brighten;
	bool dither;
	public Light glow;
	float glowTime;
	public float ditherTime;
	// Use this for initialization
	void Start () {
		glow = GetComponent<Light> ();
		brighten = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Light>().intensity > 7) {
			GetComponent<Light>().intensity=7;		
		}
		if (glowTime > ditherTime) {
			brighten=false;
			dither=true;
		}
		if (brighten) {
			GetComponent<Light>().intensity+=Time.deltaTime*2;	
			glowTime+=Time.deltaTime*2;
		}else if (dither) {
			GetComponent<Light>().intensity-=Time.deltaTime*2;	
			if(GetComponent<Light>().intensity>0.3f){
				Destroy(gameObject);
			}
		}
	}
}
