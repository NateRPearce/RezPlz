using UnityEngine;
using System.Collections;

public class AttackColliderScript : MonoBehaviour {

    [FMODUnity.EventRef]
    string hitSound = "event:/NATECHECKTHISOUT/General_Player_Swing_Hit_Enemy_02";
    FMOD.Studio.EventInstance hitEV;

    PlayerDeathScript PDS;
	PlayerControls PC;
    PlayerControls otherPC;
	knockBackScript playerKBS;
	public Transform collisionEffect;
	public Transform collisionEffect2;
	public Transform collisionEffectPos;
    public string objHit;
	bool collisionDetected;
	float collisionCooldown;
	public byte deflectionX;
	public int deflectionY;
	public float attackPower;

	void Start(){
		playerKBS = GetComponentInParent<knockBackScript> ();
		PC = GetComponentInParent<PlayerControls> ();
		PDS = GetComponentInParent<PlayerDeathScript> ();
        hitEV=FMODUnity.RuntimeManager.CreateInstance(hitSound);
    }
    void FixedUpdate(){
		if (collisionDetected) {
			if(collisionDetected&&collisionCooldown==0){
                if (objHit == "ground")
                {
                    //hitGroundEv.start();
                }
			}
			collisionCooldown += Time.deltaTime;
		}
		if (collisionCooldown > 0.4f) {
			collisionCooldown=0;
			collisionDetected=false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
        objHit = other.tag;
		if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Ground" ||other.tag=="Switch") {
			if(PC.facingRight){
				playerKBS.Launched(-2000,0);
			}else{
				playerKBS.Launched(2000,0);
			}
			collisionDetected=true;
		}
		if(other.GetComponent<DeathScript> ()!=null){
			DeathScript DS = other.GetComponent<DeathScript> ();
            if (other.GetComponent<PlayerControls>())
            {
                hitEV.start();
                if (otherPC == null)
                {
                    otherPC = PC.otherPC;
                }
                if (PC.facingRight == otherPC.facingRight)
                {
                    Debug.Log("Face correct");
                    otherPC.Flip();
                }
                otherPC.flipDisabled = true;
            }
            DS.HitBy(tag);
		}
		if(other.GetComponent<knockBackScript> ()!=null){
			knockBackScript KBS = other.GetComponent<knockBackScript> ();
			if(PC.facingRight){
				KBS.Launched(deflectionX*attackPower,deflectionY*2000);
			}else{
				KBS.Launched(-deflectionX*attackPower,deflectionY*2000);
			}
		}
		if(other.GetComponent<BreakableScript>()!=null){
			BreakableScript BS = other.GetComponent<BreakableScript>();
			BS.Break(tag);
		}
		if (other.GetComponent<FireBallScript>()!=null) {
			FireBallScript FBS=other.GetComponent<FireBallScript>();
			Vector2 NewDirection;
			if(PC.facingRight){
				if(other.GetComponent<Rigidbody2D>().velocity.x==0){
					NewDirection=new Vector2(Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.y)*Mathf.Abs(deflectionX),Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.y)*deflectionY);
				}else{
					NewDirection=new Vector2(Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.x)*Mathf.Abs(deflectionX),Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.x)*deflectionY);
				}
			}else{
				if(other.GetComponent<Rigidbody2D>().velocity.x==0){
					NewDirection=new Vector2(Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.y)*Mathf.Abs(deflectionX)*-1,Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.y)*deflectionY);	
				}else{
					NewDirection=new Vector2(Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.x)*Mathf.Abs(deflectionX)*-1,Mathf.Abs(other.GetComponent<Rigidbody2D>().velocity.x)*deflectionY);	
				}
			}
			FBS.Deflected(NewDirection);
			AttackSpark(other.transform.position);
		}

		switch(other.tag){
		case"FireBall":
				PDS.attackDeflected=true;
			break;
		default:
			break;
		}
	}
	public void AttackSpark(Vector3 position){
		Instantiate(collisionEffect,position,transform.rotation);
		Instantiate(collisionEffect2,position,transform.rotation);
	}
}
