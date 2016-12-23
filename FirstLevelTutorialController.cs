using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FirstLevelTutorialController : MonoBehaviour {


	public Transform detectionPoint;
	public Image[] btnImg=new Image[2];
	public Text[] txtPrompt=new Text[2];
	public bool playerNear;
	public float detectionRadius;
	public float alpha;
	public LayerMask WhatIsPlayer;

	void Start () {
		alpha = 0;
		btnImg[0].color=new Color(1,1,1,0);
		txtPrompt[0].color=new Color(1,1,1,0);
		btnImg[1].color=new Color(0,0,0,0);
		txtPrompt[1].color=new Color(0,0,0,0);
	}
	

	void Update () {
		playerNear= Physics2D.OverlapCircle (detectionPoint.position, detectionRadius, WhatIsPlayer);
	}
	void FixedUpdate(){
	if (playerNear && alpha < 1) {
			alpha+=0.1f;
		}
		if (!playerNear && alpha > 0) {
			alpha-=0.1f;
		}
		btnImg[0].color=new Color(1,1,1,alpha);
		txtPrompt[0].color=new Color(1,1,1,alpha);
		btnImg[1].color=new Color(0,0,0,alpha);
		txtPrompt[1].color=new Color(0,0,0,alpha);
	}
}
