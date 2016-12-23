using UnityEngine;
using System.Collections;

public class PlayerHandsScript : GameStateFunctions {

	Vector3 offset;
	PlayerControls PC;
	string grabbtn;
	string movebtn;

	void Start () {
		FindGM ();
		PC = GetComponentInParent<PlayerControls> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (PC.PDS.playerDead)
        {
            if (GetComponent<Collider2D>().enabled)
            {
                GetComponent<Collider2D>().enabled = false;
            }
            return;
        }
        else
        {
            if (!GetComponent<Collider2D>().enabled)
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }
		if (PC.isPlayer1||(!PC.isPlayer1&&GM.numOfPlayers==1)) {
			grabbtn=GM.grabbtn[0];
			movebtn=GM.movebtn[0];
		} else{
			grabbtn=GM.grabbtn[1];
			movebtn=GM.movebtn[1];
		}
	}


	void OnTriggerStay2D(Collider2D other){
	if (other.tag == "Rope"||other.tag=="Legs"&&!PC.exploding&&!PC.onFire&&!PC.bisected) {
		if (Input.GetButton (grabbtn)&&!PC.onFire&&!PC.bisected) {
			if(other.tag=="Legs"){
					offset=new Vector3(-0.215f,-0.05f,0f);
					PC.cape.localRotation=Quaternion.AngleAxis(270,Vector3.forward);
					PC.cape.localPosition=offset;
				if(!PC.facingRight&&PC.transform.localScale.y>0){
						FlipTheY();
				}
			}

				if(Input.GetButton("KB_MoveL")&&(GM.numOfPlayers==1||PC.name=="Player1")){
					PC.anim.SetFloat ("TrueSpeed", -1);
				}else if(Input.GetButton("KB_MoveR")&&(GM.numOfPlayers==1||PC.name=="Player1")){
					PC.anim.SetFloat ("TrueSpeed", 1);
				}else{
					PC.anim.SetFloat ("TrueSpeed", Input.GetAxis (movebtn));
				}
			PC.move = Input.GetAxis (movebtn);
		}
	}
	}
	public void FlipTheY()
	{
		Vector3 theScale = PC.transform.localScale;
		theScale.y *= -1;
		theScale.x *= -1;
		PC.transform.localScale = theScale;
	}
}
