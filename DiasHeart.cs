using UnityEngine;
using System.Collections;

public class DiasHeart : MonoBehaviour {


    [FMODUnity.EventRef]
    string hitSound = "event:/Player/General_Player_Spike_BodySquish";
    FMOD.Studio.EventInstance hitEV;

    public PlayerControls PC;
    public Rigidbody2D rbody;
    public Animator anim;
    public Transform target;
    public bool invert;
    public Animator diasAnim;
    public Collider2D col;
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        diasAnim = transform.parent.GetComponent<Animator>();
        hitEV = FMODUnity.RuntimeManager.CreateInstance(hitSound);
        col = GetComponent<Collider2D>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponentInParent<PlayerControls>()!=null && other.tag == Tags.attackCollider)
        {
            PC = other.GetComponentInParent<PlayerControls>();
            col.isTrigger = false;
            gameObject.layer = 11;
            anim.SetTrigger("Hit");
            diasAnim.SetTrigger("Hit");
            hitEV.start();
            target.GetComponent<RemoteTriggerScript>().Activated=!invert;
            rbody.isKinematic = false;
            if (PC.facingRight)
            {
                rbody.AddForce(new Vector2(1500, 0));
                rbody.AddTorque(-100);
            }else
            {
                Vector3 xScale = transform.localScale;
                xScale.x *= -1;
                transform.localScale = xScale;
                rbody.AddTorque(100);
                rbody.AddForce(new Vector2(-1500, 0));
            }
        }
    }
}
