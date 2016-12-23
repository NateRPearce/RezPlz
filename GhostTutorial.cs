using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GhostTutorial : GameStateFunctions {

	Animator anim;	
	public LayerMask whatIsPlayer;
	public Text tutorialPrompt;
	public Image SpeachBubble;
	int playerOnButton;
	PlayerControls playerPc;
	bool GhostTalk;

	void Start () {
		anim = GetComponent<Animator> ();
		FindGM ();
	}
	
	
	
	void Update(){
		if (playerPc == null) {
			return;
		}
		if(playerPc.ControlsEnabled&&((playerOnButton==1&&(Input.GetButton(GM.PM.PC1.attackbtn))||(playerOnButton==2&&Input.GetButton(GM.PM.PC2.attackbtn))&&!GhostTalk))){
			GhostTalk=true;
		}
	}
	
	
	
	void FixedUpdate () {
		if (playerCheck ()) {
			if(!anim.GetBool("Activated"))
			{
				anim.SetBool("Activated",true);
			}
			if(GhostTalk){
				anim.SetBool("Talk",true);
				gameObject.tag="Untagged";
				tutorialPrompt.color=new Color(0,0,0,1);
				SpeachBubble.color=new Color(1,1,1,1f);
			}else{
				if(!anim.GetBool("Talk")){
				}
				SpeachBubble.color=new Color(0,0,0,0);
			}
		} else {
			gameObject.tag="Button";
			GhostTalk=false;
			SpeachBubble.color=new Color(0,0,0,0);
			if(!GhostTalk){
				tutorialPrompt.color=new Color(0,0,0,0);
				SpeachBubble.color=new Color(0,0,0,0);
			}
		}
	}
	
	
	
	public bool playerCheck(){
		bool playerNear= Physics2D.OverlapCircle (transform.position, 4.5f, whatIsPlayer);
		return playerNear;
	}
	
	
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Player1") {
			playerPc=other.GetComponent<PlayerControls>();
			playerOnButton = 1;
		}else if(other.name=="Player2"){
			playerPc=other.GetComponent<PlayerControls>();
			playerOnButton=2;
		}
	}

}
