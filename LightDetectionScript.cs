using UnityEngine;
using System.Collections;

public class LightDetectionScript : GameStateFunctions {

	public SpriteRenderer sr;
	public Transform shadow;
	public bool hasShadow;
	public float shadowStartingPosX;
	public bool onFire;
	public bool resurrecting;
	public bool dissolving;
	public bool exploding;
	public bool energyAbsorbed;
	float fullOfEnergyTimer;
	float explodingTimer;
	float playerPosX;
	float playerPosY;
	float lightSourceX;
	float shadowSourceX;
	float lightSourceY;
	float distFromLight;
	float xDistFromLight;
	float yDistFromLight;
	public float xDistFromShadow;
	public LayerMask whereIsLight;
	public PlayerControls PC;
	public int playerNum;

	void Awake () {
		shadowSourceX = 1000;
		lightSourceX = 1000;
		lightSourceY = 1000;
		if (sr == null) {
			if(GetComponentInParent<SpriteRenderer> ()!=null){
				sr = GetComponentInParent<SpriteRenderer> ();
			}else{
				sr = GetComponent<SpriteRenderer> ();
			}
		}
		if (hasShadow) {
			shadowStartingPosX=shadow.localPosition.x;
		}
	}
	void Start(){
		FindCBS ();
		if(GetComponentInParent<PlayerControls>()){
			PC = GetComponentInParent<PlayerControls> ();
		}
	}

	void Update () {
		playerPosX = transform.position.x;
		playerPosY = transform.position.y;
		xDistFromLight =lightSourceX-playerPosX;
		yDistFromLight = lightSourceY-playerPosY;
		xDistFromShadow = Mathf.Abs(shadowSourceX - playerPosX)/15;
		distFromLight = Mathf.Abs (yDistFromLight)+Mathf.Abs (xDistFromLight);
		SearchForLight ();
		if (energyAbsorbed) {
		}else if(exploding) {
		}else if (onFire || resurrecting || dissolving || (1 - (distFromLight / 30) > 1)){
			sr.material.color = new Color (1, 1, 1);
		} else if (1 - (distFromLight / 30) > 0.7f&&1 - (distFromLight / 30) < 1f) {
			sr.material.color = new Color (1 - (distFromLight / 30), 1 - (distFromLight / 30), 1 - (distFromLight / 30));
		} else if ((xDistFromShadow) < 0.7f&&xDistFromShadow > 0) {
			sr.material.color = new Color (xDistFromShadow, xDistFromShadow, xDistFromShadow);
		} else {
			sr.material.color=new Color(0.7f,0.7f,0.7f);
		}
	}
	 void FixedUpdate(){
		if (energyAbsorbed) {
			if(fullOfEnergyTimer<1){
				fullOfEnergyTimer += Time.deltaTime;
				sr.material.color = new Color (fullOfEnergyTimer, fullOfEnergyTimer, fullOfEnergyTimer);
			}else{
				fullOfEnergyTimer=0.7f;
			}
		} else {
			fullOfEnergyTimer=0.7f;
		}
		if (exploding) {
			explodingTimer+=Time.deltaTime;
			if(explodingTimer<0.7f){
				sr.material.color= new Color(0.7f+explodingTimer,0.7f+explodingTimer,0.7f+explodingTimer);
			}else{
				sr.material.color = new Color (1, 1, 1);
			}
		}else{
			explodingTimer=0;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Lava") {
			onFire=true;		
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Fire") {
			lightSourceX=transform.position.x;
			lightSourceY=other.transform.position.y;		
		}
		if (other.tag == "LightSource") {
			lightSourceX=other.transform.position.x;
			lightSourceY=other.transform.position.y;
		}
		if (other.tag == "NextLevelTrigger") {
			shadowSourceX=other.transform.position.x;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Fire") {
			onFire=false;		
		}
		if (other.tag == "LightSource") {
			lightSourceX=other.transform.position.x;
			lightSourceY=other.transform.position.y;
		}
	}
	void SearchForLight(){
        if (CBS == null)
        {
            return;
        }
		if (PC) {
			if(onFire||PC.summoning){
				CBS.nearLight [playerNum] = true;
				CBS.onLight [playerNum] = true;
			}else{
				CBS.nearLight [playerNum] = Physics2D.OverlapCircle (transform.position, 8, whereIsLight);
				CBS.onLight [playerNum] = Physics2D.OverlapCircle (transform.position, 3, whereIsLight);
			}
		}
	}
}
