using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class TutorialPillar : GameStateFunctions {


	public LayerMask whatIsPlayer;
	public Image btnPrompt;
	public Text tutorialPrompt;
	public Text backDrop;
	public Image textPanel;
	public Light playerCheckLight;
	int playerOnButton;
	PlayerControls playerPc;
	bool pillarActivated;
	Animator anim;


	void Start () {
		anim = GetComponent<Animator> ();
		FindGM ();
		playerCheckLight = GetComponentInChildren<Light> ();
	}



	void Update(){
		if (playerPc == null) {
			return;
		}
		if(playerPc.ControlsEnabled&&((playerOnButton==1&&(Input.GetButton(GM.PM.PC1.grabbtn))||(playerOnButton==2&&Input.GetButton(GM.PM.PC2.grabbtn))&&!pillarActivated))){
			pillarActivated=true;
		}
	}



	void FixedUpdate () {
		if (playerCheck ()) {
			if(pillarActivated){
				anim.SetBool("Triggered",true);
				gameObject.tag="Untagged";
				tutorialPrompt.color=new Color(1,1,1,1);
				btnPrompt.color=new Color(1,1,1,0);
				backDrop.color=new Color(0,0,0,1f);
				textPanel.color=new Color(0,0,0,0.4f);
				playerCheckLight.intensity=8;
			}else{
				if(!anim.GetBool("Triggered")){
					btnPrompt.color=new Color(1,1,1,1);
				}
				backDrop.color=new Color(0,0,0,0);
				textPanel.color=new Color(0,0,0,0);
				playerCheckLight.intensity=3;
			}
		} else {
			gameObject.tag="Button";
			pillarActivated=false;
			btnPrompt.color=new Color(1,1,1,0);
			backDrop.color=new Color(0,0,0,0);
			textPanel.color=new Color(0,0,0,0);
			playerCheckLight.intensity=0;
			if(!pillarActivated){
				tutorialPrompt.color=new Color(1,1,1,0);
				backDrop.color=new Color(0,0,0,0);
				playerCheckLight.intensity=0;
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
