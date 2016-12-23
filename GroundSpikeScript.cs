using UnityEngine;
using System.Collections;

public class GroundSpikeScript : MonoBehaviour {

	public Animator anim;
	public bool playerBledOut;
	public Transform bloodSpurt;
	public LayerMask whatIsCorpse;
	public bool corpseOnSpike;
    public bool preBlood;
    public bool disabled;
    DeadlyObject DO;
	Collider2D col;

	void Start(){
        DO = GetComponent<DeadlyObject>();
        if (GetComponentInParent<Animator>() != null)
        {
            anim = GetComponentInParent<Animator>();
        }
		col = GetComponent<Collider2D> ();
        if (preBlood)
        {
            anim.SetBool("PlayerOnSpike", true);
        }
        whatIsCorpse = LayerMask.GetMask("Corpse");
	}


	void FixedUpdate(){
        if (disabled)
        {
            return;
        }
		corpseOnSpike = Physics2D.OverlapCircle (transform.position, 1, whatIsCorpse);
        if (corpseOnSpike && col.enabled) {
            col.enabled = false;
		} else if (!corpseOnSpike && !col.enabled) {
            col.enabled=true;
		}

        if (anim == null)
        {
            return;
        }
        if (corpseOnSpike&& !anim.GetBool("PlayerOnSpike"))
        {
            anim.SetBool("PlayerOnSpike", true);
        }
        else
        {
           
        }
    }

	void OnTriggerEnter2D(Collider2D other){
        if (disabled)
        {
            return;
        }

        if (other.tag == Tags.corpse && other.GetComponent<Rigidbody2D>() != null)
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (other.GetComponent<PlayerControls> ()) {
			if (!playerBledOut) {
					PlayerControls PC = other.GetComponent<PlayerControls> ();
					if(!PC.stoneSkinActivated){
                        PC.moveLocked = true;
                        PC.runningMomentum = 0;
                        PC.move = 0;
                        PC.rbody.velocity = new Vector2(0, 0);
                        if (anim != null)
                        {
                            anim.SetBool("PlayerOnSpike", true);
                        }
                        Instantiate(bloodSpurt, new Vector3(transform.position.x, transform.position.y, -10), bloodSpurt.rotation);
                        playerBledOut = true;
					}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
        if (disabled)
        {
            return;
        }
        if (other.GetComponent<PlayerDeathScript>() != null&& !other.GetComponent<PlayerControls>().stoneSkinActivated)
        {
            if (other.GetComponent<PlayerDeathScript>().playerDead == false){
                Debug.Log("Kill HIM");
                StartCoroutine("TempDisable");
                DO.Kill(other);
                if (!playerBledOut)
                {
                            PlayerControls PC = other.GetComponent<PlayerControls>();
                            if (!PC.stoneSkinActivated)
                            {
                                Instantiate(bloodSpurt, new Vector3(transform.position.x, transform.position.y, -10), bloodSpurt.rotation);
                                if (anim != null)
                                {
                                    anim.SetBool("PlayerOnSpike", true);
                                }
                                playerBledOut = true;
                            }                 
                }
                other.GetComponent<PlayerDeathScript>().playerDead = true;
                if (other.tag == Tags.corpse && other.GetComponent<Rigidbody2D>() != null)
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
                return;                
            }
        }

	}

	void OnTriggerExit2D(Collider2D other){
        if (disabled)
        {
            return;
        }

        if (other.name == "BodyCollider") {
			playerBledOut=false;
		}
	}
    IEnumerator TempDisable()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
    }
}
