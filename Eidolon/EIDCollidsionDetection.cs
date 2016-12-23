using UnityEngine;
using System.Collections;

public class EIDCollidsionDetection : GameStateFunctions {

	EidolonBehavior EB;
	ManaBar MB;
	bool damageDisabled;
	float hitTime;
	float hitCooldown=0.3f;
    SpriteRenderer EBSprite;

	void Start () {
		EB = GetComponentInParent<EidolonBehavior> ();
		MB = EB.lifeBarMB;
		FindGM ();
        EBSprite = EB.GetComponent<SpriteRenderer>();
	}
	

	void OnTriggerEnter2D(Collider2D other){
		if ((other.tag == Tags.attackCollider||other.tag == "Lava"||other.tag == "Fireball"||other.tag== "InstaKillLava" ||other.tag=="WallSpikes"|| other.tag==Tags.explosion)&&!damageDisabled) {
            Debug.Log(other.tag);
            GM.EidolonHit();
			damageDisabled=true;
			EB.controlsDisabled=true;
            gameObject.layer = 11;
                if (other.GetComponent<Rigidbody2D>() != null&&other.tag!=Tags.explosion)
                {
                Debug.Log("Hit 1");
                    if (other.GetComponent<Rigidbody2D>().velocity.x > 5)
                    {
                        EB.rbody.AddForce(new Vector2(70, 0), ForceMode2D.Impulse);
                    }
                    else if (other.GetComponent<Rigidbody2D>().velocity.x < -5)
                    {
                        EB.rbody.AddForce(new Vector2(-70, 0), ForceMode2D.Impulse);
                    }
                    else
                    {
                        if (other.transform.position.x < transform.position.x)
                        {
                            EB.rbody.AddForce(new Vector2(70, 0), ForceMode2D.Impulse);
                        }
                        else
                        {
                            EB.rbody.AddForce(new Vector2(-70, 0), ForceMode2D.Impulse);
                        }
                    }
                }
                else
                {
                Debug.Log("Hit 2");
                Vector2 push = new Vector2(transform.position.x- other.transform.position.x, transform.position.y- other.transform.position.y).normalized;
                        EB.rbody.AddForce(push*100, ForceMode2D.Impulse);
                }
            if (other.tag == Tags.explosion)
            {
                float xForce=transform.position.x- other.transform.position.x;
                float yForce = transform.position.y - other.transform.position.y;
                Vector2 blastforce = new Vector2(xForce, yForce).normalized;
                EB.rbody.AddForce(blastforce*70, ForceMode2D.Impulse);
            }
            if (GM.EidolonLifeBar.mana > 0)
            {
                EB.anim.SetTrigger("Damaged");
            }
            else
            {
                EB.anim.SetTrigger("Death");
                EB.eidolonActivated = false;
                EB.swordL.gameObject.SetActive(false);
                EB.swordR.gameObject.SetActive(false);
                EB.godBeams.gameObject.SetActive(false);
                EB.bodyCollider.enabled = false;
            }
		}
        StartCoroutine("TempInvincible");
    }
    void OnTriggerStay2D(Collider2D other){
		if ((other.tag == Tags.attackCollider||other.tag == "Lava")&&!damageDisabled) {
			GM.EidolonHit();
			damageDisabled=true;
            if (GM.EidolonLifeBar.mana > 0)
            {
                EB.anim.SetTrigger("Damaged");
            }
            else
            {
                EB.anim.SetTrigger("Death");
                EB.eidolonActivated = false;
                EB.swordL.gameObject.SetActive(false);
                EB.swordR.gameObject.SetActive(false);
                EB.godBeams.gameObject.SetActive(false);
                EB.bodyCollider.enabled = false;
            }
            StartCoroutine("TempInvincible");
        }
	}
    IEnumerator TempInvincible()
    {
        yield return new WaitForSeconds(0.1f);
        EB.controlsDisabled = false;
        gameObject.layer = 10;
        yield return new WaitForSeconds(0.5f);
        damageDisabled = false;
    }
}
