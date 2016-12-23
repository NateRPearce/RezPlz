using UnityEngine;
using System.Collections;

public class StungCorpsePlayerHolder : MonoBehaviour {

	PlayerControls PC;
	public Collider2D col;
	public bool HoldingPlayer;
	public Transform graphic;
	public float yOffset;
	float gAngle;
	Rigidbody2D rbody;

	void Awake(){
		col = GetComponent<Collider2D> ();
		rbody = GetComponentInParent<Rigidbody2D> ();
	}

	void Update(){
		if (PC == null) {
			return;
		}
		gAngle+=rbody.velocity.x/4;
		graphic.rotation=Quaternion.AngleAxis(-gAngle,Vector3.forward);
		if (HoldingPlayer) {
			if(PC.anim.GetBool("Dead")){
				StartCoroutine("TempDisable");
				HoldingPlayer=false;
				return;
			}
			if(Input.GetButtonDown(PC.jumpbtn)){
				StartCoroutine("TempDisable");
				return;
			}
			rbody.velocity=new Vector2(PC.move*25,rbody.velocity.y);
			PC.transform.position=new Vector3(transform.position.x,transform.position.y+yOffset,PC.transform.position.z);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponentInParent<PlayerControls> () != null&&!HoldingPlayer) {
			PC=other.GetComponentInParent<PlayerControls>();
			PC.anim.SetBool("LogRolling",true);
			HoldingPlayer=true;
		}
	}

	public IEnumerator TempDisable(){
		HoldingPlayer = false;
		col.enabled = false;
		PC.anim.SetBool("LogRolling",false);
		yield return new WaitForSeconds (0.4f);
		col.enabled = true;
	}
}

