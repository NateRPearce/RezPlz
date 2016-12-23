using UnityEngine;
using System.Collections;


public class PlayerControls : BasicPlayerFunctions {

	public bool FullDisable;
	public bool disableEidolon;
	public bool eidolonMode;
	SpriteRenderer sr;
	Collider2D[] cols = new Collider2D[0];
    void Awake()
    {
        findChildren();
    }

	void Start () {
		cols = GetComponentsInChildren<Collider2D> ();
		sr=GetComponent<SpriteRenderer>();
		if (FullDisable) {
			return;
		}
			RunStart ();
	}
	
	void Update () {
		if (disableEidolon) {
			eidolonMode=false;
			otherPC.eidolonMode=false;
			//ReinablePlayers();
		}
		if (eidolonMode) {
			if(!otherPC.eidolonMode)
			{
				otherPC.eidolonMode=true;
			}
			if (BodyCollider.GetComponent<Collider2D>().enabled) {
				GetComponent<Rigidbody2D>().isKinematic = true;
				GetComponent<Rigidbody2D>().gravityScale=0;
				foreach (Collider2D c in cols) {
					c.enabled = false;
				}
				anim.enabled=false;
				sr.enabled=false;
				GetComponent<Collider2D>().enabled = false;
				}
			return;
		}
		RunningCheck ();
		if (FullDisable) {
			return;
		}
		RunUpdate ();
	}

	void FixedUpdate(){
		if (FullDisable||eidolonMode) {
			return;
		}
		RunFixedUpdate();
	}

	void ReinablePlayers(){
			//GetComponent<Rigidbody2D>().isKinematic = false;
			GetComponent<Rigidbody2D>().gravityScale=1;
			foreach (Collider2D c in cols) {
				c.enabled = true;
			}
			anim.enabled=true;
			sr.enabled=true;
			GetComponent<Collider2D>().enabled = true;
			disableEidolon = false;
	}
}
