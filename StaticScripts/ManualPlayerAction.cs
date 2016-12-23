using UnityEngine;
using System.Collections;

public class ManualPlayerAction : GameStateFunctions {

    public float p2Offset;
	public bool player1Trigger;
	public bool player2Trigger;
	public bool other_Player;
	public GameObject winningPlayer;
	public float playerSpeed;
	public bool jumpTrigger;
	public bool movePlayers;
	public float jumpForce;
	public bool triggered;
	public bool bringP2;
	public bool DestroyOnTrigger;
    public bool moveLock;

	void Start(){
		FindGM ();
	}

	void FixedUpdate(){
		if (triggered&&movePlayers) {
			if(player1Trigger){
			GM.PM.Player1.GetComponent<Rigidbody2D>().velocity=new Vector2(playerSpeed,GM.PM.Player1.GetComponent<Rigidbody2D>().velocity.y);
			}
			if(player2Trigger){
			GM.PM.Player2.GetComponent<Rigidbody2D>().velocity=new Vector2(playerSpeed,GM.PM.Player2.GetComponent<Rigidbody2D>().velocity.y);	
			}
			if(other_Player){
				Rigidbody2D playerRbody=winningPlayer.GetComponentInParent<Rigidbody2D>();
				playerRbody.velocity=new Vector2(playerSpeed,playerRbody.velocity.y);	
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerControls> () != null && bringP2) {
			BringPlayer2(other.gameObject);
			bringP2=false;
		}

		if(jumpTrigger&&other.GetComponent<Rigidbody2D>()!=null){
			if (other.name == "Player1"&&player1Trigger) {
				other.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
			}
			if (other.name == "Player2"&&player2Trigger) {
				other.GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
			}
		}
		if (other.tag== "Player") {
            if (moveLock)
            {
                GM.PM.PC1.moveLocked = true;
                GM.PM.PC2.moveLocked = true;
            }
            triggered =true;
			winningPlayer=other.gameObject;
			if(DestroyOnTrigger){
				Destroy(gameObject);
			}
		}
	}
		void BringPlayer2(GameObject other){
			PlayerControls PC=other.GetComponent<PlayerControls>();
            if (other.name == "Player1")
            {
                GM.PM.PC2.hangDisabled = true;
                GM.PM.PC2.transform.position = new Vector3(other.transform.position.x + p2Offset, other.transform.position.y, other.transform.position.z);
            }
            else
            {
            GM.PM.PC1.hangDisabled = true;
            GM.PM.PC1.transform.position = new Vector3(other.transform.position.x - p2Offset, other.transform.position.y, other.transform.position.z);
            }
		}
}
