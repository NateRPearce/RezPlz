using UnityEngine;
using System.Collections;

public class PufferScript : FlyingMinionScript {

	public float xDistFromPlayer;
	public float yDistFromPlayer;
	public bool exploding;
	public float maxVelocity;
	public bool stopFlapping;
	public Transform bloodSpurt;
	public bool Entered;
	public bool Entering;
	public SpriteRenderer sr;
	public float enterTime;
	public float enterDelay;
	public Vector3 startingPos;
	public Transform idlePoint;
	public Vector3 TargetPos;
	public bool disableVision;
	Collider2D[] cols;
	int cycle;
    PufferSounds PSounds;

	void Awake () {
        rBody = GetComponent<Rigidbody2D>();
        PSounds = GetComponent<PufferSounds>();
		cols=new Collider2D[GetComponentsInChildren<Collider2D>().Length];
		cols = GetComponentsInChildren<Collider2D> ();
		LDS = GetComponent<LightDetectionScript> ();
		transform.localScale = new Vector3 (5, 5, 5);
		sr = GetComponent<SpriteRenderer> ();
		facingRight = false;
		Entered = false;
		Entering = false;
		sr.sortingOrder=0;
		sr.sortingLayerName="Default";
		anim = GetComponent<Animator> ();
		exploding = false;
		playerSpotted=false;
		flapTime=0;
		hitByPlayer = false;
		gameObject.GetComponent<CircleCollider2D> ().gameObject.layer = 1;
		startingPos = transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		LDS.exploding = exploding;
		anim.SetBool ("Detonate", exploding);
		bodyCollider.isTrigger = !Entered;
		if (facingRight && rBody.velocity.x < 0) {
			Flip ();		
		}
		if (!facingRight && rBody.velocity.x > 0) {
			Flip ();		
		}
		if (hitByPlayer||exploding) {
			return;		
		}
		if (rBody.velocity.x > maxVelocity) {
			rBody.velocity=new Vector2(maxVelocity,rBody.velocity.y);		
		}
		if (rBody.velocity.x < -maxVelocity) {
			rBody.velocity=new Vector2(-maxVelocity,rBody.velocity.y);		
		}
		if (rBody.velocity.y > maxVelocity) {
			rBody.velocity=new Vector2(rBody.velocity.x,maxVelocity);		
		}
		if (rBody.velocity.y < -maxVelocity) {
			rBody.velocity=new Vector2(rBody.velocity.x,-maxVelocity);		
		}
		CheckForWalls ();
	}

	void FixedUpdate () {
		if (!Entered) {
			TargetPos = enterancePoint.localPosition;
			if (enterTime > enterDelay) {
				Enter ();
			} else if (cols [0].enabled) {
				foreach (Collider2D c in cols) {
					c.enabled = false;
				}
			}
		} else {
			TargetPos=idlePoint.localPosition;
		}
		if (Entering) 
			{
			Grow();
			rBody.velocity = new Vector2 (0, 0);
			flapTime = 0;
			if(transform.localScale.y > 10)
				{
				doneEntering();
				}
		}
		enterTime += Time.deltaTime;
		flapTime += Time.deltaTime;
		if (hitByPlayer||exploding) {
			return;		
		}
		if (stopFlapping) {
			flapTime=0;
			return;
		}
		if (playerSpotted&&!disableVision&&Entered) {
			if (flapTime > 0.2f) {
				if (playerIsNorth && playerIsEast) {
					anim.SetBool ("PuffNE", true);
					FlapNE (40);	
				}
				if (playerIsNorth && playerIsWest) {
					FlapNW (40);	
					anim.SetBool ("PuffNW", true);
				}
				if (playerIsSouth && playerIsWest) {
					FlapSW (40);	
					anim.SetBool ("PuffSW", true);
				}
				if (playerIsSouth && playerIsEast) {
					FlapSE (40);	
					anim.SetBool ("PuffSE", true);
				}
			}
			if (flapTime > 0.4f) {
				playerSpotted = false;
				flapTime = 0;
			}
			return;		
		} else {
			if(flapTime>1){
				GoToStart();
			}
		}
	}


	public void resetAnims(){
		anim.SetBool ("PuffSE", false);
		anim.SetBool ("PuffNE", false);
		anim.SetBool ("PuffNW", false);
		anim.SetBool ("PuffSW", false);
	}
	public void CreateBlood(){
		Instantiate(bloodSpurt,transform.position,bloodSpurt.rotation);
	}
	public void doneEntering(){
		foreach(Collider2D c in cols){
			c.enabled=true;
		}
		anim.SetBool ("Entering", false);
		Entered = true;
		Entering = false;
        flapTime = 1;      
    }

	public void Grow(){
		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
		Vector3 currentScale;
		currentScale = transform.localScale;
		transform.localScale = Vector3.MoveTowards (transform.localScale, currentScale*2, Time.deltaTime*20);
		}

	public void Shrink(){
		sr.sortingLayerName="Default";
		sr.sortingOrder=0;
		transform.localScale =new Vector3(5,5,5);
		facingRight = false;
	}

	void Enter(){
		anim.SetBool("Entering",true);
		sr.sortingLayerName="player";
		sr.sortingOrder=4;
		Entering=true;
	}

	public void resetPosition(){
		enterTime = 0;
		Shrink ();
		hitByPlayer = false;
		exploding = false;
		Entered = false;
		Entering = false;
		anim.SetBool ("Detonate", false);
		transform.localPosition = startingPos;
		TargetPos = enterancePoint.localPosition;
	}

	public void Flip2()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void GoToStart(){
		if (Mathf.Abs (transform.localPosition.x - TargetPos.x) < 3 && Mathf.Abs (transform.localPosition.y - TargetPos.y) < 3) {
			//if close to target position loop around
			if (cycle == 0) {
				rBody.velocity = new Vector2 (0, 0);
				anim.SetBool ("PuffNE", true);
				FlapNE (80);
				cycle += 1;
			} else if (cycle == 1) {
                rBody.velocity = new Vector2 (0, 0);
				anim.SetBool ("PuffSE", true);
				FlapSE (80);
				cycle += 1;
			} else if (cycle == 2) {
                rBody.velocity = new Vector2 (0, 0);
				anim.SetBool ("PuffSW", true);
				FlapSW (80);
				cycle += 1;
			} else {
                rBody.velocity = new Vector2 (0, 0);
				anim.SetBool ("PuffNW", true);
				FlapNW (80);
				cycle = 0;
			}
			flapTime = 0;
		} else {
			if (transform.localPosition.x < TargetPos.x && transform.localPosition.y < TargetPos.y) {
                rBody.velocity = new Vector2 (0, 0);
				anim.SetBool ("PuffNE", true);
				FlapNE (140);		
			} else if (transform.localPosition.x > TargetPos.x && transform.localPosition.y < TargetPos.y) {
                rBody.velocity = new Vector2 (0, 0);
				FlapNW (140);
				anim.SetBool ("PuffNW", true);
			} else if (transform.localPosition.x > TargetPos.x && transform.localPosition.y > TargetPos.y) {
                rBody.velocity = new Vector2 (0, 0);
				FlapSW (140);
				anim.SetBool ("PuffSW", true);
			} else if (transform.localPosition.x < TargetPos.x && transform.localPosition.y > TargetPos.y) {
                rBody.velocity = new Vector2 (0, 0);
				FlapSE (140);
				anim.SetBool ("PuffSE", true);
			} else {
                rBody.velocity = new Vector2 (0, 0);
				FlapSE (80);
				anim.SetBool ("PuffSE", true);
			}

			flapTime = 0;
		}
	}

}
