using UnityEngine;
using System.Collections;

public class SkullFlip : GameStateFunctions
{


    public bool mimicTarget;
    public Transform target;
    ParticleSystem[] PS = new ParticleSystem[0];
    ParticleSystem.EmissionModule[] PE;
    FollowScript FS;
    bool facingRight = true;

    void Start()
    {
        FindGM();
        PS = GetComponentsInChildren<ParticleSystem>();
        FS = GetComponent<FollowScript>();
        PE = new ParticleSystem.EmissionModule[PS.Length];
        for (int i = 0; i < PS.Length; i++)
        {
            PE[i] = PS[i].emission;
        }
    }
    void Update()
    {
        if (GM.PM.PC1.eidolonMode && PE[0].rate.constantMax == 0)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.clear;
            GetComponentInChildren<Light>().enabled = false;
            for (int i = 0; i < PE.Length; i++)
            {
                PE[i].rate = new ParticleSystem.MinMaxCurve(0);
            }
            return;
        }
        if (FS.target == null)
        {
            return;
        }
        target = FS.target;
        //Flip the character when direction is changed
        if (mimicTarget)
        {
            if (target.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (target.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
        }
        else
        {
            if (GetComponent<Rigidbody2D>().velocity.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (GetComponent<Rigidbody2D>().velocity.x < 0 && facingRight)
            {
                Flip();
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
