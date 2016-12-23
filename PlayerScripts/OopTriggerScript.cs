using UnityEngine;
using System.Collections;

public class OopTriggerScript : MonoBehaviour {

	Animator anim;
	public bool isPlayer1;
	public string otherPlayer;
	public float strength;
	PlayerControls PC;

	void Start(){
		PC = GetComponentInParent<PlayerControls> ();
		anim = GetComponentInParent<Animator> ();
		if (isPlayer1) {
			otherPlayer="Player2";
		}else{
			otherPlayer="Player1";
		}
	}


	void OnTriggerEnter2D(Collider2D other){
			if (other.name == otherPlayer&&other.GetComponent<Rigidbody2D>()!=null) {
            Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();
                anim.SetBool ("AlleyOopToss",true);
				anim.SetBool ("AlleyOopReady",false);
			if (PC.AB.CrystalList[2].enabled && PC.AB.CrystalList[2].unlocked) {
				PC.SpiritS.OopPhase(3);
			}
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            rbody.AddForce(new Vector2(0,strength),ForceMode2D.Impulse);
			}
	}
}
