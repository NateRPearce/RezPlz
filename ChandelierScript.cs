using UnityEngine;
using System.Collections;

public class ChandelierScript : MonoBehaviour {

    public PlayerControls PC;
    public Transform chandelierPlat;    
    Rigidbody2D chandRbody;
    public float xOffset;
    public float yOffset;
    public Transform otherCorner;
    ChandelierScript otherCscripts;
    void Awake()
    {
        chandRbody = chandelierPlat.GetComponent<Rigidbody2D>();
        SpriteRenderer ren = GetComponentInParent<SpriteRenderer>();
        ren.enabled = false;
        otherCscripts = otherCorner.GetComponent<ChandelierScript>();
    }
    void Update()
    {
        if (PC != null&&otherCscripts.PC!= null)
        {
            chandelierPlat.GetComponent<Collider2D>().enabled = false;
        }
        if (PC != null)
        {

            if (Input.GetButtonDown(PC.jumpbtn) && PC.ControlsEnabled)
            {
                PC.hangDisabled = true;
                PC.HangCollider.GetComponent<Collider2D>().enabled = false;
                PC.hanging = false;
                PC.flipDisabled = false;
                PC.swinging = false;
                PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                PC.anim.SetBool("Swinging", false);
                StartCoroutine("TempDisable");
                PC.rbody.isKinematic = false;
                PC.rbody.velocity = new Vector2(PC.rbody.velocity.x, 0);
                PC.transform.position = new Vector3(transform.position.x - xOffset, transform.position.y + yOffset, PC.transform.position.z);
                PC.rbody.AddForce(new Vector2(0, PC.jumpForce), ForceMode2D.Impulse);
                PC.FeetCollider.gameObject.layer = 10;
                PC.HeadCollider.gameObject.layer = 10;
                PC.BodyCollider.gameObject.layer = 10;
                PC = null;
                return;
                }
            
            if (Input.GetButtonDown(PC.grabbtn) && PC.ControlsEnabled)
            {
                PC.rbody.isKinematic = false;
                PC.flipDisabled = false;
                PC.hangDisabled = true;
                PC.swinging = false;
                PC.HangCollider.GetComponent<Collider2D>().enabled = false;
                PC.hanging = false;
                PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                PC.anim.SetBool("Swinging", false);
                StartCoroutine("TempDisable");
                PC.rbody.velocity = new Vector2(PC.rbody.velocity.x, 0);
                if (PC.facingRight)
                {
                    PC.rbody.AddForce(new Vector2(-1000, 0), ForceMode2D.Impulse);
                }else
                {
                    PC.rbody.AddForce(new Vector2(1000, 0), ForceMode2D.Impulse);
                }
                PC.FeetCollider.gameObject.layer = 10;
                PC.HeadCollider.gameObject.layer = 10;
                PC.BodyCollider.gameObject.layer = 10;
                PC = null;
                return;
            }
            PC.capeAnim.SetFloat("SwingSpeed", chandRbody.velocity.x);
            PC.anim.SetFloat("TrueSpeed",PC.move);
            PC.transform.position = new Vector3(transform.position.x+xOffset, transform.position.y+yOffset, PC.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "HandCollider"&&PC==null)
        {
            PC = other.GetComponentInParent<PlayerControls>();
            if (PC.hangDisabled)
            {
                PC.HangCollider.GetComponent<Collider2D>().enabled = false;
                PC.hanging = false;
                PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                PC.anim.SetBool("Swinging", false);
                PC.rbody.isKinematic = false;
                return;
            }
            if (PC.dying)
            {
                return;
            }
            PC.hanging = true;
            PC.anim.SetBool("Swinging", true);
            if (PC.facingRight)
            {
                chandRbody.AddForce(new Vector2(50, 0));
            }else
            {
                chandRbody.AddForce(new Vector2(-50, 0));
            }
            PC.rbody.velocity=new Vector2(0,0);
            PC.rbody.isKinematic = true;
            PC.wallJumpL = true;
            PC.wallJumpR = true;
            PC.flipDisabled = true;
            PC.swinging = true;
            PC.FeetCollider.gameObject.layer = 9;
            PC.HeadCollider.gameObject.layer = 9;
            PC.BodyCollider.gameObject.layer = 9;
            if (PC.GetComponent<Rigidbody2D>().velocity.y > -15)
                {
                PC.anim.SetBool("Swinging", true);
                }
            if (transform.localScale.x > 0 && other.transform.localScale.x < 0 || transform.localScale.x < 0 && other.transform.localScale.x > 0)
                {
                    PC.Flip();
                }
                PC.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);
        }
    }

    IEnumerator TempDisable()
    {
        chandelierPlat.GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        chandelierPlat.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().enabled = true;
    }
}


