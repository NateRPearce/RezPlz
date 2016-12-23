using UnityEngine;
using System.Collections;

public class WallCorpseHangTrigger : MonoBehaviour {

    public PlayerControls PC;
    public float xOffset;
    public float yOffset;
    WallCorpse WC;
    Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        WC = GetComponentInParent<WallCorpse>();
        SpriteRenderer ren = GetComponentInParent<SpriteRenderer>();
        ren.enabled = false;
    }


    void Update()
    {
        if (PC != null)
        {
            PC.anim.SetBool("Hanging", true);
            PC.anim.SetBool("HangeIdle", true);
            if (PC.stoneSkinActivated)
            {
                return;
            }
            if (Input.GetButtonDown(PC.jumpbtn) && PC.ControlsEnabled)
            {                
                PC.hangDisabled = true;
                PC.HangCollider.GetComponent<Collider2D>().enabled = false;
                PC.hanging = false;
                PC.flipDisabled = false;
                PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                PC.rbody.isKinematic = false;
                StartCoroutine("TempDisable");
                PC.transform.position = new Vector3(transform.position.x + xOffset*2, transform.position.y + yOffset, PC.transform.position.z);
                if (PC.facingRight)
                {
                    Debug.Log("LaunchL");
                    PC.StartCoroutine("LaunchL");
                }
                else
                {
                    Debug.Log("LaunchR");
                    PC.StartCoroutine("LaunchR");
                }
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
                PC.rbody.velocity = new Vector2(PC.rbody.velocity.x, 0);
                StartCoroutine("LongDisable");
                PC = null;
                return;
            }
            if (PC != null)
            PC.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, PC.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (WC.transform.localScale.x < 0)
        {
            xOffset = Mathf.Abs(xOffset) * -1;
        }
        else
        {
            xOffset = Mathf.Abs(xOffset);
        }
        if (other.name == "HandCollider" && PC == null)
        {
            PC = other.GetComponentInParent<PlayerControls>();
            if (PC.hangDisabled)
            {
                PC.HangCollider.GetComponent<Collider2D>().enabled = false;
                PC.hanging = false;
                PC.anim.SetBool("Hanging", false);
                PC.anim.SetBool("HangeIdle", false);
                PC.rbody.isKinematic = false;
                return;
            }
            if (PC.dying)
            {
                return;
            }
            PC.hanging = true;
            PC.anim.SetBool("Hanging", true);
            Debug.Log("Stop velocity");
            PC.rbody.velocity = new Vector2(0, 0);
            PC.rbody.isKinematic = true;
            PC.wallJumpL = true;
            PC.wallJumpR = true;
            PC.flipDisabled = true;
            if (PC.GetComponent<Rigidbody2D>().velocity.y > -15)
            {
                PC.anim.SetBool("Hanging", true);
            }
            if (transform.localScale.x > 0 && other.transform.localScale.x < 0 || transform.localScale.x < 0 && other.transform.localScale.x > 0)
            {
                PC.Flip();
            }
            PC.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            PC.rbody.isKinematic = true;
            PC.transform.position = new Vector3(transform.position.x, transform.position.y, other.transform.position.z);
        }
    }
    public IEnumerator TempDisable()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
    }
    public IEnumerator LongDisable()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
    }
}
