using UnityEngine;
using System.Collections;

public class MaskBoxScript : MonoBehaviour {

	public bool exploded;
	Collider2D mainCol;
	public Rigidbody2D[] pieces=new Rigidbody2D[17];
	public SpriteRenderer mainGraphic;
	public SpriteRenderer shadow;
	void Awake(){
		mainCol = GetComponent<Collider2D> ();
		pieces = GetComponentsInChildren<Rigidbody2D> ();
	}

	void OnTriggerEnter2D(Collider2D other){
	if (other.tag == Tags.attackCollider||other.tag==Tags.explosion) {
			if(!exploded){
			mainCol.enabled=false;
			mainGraphic.enabled=false;
				shadow.enabled=false;
				for(int i=0;i<pieces.Length;i++){
					pieces[i].isKinematic=false;
				}
				exploded=true;
			}
		}
	}
}
