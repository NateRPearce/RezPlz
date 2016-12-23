using UnityEngine;
using System.Collections;

public class RingofFireScript : MonoBehaviour {

	public bool dissappate;
	Animator anim;
	public Transform BurningCheckPoint;
	public LayerMask whatBurns;
	float disTime=0;
	void Awake(){
		anim=GetComponent<Animator>();
	}

	void Update(){
		dissappate = !Physics2D.OverlapCircle (BurningCheckPoint.position, 0.25f, whatBurns);
			anim.SetBool ("NotActive",dissappate);
		if (dissappate) {
			disTime += 1;
		}
		if (disTime > 100) {
			transform.position=new Vector2(-100,-100);
			disTime=0;
		}
	}
}
