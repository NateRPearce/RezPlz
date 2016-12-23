using UnityEngine;
using System.Collections;

public class CornerTriggerScript : MonoBehaviour {

	PlayerControls PC;
	float releaseTimer;
	Rigidbody2D rbody;
	public bool hasRbody;
	void Awake(){
		SpriteRenderer ren = GetComponentInParent<SpriteRenderer> ();
		if (GetComponentInParent<Rigidbody2D> () != null) {
			rbody=GetComponentInParent<Rigidbody2D> ();
			hasRbody=true;
		}
		ren.enabled = false;
	}

	void FixedUpdate(){
	if (releaseTimer > 0.5f) {
			PC.hangDisabled=true;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "HandCollider") {				
			PC=other.GetComponentInParent<PlayerControls>();
			if(PC.hangDisabled){
				PC.HangCollider.GetComponent<Collider2D>().enabled=false;
				PC.hanging=false;
				PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                return;
			}
			if(PC.dying){
				return;
			}
			if (Input.GetButton (PC.grabbtn)) {
				PC.HangCollider.GetComponent<Collider2D>().enabled=false;
				PC.hanging=false;
                PC.anim.SetBool("HangeIdle", false);
                PC.anim.SetBool("Hanging", false);
				return;
			}
			PC.hanging=true;
			PC.anim.SetBool("Hanging", true);
			PC.wallJumpL=true;
			PC.wallJumpR=true;
			if(hasRbody){
			
			}else{
                if (PC.GetComponent<Rigidbody2D>().velocity.y > -15)
                {
                    PC.anim.SetBool("HangeIdle", true);
                }               
			PC.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
			if(transform.localScale.x>0&&other.transform.localScale.x<0||transform.localScale.x<0&&other.transform.localScale.x>0){
				PC.Flip();
			}
			PC.transform.position=new Vector3(transform.position.x,transform.position.y,other.transform.position.z);
			}
			}
	}


	void OnTriggerStay2D(Collider2D other){
		if (other.name == "HandCollider") {				
			PC=other.GetComponentInParent<PlayerControls>();
			if(PC.dying){
				return;
			}
			if(PC.hanging&&PC.facingRight&&Input.GetAxis(PC.movebtn)<0){
				releaseTimer+=Time.deltaTime;
			}
			if(PC.hanging&&!PC.facingRight&&Input.GetAxis(PC.movebtn)>0){
				releaseTimer+=Time.deltaTime;
			}
			if(!PC.hanging||Input.GetAxis(PC.movebtn)==0){
				releaseTimer=0;
			}
			if(PC.hangDisabled){
				PC.HangCollider.GetComponent<Collider2D>().enabled=false;
				PC.hanging=false;
				PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                return;
			}
			PC.timeAirborne=0;
			PC.hanging=true;
			PC.anim.SetBool("Hanging", true);
            PC.jumpCooldown = 3;
            PC.wallJumpL=true;
			PC.wallJumpR=true;
            if (Input.GetButtonDown(PC.jumpbtn)){
				return;
			}
			PC.rbody.velocity=new Vector2(0,0);
				if(transform.localScale.x>0&&other.transform.localScale.x<0||transform.localScale.x<0&&other.transform.localScale.x>0){
					PC.Flip();
				}
				PC.transform.position=new Vector3(transform.position.x,transform.position.y,other.transform.position.z);		
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.name == "HandCollider"&&other.GetComponentInParent<PlayerControls> ()) {
			PC=other.GetComponentInParent<PlayerControls>();
			PC.hanging = false;
            PC.anim.SetBool("HangeIdle", false);
            PC.anim.SetBool ("Hanging", false);
		}
		}
}
