using UnityEngine;
using System.Collections;

public class FallingCorpseScript : MonoBehaviour {

    Animator anim;
    Rigidbody2D rbody;
    bool grounded;
    bool[] groundChecks = new bool[2];
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask whatIsGround;
    AudioSource source;
    public AudioClip hitGround;

	void Awake () {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
	}
	

	void FixedUpdate () {
        groundChecks[0] = Physics2D.OverlapCircle(groundCheck.position, 1, whatIsGround);
        groundChecks[1] = Physics2D.OverlapCircle(groundCheck2.position, 1, whatIsGround);
        if (groundChecks[0] || groundChecks[1])
        {
            if (!grounded)
            {
                source.PlayOneShot(hitGround, 0.3f);
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
            anim.SetBool("Falling", !grounded);
	}
}
