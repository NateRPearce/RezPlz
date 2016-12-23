using UnityEngine;
using System.Collections;

public class EnergySwordBehavior : MonoBehaviour {

    Animator anim;
    Quaternion startingRot;
    public Transform Savior;
    EidolonBehavior EB;
    public float xForce;
    public float yForce;
    Rigidbody2D rbody;
    FollowScript FS;
    VelocityBasedRotation VBR;
    Collider2D col;

    void Awake()
    {
        anim = GetComponent<Animator>();
        startingRot = transform.rotation;
        EB = Savior.GetComponent<EidolonBehavior>();
        rbody = GetComponent<Rigidbody2D>();
        FS = GetComponent<FollowScript>();
        VBR = GetComponent<VelocityBasedRotation>();
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BossWeakPoint>() != null)
        {
            BossWeakPoint BWP = other.GetComponent<BossWeakPoint>();
            BWP.WeakPointHit();
            anim.SetTrigger("Success");
            VBR.disabled = true;
            rbody.velocity = Vector3.zero;
            col.enabled = false;
        }
        else if(other.tag==Tags.enemy||other.tag==Tags.ground)
        {
            anim.SetTrigger("Fail");
            VBR.disabled = true;
            rbody.velocity = Vector3.zero;
            col.enabled = false;
        }

    }

    public void Aim()
    {

        rbody.freezeRotation = false;
        float angle =  Mathf.Atan2(yForce, xForce) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rbody.freezeRotation = true;
    }

    public void Attack()
    {
        FS.disabled = true;
        anim.SetTrigger("Attack");
    }

    public void Launch()
    {       
        rbody.isKinematic = false;
        VBR.disabled = false;
        FS.disabled = true;
        transform.position = EB.transform.position;
        col.enabled = true;
        rbody.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }


    public void ReturnToStart()
    {
        rbody.freezeRotation = false;
        xForce = 0;
        yForce = 0;
        transform.position = FS.target.position;
        transform.rotation = startingRot;
        rbody.isKinematic = true;
        rbody.freezeRotation = true;
        FS.disabled = false;
    }

    public void SwordReady()
    {
        if (EB.swordL == this.transform)
        {
            EB.swordLReady = true;
        }
        else {
            EB.swordRReady = true;
        }
    }
}
