using UnityEngine;
using System.Collections;

public class ParallxingBGScript : MonoBehaviour {

	public Transform[] BGLayersPos= new Transform[1];
	public float[] offSetX=new float[1];
	public float[] LayerSpeed;
	private float[] NewBGLayerXPos;
	private Vector3 startingCamPos;
	private Transform cam;


	void Awake() {
		startingCamPos = Camera.main.transform.position;
	}


	void Start(){
		LayerSpeed=new float[BGLayersPos.Length];
		NewBGLayerXPos=new float[BGLayersPos.Length];
		for (int i=0; i<BGLayersPos.Length; i++) {
			if(BGLayersPos[i]==null){
				return;
			}
			LayerSpeed[i]=BGLayersPos[i].position.z;		
		}
	}
	void FixedUpdate () {
		cam = Camera.main.transform;
		for(int i=0;i<BGLayersPos.Length;i++){
			NewBGLayerXPos[i]=((startingCamPos.x-cam.position.x)/LayerSpeed[i])+offSetX[i];
			if(BGLayersPos[i]==null){
				return;
			}
			BGLayersPos[i].position=Vector3.Lerp(BGLayersPos[i].position,new Vector3(NewBGLayerXPos[i],BGLayersPos[i].position.y,BGLayersPos[i].position.z),Time.deltaTime);
		}

	}
}
