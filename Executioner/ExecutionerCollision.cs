using UnityEngine;
using System.Collections;

public class ExecutionerCollision : MonoBehaviour {

    Animator anim;
    public Collider2D[] col;
    GibCorpse GB;
    Rigidbody2D rbody;
    SpriteRenderer SR;
	void Start () {
        anim = GetComponentInParent<Animator>();
        col = GetComponentsInParent<Collider2D>();
        GB = GetComponent<GibCorpse>();
        rbody = GetComponentInParent<Rigidbody2D>();
        SR = GetComponentInParent<SpriteRenderer>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.explosion)
        {
            anim.SetTrigger("Dead");
            GB.createGiblets();
            col[0].enabled = false;
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
            SR.sortingOrder = 1;
        }
    }
    public void DisableCols()
    {
        foreach (Collider2D c in col)
        {
            c.gameObject.layer = 11;
        }

    }
}
